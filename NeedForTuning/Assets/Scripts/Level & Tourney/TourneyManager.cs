using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TourneyManager : MonoBehaviour
{
    public static TourneyManager Instance;

    [HideInInspector]
    public LevelProfile[] levels;
    public int levelActu = 0;
    public string levelScene = "SceneTestController";

    //test for debug purposes
    public bool testIsFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        TourneySelectionManager.Instance.StartRace();
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

    public void NextLevel()
    {
        

        if (levelActu == 0)
        {
            //TODO : override on equal
            //Debug.Log("start");
            //Debug.Log(levels[levelActu].chunks.Length);
            //Debug.Log(levels[levelActu].nbOfLine);
            ChunkManager.Instance.selectedLevel = levels[levelActu++];
            SceneManager.LoadScene(levelScene);
            
        }
        else if(levelActu < levels.Length)
        {
            Debug.Log("reset next level");
            //TODO : override on equal
            GameManager.Instance.timerScript.resetTimer();
            ChunkManager.Instance.selectedLevel = levels[levelActu++];
            ChunkManager.Instance.Preview();
        }
        else
        {
            //tourney finished
            testIsFinished = true;
            SceneManager.LoadScene("TourneySelection");
        }
    }
    public void SpawnCar()
    {
        GameManager.Instance.car = FindObjectOfType<CarController>().gameObject;
        GameManager.Instance.gameObject.GetComponent<CustomizeCar>().UpdateVisual();
    }

}
