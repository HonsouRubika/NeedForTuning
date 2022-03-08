using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ChunkManager : MonoBehaviour
{
    //Singleton
    public static ChunkManager Instance;

    public CarController car;
    public GameObject[] chunkPrefabs;
    public GameObject[] groundPrefabs;
    public LevelProfile selectedLevel;
    public Vector3 spawn;
    public Vector3 depopLine;

    //chunk generation
    private bool alreadyObstacleInLine = false;
    public List<GameObject> chunksInLD;
    public List<GameObject> groundsInLD;

    //LD rules
    public uint nbObstacle = 3;
    public float probaObstacleGenerates = 25f; //pourcentage
    public int totalNbOfLine = 50;
    [HideInInspector]
    public int totalNbOfLineActu;
    [HideInInspector]
    public int totalNbOfChunkActu;
    public int modulesToCross;
    public int modulesToCrossLaunchPad;
    
    public int nbLineInLD = 5;

    //movement
    public float speedActu = 6f;

    //start run
    public bool isRuning = false;
    public bool isFinished = false;
    public float startTimer = 1f;
    private float startTimerActu = 0;

    void Start()
    {
        //new seed for random
        Random.InitState((int)Time.time);

        totalNbOfLineActu = 0;
        totalNbOfChunkActu = 0;

        SceneManager.sceneLoaded += OnLoadScene;

        //InitLD();
        //yield return new WaitUntil(()=>)
        //Preview();
    }
    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        if(startTimerActu != 0 && !isRuning && Time.time > startTimer && !isFinished)
        {
            isRuning = true;
        }

        //if (isRuning)
        //{
            //Debug.Log(speedActu);
        //}

        //test
        LineSensor();
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        //CreateChunks
        //Debug.Log(scene.name);
        if (scene.name == "SceneTestAbilities")
        {
            Preview();
        }
    }

    public void LineSensor()
    {
        if (isRuning && chunksInLD.Count() < nbLineInLD * 3 && totalNbOfLineActu < totalNbOfLine)
        {
            NewLine();
        }
        else if(isRuning && totalNbOfLineActu >= totalNbOfLine && chunksInLD.Count() > 0 && chunksInLD.Last().transform.position.z + 5 <= car.transform.position.z)
        {
            //run is finished
            //Debug.Log("finished");
            isFinished = true;
            isRuning = false;
            ChunkManager.Instance.speedActu = 0;
        }
    }

    public void Preview()
    {
        ///TODO : set cam

        //reset var
        isRuning = false;
        isFinished = false;
        startTimerActu = 0;

        //get car
        car = FindObjectOfType<CarController>();

        //generate map
        DeleteAllLine();
        StartLine();

        //load level
        totalNbOfLine = selectedLevel.nbOfLine;

        for (int i = 0; i< totalNbOfLine; i++)
        {
            NewLine();
        }
    }

    public void DeleteAllLine()
    {
        for (int i = 0; i < chunksInLD.Count(); i++)
        {
            Destroy(chunksInLD[i].gameObject);
            Destroy(groundsInLD[i].gameObject);
        }
        chunksInLD.Clear();

        for (int j=0; j< groundsInLD.Count(); j++)
        {
            Destroy(groundsInLD[j].gameObject);
        }
        groundsInLD.Clear();

        totalNbOfLineActu = 0;
        totalNbOfChunkActu = 0;
    }

    public void InitLD()
    {
        DeleteAllLine();

        StartLine();

        //TODO: NewLine x fois
        NewLine();
        NewLine();
        NewLine();
        NewLine();

        startTimerActu = Time.time + startTimer;
    }

    public void StartLine()
    {
        totalNbOfLineActu++;
        GameObject chunk1 = chunkPrefabs[(int)Modules.empty];
        GameObject newChunk1 = Instantiate(chunk1, new Vector3(spawn.x - 5, spawn.y, spawn.z), chunk1.transform.rotation);
        //ground
        GameObject newGround1 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x - 5, spawn.y, spawn.z), chunk1.transform.rotation);
        newGround1.GetComponent<Chunk>().isGround = true;
        chunksInLD.Add(newChunk1);
        groundsInLD.Add(newGround1);

        //column 2
        GameObject chunk2 = chunkPrefabs[(int)Modules.empty];
        GameObject newChunk2 = Instantiate<GameObject>(chunk2, new Vector3(spawn.x, spawn.y, spawn.z), chunk2.transform.rotation);
        //ground
        GameObject newGround2 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x, spawn.y, spawn.z), chunk2.transform.rotation);
        newGround2.GetComponent<Chunk>().isGround = true;

        chunksInLD.Add(newChunk2);
        groundsInLD.Add(newGround2);

        //column 3
        GameObject chunk3 = chunkPrefabs[(int)Modules.empty];
        GameObject newChunk3 = Instantiate<GameObject>(chunk3, new Vector3(spawn.x + 5, spawn.y, spawn.z), chunk3.transform.rotation);
        //ground
        GameObject newGround3 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x + 5, spawn.y, spawn.z), chunk3.transform.rotation);
        newGround3.GetComponent<Chunk>().isGround = true;

        chunksInLD.Add(newChunk3);
        groundsInLD.Add(newGround3);
    }

    public void NewLine()
    {
        totalNbOfLineActu++;
        if (totalNbOfLineActu == modulesToCross)
        {
            car.GetComponent<AbilityController>().StopAbility();
            
        }
        if (totalNbOfLineActu == modulesToCrossLaunchPad)
        {
            car.EndJump();
        }
        //reset var
        alreadyObstacleInLine = false;

        float zPos = chunksInLD.Last().transform.position.z + 5;

        //column 1
        GameObject chunk1 = PickChunk();
        GameObject newChunk1 = Instantiate<GameObject>(chunk1, new Vector3(spawn.x - 5, spawn.y, zPos), chunk1.transform.rotation);
        //ground
        GameObject newGround1 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x - 5, spawn.y, zPos), chunk1.transform.rotation);
        newGround1.GetComponent<Chunk>().isGround = true;
        chunksInLD.Add(newChunk1);
        groundsInLD.Add(newGround1);

        //column 2
        GameObject chunk2 = PickChunk();
        GameObject newChunk2 = Instantiate<GameObject>(chunk2, new Vector3(spawn.x, spawn.y, zPos), chunk2.transform.rotation);
        //ground
        GameObject newGround2 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x, spawn.y, zPos), chunk2.transform.rotation);
        newGround2.GetComponent<Chunk>().isGround = true;

        chunksInLD.Add(newChunk2);
        groundsInLD.Add(newGround2);

        //column 3
        GameObject chunk3 = PickChunk();
        GameObject newChunk3 = Instantiate<GameObject>(chunk3, new Vector3(spawn.x + 5, spawn.y, zPos), chunk3.transform.rotation);
        //ground
        GameObject newGround3 = Instantiate<GameObject>(getGround(selectedLevel.levelType), new Vector3(spawn.x + 5, spawn.y, zPos), chunk3.transform.rotation);
        newGround3.GetComponent<Chunk>().isGround = true;

        chunksInLD.Add(newChunk3);
        groundsInLD.Add(newGround3);
    }

    public GameObject PickChunk()
    {
        if (selectedLevel != null)
        {
            //Debug.Log("oui");
            switch (selectedLevel.chunks[totalNbOfChunkActu])
            {
                case Modules.empty:
                    //Debug.Log((totalNbOfChunkActu) + " empty");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.empty];
                case Modules.barrel:
                    //Debug.Log((totalNbOfChunkActu) + " barrel");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.barrel];
                case Modules.waterfall:
                    //Debug.Log((totalNbOfChunkActu) + " waterfall");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.waterfall];
                case Modules.treeTruck:
                    //Debug.Log((totalNbOfChunkActu) + " treeTruck");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.treeTruck];
                case Modules.junk:
                    //Debug.Log((totalNbOfChunkActu) + " junk");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.junk];
                case Modules.launchingPad:
                    //Debug.Log((totalNbOfChunkActu) + " launchingPad");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.launchingPad];
                case Modules.ice:
                    //Debug.Log((totalNbOfChunkActu) + " launchingPad");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.ice];
                case Modules.sand:
                    //Debug.Log((totalNbOfChunkActu) + " launchingPad");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.sand];
                default:
                    //Debug.Log((totalNbOfChunkActu) + " error");
                    totalNbOfChunkActu++;
                    return chunkPrefabs[(int)Modules.empty];
            }
        }
        else
        {
            float diceRoll = Random.Range(0, 100);

            if (diceRoll < probaObstacleGenerates && !alreadyObstacleInLine)
            {
                //creates obsacle
                int pickedCHunk = Random.Range(1, (int)Modules.total);
                alreadyObstacleInLine = true;

                //TODO: add modules
                //return chunkPrefabs[pickedCHunk];
                totalNbOfChunkActu++;
                return chunkPrefabs[pickedCHunk];
            }
            else
            {
                //creates empty chunk
                totalNbOfChunkActu++;
                return chunkPrefabs[0];
            }
        }
    }

    public GameObject getGround(RoadType r)
    {
        switch (r)
        {
            case RoadType.beton:
                return groundPrefabs[(int)RoadType.beton];
            case RoadType.sable:
                return groundPrefabs[(int)RoadType.sable];
            case RoadType.glace:
                return groundPrefabs[(int)RoadType.glace];
            case RoadType.bosse:
                return groundPrefabs[(int)RoadType.bosse];

            default:
                return groundPrefabs[(int)RoadType.beton];
        }
    }


    public enum Modules
    {
        empty,
        barrel,
        waterfall,
        treeTruck,
        junk,
        launchingPad,
        ice, //debug
        sand, //debug
        total
    }

    public enum RoadType
    {
        beton,
        sable,
        glace,
        bosse,
        total
    }


}
