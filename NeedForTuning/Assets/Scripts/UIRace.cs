using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRace : MonoBehaviour
{

    public GameObject pressSpace;

    private CarController carController;

    private void Start()
    {
        carController = GameManager.Instance.car.GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carController == null)
        {
            carController = GameManager.Instance.car.GetComponent<CarController>();
        }

        if (carController != null && carController.currentState == CarController.LevelState.preview)
        {
            pressSpace.GetComponent<TextMeshProUGUI>().text = "Press Space to start";
        }
        else
        {
            pressSpace.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
