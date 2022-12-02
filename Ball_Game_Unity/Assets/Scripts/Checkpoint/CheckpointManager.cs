using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public bool useIdSystem;
    //advance checkpoint system
    private Dictionary<int, GameObject> CheckPointDictionary = new Dictionary<int, GameObject>();

    private int currentCheckpointID;
    private int currentID;

    // average checkpoint system
    [SerializeField] private List<GameObject> AllCheckpointParents = new List<GameObject>();

    public int currentCheckpoint = 0;
    private int TrueCheckpointCount = 0;
    private bool isThereCheckpoints = false;

    public Scene MasterScene;

    private Scene scene;
    private Vector3 currentCheckpointResetPoint;
    private AsyncOperation LoadSceneAsync;

    // Start is called before the first frame update
    void Start()
    {
        if (!isThereCheckpoints)
            return;

        if (useIdSystem)
            AdvanceCheckpointStart();
        else
            AverageCheckpointStart(); // this is correct, look closer
    }




    void Update()
    {
        if (!isThereCheckpoints)
            return;

        if (useIdSystem)
            AdvanceCheckpointUpdate();
        else
            AdverageCheckpointUpdate();
    }



    private void AdvanceCheckpointStart()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        scene = SceneManager.GetSceneAt(0);

        foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
        {
            if (PossibleCheckpoint.transform.tag == "CheckpointAdvance")
            {
                CheckPointId CPID = PossibleCheckpoint.GetComponent<CheckPointId>();

                CheckPointDictionary.Add(CPID.ID, CPID._ThisObject);
            }
        }

        if (CheckPointDictionary.Count <= 0)
        {
            isThereCheckpoints = false;
        }
        else
        {
            isThereCheckpoints = true;
        }
    }

    private void AdvanceCheckpointUpdate()
    {
        // var = gets the gameobject with the key (key is the id) then it is like it's gameObject so you can do .transform after.
        currentCheckpointResetPoint = CheckPointDictionary[currentCheckpointID].transform.Find("Respawn Point").position;
    }

    public void SetCurrentCheckpointID(int id)
    {
        currentCheckpointID = id;
    }

    private void AverageCheckpointStart()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        currentCheckpoint = 0;

        scene = SceneManager.GetSceneAt(0);

        AllCheckpointParents.Clear();

        foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
        {
            if (PossibleCheckpoint.transform.tag == "Checkpoint")
            {
                AllCheckpointParents.Add(PossibleCheckpoint);
            }
        }

        PopulateList();

        AllCheckpointParents.Sort(SortByName);

        // if (AllCheckpointParents.Count >= 0)
        //     isThereCheckpoints = true;

        TrueCheckpointCount = AllCheckpointParents.Count - 1;

        if (AllCheckpointParents.Count <= 0)
        {
            isThereCheckpoints = false;
        }
        else
        {
            isThereCheckpoints = true;
        }
    }

    private void AdverageCheckpointUpdate()
    {
        PopulateList();

        if (currentCheckpoint > TrueCheckpointCount)
            currentCheckpoint = TrueCheckpointCount; // could be simplified

        if (currentCheckpoint >= 0)
            currentCheckpointResetPoint = AllCheckpointParents[currentCheckpoint].transform.Find("Respawn Point").position;
    }

    private static int SortByName(GameObject o1, GameObject o2) // simple string sorting by comparing
    {
        return o1.name.CompareTo(o2.name);
    }

    // called so another script can reset the player like the death zones
    public void RestartToChecpoint()
    {
        // this gets the rigidbody and stops all velocity. 
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        // sends the player to the curent checkpoint.
        transform.position = currentCheckpointResetPoint;
    }

    void PopulateList()
    {
        if (LoadSceneAsync == null) return;

        if (LoadSceneAsync.isDone && AllCheckpointParents.Count == 0)
        {
            AllCheckpointParents.Clear();

            currentCheckpoint = 0;

            foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
            {
                if (PossibleCheckpoint.transform.tag == "Checkpoint")
                {
                    AllCheckpointParents.Add(PossibleCheckpoint);
                }
            }

            print(AllCheckpointParents.Count);

            for (int i = 0; i >= AllCheckpointParents.Count; i++)
            {
                print(AllCheckpointParents[i].transform.name);
            }
        }
    }
}
