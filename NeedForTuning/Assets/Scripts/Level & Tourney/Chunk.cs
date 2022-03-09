using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //component
    private Rigidbody rb;
    public bool isGround = false;

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
        else
        {
            rb.velocity = Vector3.zero;
        }

        //verif if out of camera (passed end line)
        if (ChunkManager.Instance.isRuning && transform.position.z <= ChunkManager.Instance.depopLine.z)
        {
            if (!isGround)
            {
                //Debug.Log("destroy chunk");
                ChunkManager.Instance.chunksInLD.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                //Debug.Log("destroy ground");
                ChunkManager.Instance.groundsInLD.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetInvisible()
    {
        foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>()){
            mr.enabled = false;
        }
    }
}
