using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TourneySelectionManager : MonoBehaviour
{
    public static TourneySelectionManager Instance;
    public LevelProfile[] tutorialTracks;
    public LevelProfile[] saison1Tracks;
    public LevelProfile[] saison2Tracks;
    public LevelProfile[] saison3Tracks;

    public Tourney[] tourneys;

    public Transform posEngine;
    public Transform posTire;
    public Transform posChasis;

    public GameObject recapSeason1;
    public GameObject recapSeason2;
    public GameObject recapSeason3;

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

    private void Start()
    {
        GameManager.Instance.car = FindObjectOfType<CarController>().gameObject;
        GameManager.Instance.GetComponent<CustomizeCar>().GarageUpdate();
        if (GameManager.Instance.tourneys == null) StartRace();

        switch (GameManager.Instance.nbOfTurney)
        {
            case 0:
                recapSeason1.GetComponent<TextMeshProUGUI>().enabled = true;
                recapSeason2.GetComponent<TextMeshProUGUI>().enabled = false;
                recapSeason3.GetComponent<TextMeshProUGUI>().enabled = false;
                break;
            case 1:
                recapSeason1.GetComponent<TextMeshProUGUI>().enabled = false;
                recapSeason2.GetComponent<TextMeshProUGUI>().enabled = true;
                recapSeason3.GetComponent<TextMeshProUGUI>().enabled = false;
                break;
            case 2:
                recapSeason1.GetComponent<TextMeshProUGUI>().enabled = false;
                recapSeason2.GetComponent<TextMeshProUGUI>().enabled = false;
                recapSeason3.GetComponent<TextMeshProUGUI>().enabled = true;
                break;
        }
    }

    public void OnClickAbilityButton(int nb)
    {
        GameManager.Instance.customizeCarScript.instantiateCards(nb);
    }

    public void StartRace()
    {
        //Debug.Log("start race");
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

            GameManager.Instance.tourneys = tourneys;
        }
        else
        {
            Debug.Log("all tourneys finished");
        }
    }

    public void PickTourney()
    {
        //Debug.Log("ici");
        if (tourneys == null)
        {
            tourneys = GameManager.Instance.tourneys;
        }
       
        TourneyManager.Instance.levels = new LevelProfile[tourneys[GameManager.Instance.nbOfTurney].GetNbOfLevel()];

        //Debug.Log("hihih");
        for (uint i = 0; i< tourneys[GameManager.Instance.nbOfTurney].GetNbOfLevel(); i++)
        {
            TourneyManager.Instance.levels[i] = tourneys[GameManager.Instance.nbOfTurney].GetLevel(i);
        }

        GameManager.Instance.nbOfTurney++;
        TourneyManager.Instance.NextLevel();
    }
}
