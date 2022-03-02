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
        arrived //a termine la course
    }

    //input
    private Vector2 movementInput = Vector2.zero;
    private uint carState = (uint)CarState.idle;
    private uint lanePosition = 1; //value between 0 and 2 : {0,1,2}

    //speed
    public float speedActu = 60f;
    public float changingLaneSpeed = 100f;
    public float changingLaneSpeedLoss = 33f;

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
        switch (carState)
        {
            case (uint)CarState.changing_lane:
                ChangeLane();
                break;
            case (uint)CarState.idle:
                //TODO : accelerate
                break;
            case (uint)CarState.using_capacity:
                //TODO
                break;
            case (uint)CarState.arrived:
                //TODO: car stopps
                break;
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
                    transform.position = new Vector3(lineWidth, transform.position.y, transform.position.z);
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                }
                else if (movementInput.x > 0) // right
                {
                    rb.velocity = new Vector3(rb.velocity.x + (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z - (changingLaneSpeedLoss * Time.deltaTime));

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
                    rb.velocity = new Vector3(rb.velocity.x - (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z - (changingLaneSpeedLoss * Time.deltaTime));

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
                    rb.velocity = new Vector3(rb.velocity.x + (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z - (changingLaneSpeedLoss * Time.deltaTime));

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
                    rb.velocity = new Vector3(rb.velocity.x - (changingLaneSpeed * Time.deltaTime), rb.velocity.y, rb.velocity.z - (changingLaneSpeedLoss * Time.deltaTime));

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
                }
                break;
        }
    }
}
