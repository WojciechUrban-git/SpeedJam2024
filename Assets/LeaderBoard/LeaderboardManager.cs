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
    public TMP_InputField usernameInput; // Input field for username
    public GameObject prefab; // Prefab with TMP text fields for name and time
    public Transform contentTransform; // Parent of the scroll view content

    private string playerName;
    private bool canUpdateLeaderboard = true; // Throttling flag

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

    public void SetUsername()
    {
        playerName = usernameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Username cannot be empty!");
            return;
        }

        Debug.Log("Username set: " + playerName);
    }

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

        LootLockerSDKManager.GetScoreList(leaderboardID, 50, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Leaderboard fetched successfully!");

                // Clear existing entries
                foreach (Transform child in contentTransform)
                {
                    Destroy(child.gameObject);
                }

                foreach (var entry in response.items)
                {
                    // Instantiate a new prefab and set its data
                    GameObject leaderboardEntry = Instantiate(prefab, contentTransform);
                    TMP_Text[] textFields = leaderboardEntry.GetComponentsInChildren<TMP_Text>();

                    textFields[0].text = entry.member_id; // Name
                    textFields[1].text = entry.score.ToString(); // Score (time)
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
        yield return new WaitForSeconds(60); // Wait for 60 seconds
        canUpdateLeaderboard = true;
    }
}
