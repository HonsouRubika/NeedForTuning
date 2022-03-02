using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChunkManager : MonoBehaviour
{
    //Singleton
    public static ChunkManager Instance;

    public GameObject[] chunkPrefabs;
    public Vector3 spawn;
    public Vector3 depopLine;

    //chunk generation
    private bool alreadyObstacleInLine = false;
    public List<GameObject> chunksInLD;

    //LD rules
    public uint nbObstacle = 3;
    public float probaObstacleGenerates = 25f; //pourcentage
    public int totalNbOfLine = 50;
    private int totalNbOfLineActu;
    public int nbLineInLD = 5;

    void Start()
    {
        //new seed for random
        Random.InitState((int)Time.time);

        totalNbOfLineActu = 1;
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
            GameManager.Instance.car.GetComponent<CarController>().speedActu = 0;
        }
    }

    public void InitLD()
    {
        //TODO: NewLine x fois
        NewLine();
        NewLine();
        NewLine();
        NewLine();
    }

    public void NewLine()
    {
        totalNbOfLineActu++;

        //reset var
        alreadyObstacleInLine = false;
        float zPos = chunksInLD.Last().transform.position.z + 5;

        //column 1
        GameObject newChunk1 = Instantiate<GameObject>(PickChunk());
        newChunk1.transform.position = new Vector3(spawn.x - 5, spawn.y, zPos);
        newChunk1.GetComponent<Chunk>().StartMovement();
        chunksInLD.Add(newChunk1);

        //column 2
        GameObject newChunk2 = Instantiate<GameObject>(PickChunk());
        newChunk2.transform.position = new Vector3(spawn.x, spawn.y, zPos);
        newChunk2.GetComponent<Chunk>().StartMovement();
        chunksInLD.Add(newChunk2);

        //column 3
        GameObject newChunk3 = Instantiate<GameObject>(PickChunk());
        newChunk3.transform.position = new Vector3(spawn.x + 5, spawn.y, zPos);
        newChunk3.GetComponent<Chunk>().StartMovement();
        chunksInLD.Add(newChunk3);
    }

    public GameObject PickChunk()
    {
        float diceRoll = Random.Range(0, 100);

        if(diceRoll < probaObstacleGenerates && !alreadyObstacleInLine)
        {
            //creates obsacle
            int pickedCHunk = Random.Range(1, (int)Modules.total);
            alreadyObstacleInLine = true;

            //TODO: add modules
            //return chunkPrefabs[pickedCHunk];
            return chunkPrefabs[0];
        }
        else
        {
            //creates empty chunk
            return chunkPrefabs[0];
        }
    }

    public enum Modules
    {
        barrel,
        total
    }
}
