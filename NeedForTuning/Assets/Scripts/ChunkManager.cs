using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChunkManager : MonoBehaviour
{
    //Singleton
    public static ChunkManager Instance;

    public CarController car;
    public GameObject[] chunkPrefabs;
    public LevelProfile selectedLevel;
    public Vector3 spawn;
    public Vector3 depopLine;

    //chunk generation
    private bool alreadyObstacleInLine = false;
    public List<GameObject> chunksInLD;

    //LD rules
    public uint nbObstacle = 3;
    public float probaObstacleGenerates = 25f; //pourcentage
    public int totalNbOfLine = 50;
    [HideInInspector]
    public int totalNbOfLineActu;
    [HideInInspector]
    public int totalNbOfChunkActu;
    public int modulesToCross;
    
    public int nbLineInLD = 5;

    //movement
    public float speedActu = 6f;

    //start run
    public bool isRuning = false;
    public float startTimer = 1f;
    private float startTimerActu = 0;

    void Start()
    {
        //new seed for random
        Random.InitState((int)Time.time);

        totalNbOfLineActu = 1;
        totalNbOfChunkActu = 0;
        InitLD();
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

        //load level
        if (selectedLevel != null)
        {
            totalNbOfLine = selectedLevel.nbOfLine;
        }

        if(startTimerActu != 0 && !isRuning && Time.time > startTimer)
        {
            isRuning = true;
        }

        if (isRuning)
        {
            //Debug.Log(speedActu);
        }

        //test
        LineSensor();
    }

    public void LineSensor()
    {
        if (chunksInLD.Count() < nbLineInLD * 3 && totalNbOfLineActu < totalNbOfLine)
        {
            NewLine();
        }
        else if(totalNbOfLineActu >= totalNbOfLine)
        {
            //run is finished
            Debug.Log("finished");
            ChunkManager.Instance.speedActu = 0;
        }
    }

    public void InitLD()
    {
        //TODO: NewLine x fois
        NewLine();
        NewLine();
        NewLine();
        NewLine();

        startTimerActu = Time.time + startTimer;
    }

    public void NewLine()
    {
        totalNbOfLineActu++;
        if (totalNbOfLineActu == modulesToCross)
        {
            car.GetComponent<AbilityController>().StopAbility();
        }
        //reset var
        alreadyObstacleInLine = false;
        float zPos = chunksInLD.Last().transform.position.z + 5;

        //column 1
        GameObject newChunk1 = Instantiate<GameObject>(PickChunk());
        newChunk1.transform.position = new Vector3(spawn.x - 5, spawn.y, zPos);
        chunksInLD.Add(newChunk1);

        //column 2
        GameObject newChunk2 = Instantiate<GameObject>(PickChunk());
        newChunk2.transform.position = new Vector3(spawn.x, spawn.y, zPos);
        chunksInLD.Add(newChunk2);

        //column 3
        GameObject newChunk3 = Instantiate<GameObject>(PickChunk());
        newChunk3.transform.position = new Vector3(spawn.x + 5, spawn.y, zPos);
        chunksInLD.Add(newChunk3);
    }

    public GameObject PickChunk()
    {
        if (selectedLevel != null)
        {
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

    public enum Modules
    {
        empty,
        barrel,
        waterfall,
        treeTruck,
        junk,
        launchingPad,
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
