using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    public LevelState currentState;
    public enum LevelState
    {
        preview,
        play,
        total
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = LevelState.preview;
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

    public void OnSpace(InputAction.CallbackContext context)
    {
        if (context.started && currentState == LevelState.preview)
        {
            currentState = LevelState.play;
            //passer en game
            //Debug.Log("play");
            ChunkManager.Instance.InitLD();
        }
        else if(context.started && currentState == LevelState.play && ChunkManager.Instance.isFinished)
        {
            currentState = LevelState.preview;
            TourneyManager.Instance.NextLevel();
        }
    }



}
