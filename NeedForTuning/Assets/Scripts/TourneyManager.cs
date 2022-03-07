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
        if (levelActu < levels.Length)
        {
            ChunkManager.Instance.selectedLevel = levels[levelActu++];
            SceneManager.LoadScene(levelScene);
        }
        else
        {
            //tourney finished
            Debug.Log("tourney is finished");
            SceneManager.LoadScene("TourneySelection");
        }
    }

}
