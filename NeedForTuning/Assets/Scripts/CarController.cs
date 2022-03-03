using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{

    public enum CarState
    {
        idle, //going strait
        changing_lane, //change de voie
        using_capacity, //est en train d'utiliser une capacite (ex: esquive)
        inObstacleModule, //ralenti car dans un obsacle
        arrived //a termine la course
    }

    //input
    private Vector2 movementInput = Vector2.zero;
    private uint carState = (uint)CarState.idle;
    private bool inObsacle = false;
    private uint lanePosition = 1; //value between 0 and 2 : {0,1,2}

    //speed
    public float changingLaneSpeed = 100f;
    public float changingLaneSpeedLoss = 33f;
    public float engineAcceleration = 150f; // with deltaTime
    public float engineMaxSpeed = 10f;
    public float engineMinimumSpeed = 1f;

    //component
    private Rigidbody rb;

    //level ref
    public float lineWidth = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started && carState == (uint)CarState.idle)
        {
            movementInput = context.ReadValue<Vector2>();
            carState = (uint)CarState.changing_lane;
        }
    }

    private void Update()
    {
        DetectModule();

        switch (carState)
        {
            case (uint)CarState.changing_lane:

                ChangeLane();

                break;
            case (uint)CarState.idle:

                //TODO : accelerate
                if (!inObsacle) CarAccelerate();

                break;
            case (uint)CarState.using_capacity:

                //TODO

                break;
            case (uint)CarState.arrived:

                //TODO: car stopps

                break;
        }

    }

    /*private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "ChunkBarrel":
                CarDecelerate();
                break;
            case "ChunkWaterFall":
                CarDecelerate();
                break;
            case "ChunkTreeTrunk":
                CarDecelerate();
                break;
            case "ChunkJunk":
                CarDecelerate();
                break;
            case "ChunkLaunchingPad":
                CarDecelerate();
                break;
        }
    }*/

    public void DetectModule()
    {
        Collider[] module = Physics.OverlapBox(transform.position, transform.localScale);

        if (module.Length > 0)
        {
            //Debug.Log("module detected");
            switch (module[0].gameObject.name)
            {
                case "ChunkBarrel(Clone)":
                    CarInObstacle();
                    inObsacle = true;
                    break;
                case "ChunkWaterFall(Clone)":
                    CarInObstacle();
                    inObsacle = true;
                    break;
                case "ChunkTreeTrunk(Clone)":
                    CarInObstacle();
                    inObsacle = true;
                    break;
                case "ChunkJunk(Clone)":
                    CarInObstacle();
                    inObsacle = true;
                    break;
                case "ChunkLaunchingPad(Clone)":
                    CarInObstacle();
                    inObsacle = true;
                    break;
                default:
                    Debug.Log(module[0].gameObject.name);
                    inObsacle = false;
                    break;
            }
        }
        else
        {
            inObsacle = false;
        }
    }

    public void CarAccelerate()
    {
        ChunkManager.Instance.speedActu += engineAcceleration * Time.deltaTime;
        if (ChunkManager.Instance.speedActu > engineMaxSpeed) 
        {
            ChunkManager.Instance.speedActu = engineMaxSpeed;
        }
    }

    public void CarDecelerate()
    {
        ChunkManager.Instance.speedActu -= changingLaneSpeedLoss * Time.deltaTime;
        if (ChunkManager.Instance.speedActu < 0)
        {
            ChunkManager.Instance.speedActu = 0;
        }
    }
    
    public void CarInObstacle()
    {
        ChunkManager.Instance.speedActu -= changingLaneSpeedLoss * Time.deltaTime;
        if (ChunkManager.Instance.speedActu < engineMinimumSpeed)
        {
            ChunkManager.Instance.speedActu = engineMinimumSpeed;
        }
    }


    public void ChangeLane()
    {
        switch (lanePosition)
        {
            case 0:
                if (movementInput.x < 0) //left
                {
                    //do not move
                    transform.position = new Vector3(-lineWidth, transform.position.y, transform.position.z);
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                    carState = (uint)CarState.idle;

                }
                else if (movementInput.x > 0) // right
                {
                    rb.velocity = new Vector3(rb.velocity.x + (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z);
                    CarDecelerate();

                    if (transform.position.x >= 0)
                    {
                        //destination reached
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                        lanePosition = 1;
                        carState = (uint)CarState.idle;
                    }
                }
                break;
            case 1:
                if (movementInput.x < 0) //left
                {
                    rb.velocity = new Vector3(rb.velocity.x - (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z);
                    CarDecelerate();

                    if (transform.position.x <= -lineWidth)
                    {
                        //destination reached
                        transform.position = new Vector3(-lineWidth, transform.position.y, transform.position.z);
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                        lanePosition = 0;
                        carState = (uint)CarState.idle;
                    }
                }
                else if (movementInput.x > 0) // right
                {
                    rb.velocity = new Vector3(rb.velocity.x + (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z);
                    CarDecelerate();

                    if (transform.position.x >= lineWidth)
                    {
                        //destination reached
                        transform.position = new Vector3(lineWidth, transform.position.y, transform.position.z);
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                        lanePosition = 2;
                        carState = (uint)CarState.idle;
                    }
                }
                break;
            case 2:
                if (movementInput.x < 0) //left
                {
                    rb.velocity = new Vector3(rb.velocity.x - (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z);
                    CarDecelerate();

                    if (transform.position.x <= 0)
                    {
                        //destination reached
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                        lanePosition = 1;
                        carState = (uint)CarState.idle;
                    }
                }
                else if (movementInput.x > 0) // right
                {
                    transform.position = new Vector3(lineWidth, transform.position.y, transform.position.z);
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                    carState = (uint)CarState.idle;
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawCube(transform.position, transform.localScale+ new Vector3(0.1f,0.1f,0.1f));
    }
}
