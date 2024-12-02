using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard Settings")]
    public string leaderboardID = "25523"; // Set your LootLocker leaderboard ID in the Inspector
    //public TMP_InputField usernameInput; // Input field for username
    public GameObject prefab; // Prefab with TMP text fields for name and time
    public Transform contentTransform; // Parent of the scroll view content

    private string playerName;
    private bool canUpdateLeaderboard = true; // Throttling flag

    public static LeaderboardManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    private void Start()
    {
        LootLockerSDKManager.StartGuestSession("GuestPlayer", (response) =>
        {
            if (response.success)
            {
                Debug.Log("LootLocker session started!");
            }
            else
            {
                Debug.LogError("Failed to start LootLocker session: " + response.errorData);
            }
        });
    }

    //public void SetUsername()
    //{
    //    playerName = usernameInput.text;
    //    if (string.IsNullOrEmpty(playerName))
    //    {
    //        Debug.LogError("Username cannot be empty!");
    //        return;
    //    }
//
    //    Debug.Log("Username set: " + playerName);
    //}

    public void SubmitScore(int score)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Username is not set!");
            return;
        }

        LootLockerSDKManager.SubmitScore(playerName, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score submitted successfully!");
            }
            else
            {
                Debug.LogError("Failed to submit score: " + response.errorData);
            }
        });
    }

public void DisplayLeaderboard()
{
    if (!canUpdateLeaderboard)
    {
        Debug.Log("Leaderboard update throttled. Please wait before updating again.");
        return;
    }

    StartCoroutine(ThrottleLeaderboardUpdate());

    LootLockerSDKManager.GetScoreList(leaderboardID, 50, (response) =>
    {
        if (response.success)
        {
            Debug.Log("Leaderboard fetched successfully!");

            // Clear existing entries in the scroll view content
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (var entry in response.items)
            {
                // Convert total seconds into minutes and seconds for display
                int minutes = Mathf.FloorToInt(entry.score / 60f); // Minutes
                int seconds = Mathf.FloorToInt(entry.score % 60f); // Seconds

                // Format time as minutes:seconds
                string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

                // Instantiate a new prefab for each leaderboard entry
                GameObject leaderboardEntry = Instantiate(prefab, contentTransform);
                TMP_Text[] textFields = leaderboardEntry.GetComponentsInChildren<TMP_Text>();

                // Set player name and formatted time in the leaderboard entry
                textFields[0].text = entry.member_id;  // Player Name
                textFields[1].text = formattedTime;    // Formatted Time (minutes:seconds)
            }
        }
        else
        {
            Debug.LogError("Failed to fetch leaderboard: " + response.errorData);
        }
    });
}




    private IEnumerator ThrottleLeaderboardUpdate()
    {
        canUpdateLeaderboard = false;
        yield return new WaitForSeconds(10); // Wait for 60 seconds
        canUpdateLeaderboard = true;
    }
}
