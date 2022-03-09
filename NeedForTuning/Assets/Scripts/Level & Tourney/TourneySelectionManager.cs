using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourneySelectionManager : MonoBehaviour
{
    public static TourneySelectionManager Instance;
    public LevelProfile[] existantLevels;

    public Tourney[] tourneys;

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

    public void StartRace()
    {
        if (!TourneyManager.Instance.testIsFinished)
        {
            LevelProfile[] testTourneyLevels = { existantLevels[0], existantLevels[0], existantLevels[0] };
            Tourney testTourney = new Tourney("test", testTourneyLevels);
            PickTourney(testTourney);
        }
        else
        {
            Debug.Log("test finished");
        }
    }

    public void PickTourney(Tourney t)
    {
        TourneyManager.Instance.levels = new LevelProfile[t.GetNbOfLevel()];

        for (uint i = 0; i< t.GetNbOfLevel(); i++)
        {
            TourneyManager.Instance.levels[i] = t.GetLevel(i);
        }

        TourneyManager.Instance.NextLevel();
    }
}
