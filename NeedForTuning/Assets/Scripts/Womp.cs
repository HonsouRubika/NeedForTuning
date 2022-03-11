using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Womp : MonoBehaviour
{
    private int willFall;
    [Range (0,30)]
    public float fallSpeed;
    public BoxCollider boxCollider;
    public int chunkDetectionRange;
    // Start is called before the first frame update
    void Start()
    {
        willFall = Random.Range(0, 2);
        
        SetDetectionSize();
    }

    public void Fall()
    {
        boxCollider.enabled = false;
        Random.seed = (int)Time.time;
        if (willFall == 1)
        {
            Debug.Log("whomp falls");
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(0, -fallSpeed, 0);
        }
    }

    void SetDetectionSize()
    {
        boxCollider.size = new Vector3(1, 1, chunkDetectionRange);
        boxCollider.center = new Vector3(0, 1, ((float)-(chunkDetectionRange - 1) / 2) - 1f);
    }
}
