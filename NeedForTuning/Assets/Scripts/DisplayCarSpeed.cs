using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarSpeed : MonoBehaviour
{
    public RectTransform speedMask;

    float maxMaskSize = 130;
    float minMaskSize = 30;

    CarController car;

    private void Start()
    {
        car = GameManager.Instance.car.GetComponent<CarController>();
    }

    private void Update()
    {
        speedMask.sizeDelta = new Vector2((ChunkManager.Instance.speedActu / car.engineMaxSpeed)*maxMaskSize, speedMask.sizeDelta.y);
        if (speedMask.sizeDelta.x < minMaskSize)
        {
            speedMask.sizeDelta = new Vector2(minMaskSize, speedMask.sizeDelta.y);
        }
    }
}
