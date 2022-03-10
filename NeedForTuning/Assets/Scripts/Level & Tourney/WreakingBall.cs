using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreakingBall : MonoBehaviour
{
    private Rigidbody rb;
    private MeshCollider mc;
    private bool isDetecting = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if(isDetecting) DetectCar();
    }

    public void DetectCar()
    {
        Debug.Log("oui");
        Collider[] module = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), transform.localScale * 5);

        foreach(Collider col in module)
        {
            if(col.gameObject.name == "Car")
            {
                //Debug.Log("yes");
                rb.isKinematic = false;
                mc.enabled = false;
                isDetecting = false;
                col.gameObject.GetComponent<CarController>().CameraShake();
            }
            else
            {
                //Debug.Log(col.gameObject.name);
            }
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), transform.localScale * 5);
    }

}
