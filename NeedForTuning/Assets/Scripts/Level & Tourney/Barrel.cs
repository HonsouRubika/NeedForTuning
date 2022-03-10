using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    private MeshCollider mc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        DetectCar();
    }

    public void DetectCar()
    {
        Collider[] module = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y - transform.position.y, transform.position.z), transform.localScale);

        foreach(Collider col in module)
        {
            if(col.gameObject.name == "Car")
            {
                //Debug.Log("yes");
                rb.isKinematic = false;
                mc.enabled = false;
                //col.gameObject.GetComponent<CarController>().CameraShake();
            }
            else
            {
                //Debug.Log(col.gameObject.name);
            }
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - transform.position.y, transform.position.z), transform.localScale);
    }

}
