using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRace : MonoBehaviour
{

    public GameObject pressSpace;

    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.currentState == CarController.LevelState.preview)
        {
            pressSpace.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            pressSpace.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
