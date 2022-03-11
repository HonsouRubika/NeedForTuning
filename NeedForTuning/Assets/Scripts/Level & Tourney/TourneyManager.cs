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
        //TourneySelectionManager.Instance.StartRace();
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
            levelActu = 0;
            //SceneManager.LoadScene("TourneySelection");
            switch (GameManager.Instance.nbOfTurney)
            {
                case 1:
                    SceneManager.LoadScene("OpeningBooster 1");
                    break;
                case 2:
                    SceneManager.LoadScene("OpeningBooster 2");
                    break;
                case 3:
                    SceneManager.LoadScene("OpeningBooster 3");
                    break;
                case 4:
                    SceneManager.LoadScene("OpeningBooster 4");
                    break;
                default:
                    SceneManager.LoadScene("TourneySelection");
                    break;
            }
        }
    }

    public void SpawnCar(GameObject car)
    {
        //GameManager.Instance.car = FindObjectOfType<CarController>().gameObject;
        //GameManager.Instance.GetComponent<CustomizeCar>().UpdateVisual();
        GameManager.Instance.car = car;
        GameManager.Instance.GetComponent<CustomizeCar>().UpdateVisual();
    }

}
