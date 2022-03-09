using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Womp : MonoBehaviour
{
    private int willFall;
    public float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        willFall = Random.Range(0, 2);
        GetComponent<Rigidbody>().mass = fallSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "car" && willFall == 1)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
