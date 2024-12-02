using UnityEngine;
using TMPro;
public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private PipeManager outdoorPipesManager;
    [SerializeField] private PipeManager closetPipesManager;
    [SerializeField] private PipeManager garagePipesManager;
    [SerializeField] private SphereSpawner sphereSpawner;
    [SerializeField] private ToiletFlush toiletCode;
    [SerializeField] private ToiletFlush toiletCode1;
    [SerializeField] private ToiletFlush toiletCode2;

    public bool garagePipes;
    public bool closetPipes;
    public bool outdoorPipes;
    public bool allPipesComplete;
    public bool allToiletsFlushed;

    public TMP_Text textMeshPro;
    public GameObject npc;
    public GameObject toilet;
    public GameObject toilet1;
    public GameObject toilet2;
    public GameObject car;

    public TMP_Text timerText;    
    private float timeElapsed = 0f;
    private bool timerIsRunning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        garagePipes = false;
        closetPipes = false;
        outdoorPipes = false;
        allPipesComplete = false;
        allToiletsFlushed = false;
        textMeshPro.text = "Find Conrad";
    }

    // Update is called once per frame
    void Update()
    {
        if (!allPipesComplete)
        {
            if (outdoorPipesManager.complete) outdoorPipes = true;
            if (closetPipesManager.complete) closetPipes = true;
            if (garagePipesManager.complete) garagePipes = true;
            if (outdoorPipes && closetPipes && garagePipes)
            {
                Debug.Log("ALL PIPES COMPLETE!!!");
                allPipesComplete = true;
                npc.tag = "Selectable";
                textMeshPro.text = "Return to Conrad";
            }
        }
        else if (toiletCode.flushed && toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft < 1 && !allToiletsFlushed)
        {
            textMeshPro.text = "Return to your car!";
            car.tag = "Selectable";
            allToiletsFlushed = true;
            Debug.Log("toilets Flushed!");
            npc.transform.parent.SetPositionAndRotation(new Vector3(36.7f, 1.041f, 24.199f), Quaternion.Euler(-90f, 180f, 0f));
            NPCBehavior npcBehaviour = npc.GetComponent<NPCBehavior>();
            npcBehaviour.enabled = false;
            npc.transform.parent.SetPositionAndRotation(new Vector3(36.7f, 1.041f, 24.199f), Quaternion.Euler(-90f, 180f, 0f));

        }

        if (outdoorPipes && closetPipes && !garagePipes) textMeshPro.text = "Fix garage pipes";
        if (outdoorPipes && !closetPipes && garagePipes) textMeshPro.text = "Fix hidden indoor pipes";
        if (!outdoorPipes && closetPipes && garagePipes) textMeshPro.text = "Fix pipes on the street";
        if ((outdoorPipes && !closetPipes && !garagePipes) || (!outdoorPipes && closetPipes && !garagePipes) || (!outdoorPipes && !closetPipes && garagePipes)) textMeshPro.text = "Fix 2 more pipes";

        if (allPipesComplete && !toiletCode.flushed && !toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 3 toilets";
        if (allPipesComplete && !toiletCode.flushed && !toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 2 toilets";
        if (allPipesComplete && !toiletCode.flushed && toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 2 toilets";
        if (allPipesComplete && toiletCode.flushed && !toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 2 toilets";
        if (allPipesComplete && !toiletCode.flushed && toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 1 toilet";
        if (allPipesComplete && toiletCode.flushed && !toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 1 toilet";
        if (allPipesComplete && toiletCode.flushed && toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft < 1) textMeshPro.text = "Flush 1 toilet";
   
        if (allPipesComplete && !toiletCode.flushed && !toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 2 toilets and empty the bathtub";
        if (allPipesComplete && !toiletCode.flushed && toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 2 toilets and empty the bathtub";
        if (allPipesComplete && toiletCode.flushed && !toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 2 toilets and empty the bathtub";
        if (allPipesComplete && !toiletCode.flushed && toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 1 toilet and empty the bathtub";
        if (allPipesComplete && toiletCode.flushed && !toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 1 toilet and empty the bathtub";
        if (allPipesComplete && toiletCode.flushed && toiletCode1.flushed && !toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Flush 1 toilet and empty the bathtub";
        if (allPipesComplete && toiletCode.flushed && toiletCode1.flushed && toiletCode2.flushed && sphereSpawner.bubblesLeft > 0) textMeshPro.text = "Empty the bathtub";

        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            float seconds = timeElapsed % 60f;
            timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
        }
    }

    public void newObjective()
    {
        if (!allPipesComplete)
        {
            Debug.Log("Objectives start");
            npc.tag = "Untagged";
            outdoorPipesManager.SetPipesToSelectable();
            closetPipesManager.SetPipesToSelectable();
            garagePipesManager.SetPipesToSelectable();
            textMeshPro.text = "Fix 3 pipes";
            timerIsRunning = true;
            timeElapsed = 0f;
        }
        else
        {
            textMeshPro.text = "Flush 3 toilets and empty the bathtub";
            Debug.Log("Time to flush the toilet");
            npc.tag = "Untagged";
            toilet.tag = "Selectable";
            toilet1.tag = "Selectable";
            toilet2.tag = "Selectable";
            sphereSpawner.SpawnSphere();
        }
    }

    public void theEnd()
    {
        timerIsRunning = false;
    }
}
