using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayCarSpeed : MonoBehaviour
{
    public RectTransform speedMask;

    float maxMaskSize = 130;
    float minMaskSize = 30;

    CarController car;

    private void Start()
    {
        //car = GameManager.Instance.car.GetComponent<CarController>();
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {

        //CreateChunks
        //Debug.Log(scene.name);
        if (scene.name == "SceneTestController")
        {
            Debug.Log("gets car");
            car = FindObjectOfType<CarController>();
            //TourneyManager.Instance.SpawnCar();
        }
    }

    private void Update()
    {
        if (car != null)
        {
            speedMask.sizeDelta = new Vector2((ChunkManager.Instance.speedActu / car.engineMaxSpeed) * maxMaskSize, speedMask.sizeDelta.y);
            if (speedMask.sizeDelta.x < minMaskSize)
            {
                speedMask.sizeDelta = new Vector2(minMaskSize, speedMask.sizeDelta.y);
            }
        }
    }
}
