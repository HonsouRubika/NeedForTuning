using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GhostController : MonoBehaviour
{
    private Rigidbody rb;
    public float ghostSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChunkManager.Instance.isRuning)
        {
            //Debug.Log("ghost moves : " + ChunkManager.Instance.speedActu + " - " + ghostSpeed);
            rb.velocity = new Vector3(0, 0, -ChunkManager.Instance.speedActu + ghostSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //check if finished
        if (ChunkManager.Instance.chunksInLD.Count() > 0 && ChunkManager.Instance.chunksInLD.Last().transform.position.z + 5 <= transform.position.z)
        {
            ChunkManager.Instance.isGhostFinished = true;
        }
    }
}
