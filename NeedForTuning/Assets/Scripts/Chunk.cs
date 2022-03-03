using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //component
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChunkManager.Instance.isRuning)
        {
            rb.velocity = new Vector3(0, 0, -ChunkManager.Instance.speedActu);
        }

        //verif if out of camera (passed end line)
        if (transform.position.z <= ChunkManager.Instance.depopLine.z)
        {
            ChunkManager.Instance.chunksInLD.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
