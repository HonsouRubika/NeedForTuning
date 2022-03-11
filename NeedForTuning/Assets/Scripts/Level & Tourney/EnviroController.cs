using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroController : MonoBehaviour
{
    //component
    private Rigidbody rb;

    private Vector3 originPos;

    void Start()
    {
        originPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChunkManager.Instance.isRuning)
        {
            rb.velocity = new Vector3(0, 0, -ChunkManager.Instance.speedActu);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (ChunkManager.Instance.car.GetComponent<CarController>().currentState == CarController.LevelState.preview)
        {
            transform.position = originPos;
        }
    }
}
