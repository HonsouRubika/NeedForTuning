using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //component
    private Rigidbody rb;
    private CarController carScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carScript = GameManager.Instance.car.GetComponent<CarController>();
        StartMovement();
    }

    public void StartMovement()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -6);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -(carScript.speedActu * Time.deltaTime));
        //Debug.Log(carScript.speedActu);
        //verif if out of camera (passed end line)
        if (transform.position.z <= ChunkManager.Instance.depopLine.z)
        {
            ChunkManager.Instance.chunksInLD.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
