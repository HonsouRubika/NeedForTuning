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
    public string levelScene = "SceneTestAbilities";

    //test for debug purposes
    public bool testIsFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
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
            Debug.Log("start");
            ChunkManager.Instance.selectedLevel = levels[levelActu++];
            SceneManager.LoadScene(levelScene);
        }
        else if(levelActu < levels.Length)
        {
            //TODO : override on equal
            ChunkManager.Instance.selectedLevel = levels[levelActu++];
            ChunkManager.Instance.Preview();
        }
        else
        {
            //tourney finished
            testIsFinished = true;
            Debug.Log("tourney is finished");
            SceneManager.LoadScene("TourneySelection");
        }
    }

}
