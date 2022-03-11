using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject car;
    public Timer timerScript;
    public uint nbOfTurney = 0;

    public CustomizeCar customizeCarScript;

    public Tourney[] tourneys;

    private void Start()
    {
        customizeCarScript = GetComponent<CustomizeCar>();
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
    
}
