using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TourneySelectionManager : MonoBehaviour
{
    public static TourneySelectionManager Instance;
    public LevelProfile[] tutorialTracks;
    public LevelProfile[] saison1Tracks;
    public LevelProfile[] saison2Tracks;
    public LevelProfile[] saison3Tracks;

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
            tourneys = new Tourney[4];

            Tourney tutorial = new Tourney("tutorial", tutorialTracks);
            Tourney saison1 = new Tourney("saison1", saison1Tracks);
            Tourney saison2 = new Tourney("saison2", saison2Tracks);
            Tourney saison3 = new Tourney("saison3", saison3Tracks);
            tourneys[0] = tutorial;
            tourneys[1] = saison1;
            tourneys[2] = saison2;
            tourneys[3] = saison3;
        }
        else
        {
            Debug.Log("all tourneys finished");
        }
    }

    public void PickTourney()
    {
       
        TourneyManager.Instance.levels = new LevelProfile[tourneys[GameManager.Instance.nbOfTurney].GetNbOfLevel()];
        Debug.Log("hihih");
        for (uint i = 0; i< tourneys[GameManager.Instance.nbOfTurney].GetNbOfLevel(); i++)
        {
            Debug.Log("hahah");
            TourneyManager.Instance.levels[i] = tourneys[GameManager.Instance.nbOfTurney].GetLevel(i);
        }

        GameManager.Instance.nbOfTurney++;
        TourneyManager.Instance.NextLevel();
    }
}
