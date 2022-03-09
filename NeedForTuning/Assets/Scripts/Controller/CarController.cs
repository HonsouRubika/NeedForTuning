using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public enum Surface { Concrete,Ice,Sand,Bumps }
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

    private Surface currentSurface;
    //ref
    private AbilityController abilityController;
    private CameraController cameraController;

    //input
    private Vector2 movementInput = Vector2.zero;
    private uint carState = (uint)CarState.idle;
    private bool inObtsacle = false;
    private bool collideWithModule = false;

    private uint lanePosition = 1; //value between 0 and 2 : {0,1,2}

    //speed
    public float changingLaneSpeed = 100f;
    public float changingLaneSpeedLossConcrete = 33f;
    public float changingLaneSpeedLossIce = 33f;
    public float changingLaneSpeedLossSand = 33f;
    public float changingLaneSpeedLossBumps = 33f;
    public float obstacleSpeedLoss = 33f;
    public float surfaceSpeedLoss = 33f;
    public float engineAccelerationConcrete = 150f; // with deltaTime
    public float engineAccelerationIce = 100f; // with deltaTime
    public float engineAccelerationSand = 60f; // with deltaTime
    public float engineAccelerationBumps = 40f; // with deltaTime
    public float engineMaxSpeed = 10f;
    public float engineMinimumSpeed = 1f;
    public float minSpdIce = 6f;
    public float minSpdSand = 3f;
    public float minSpdBumps = 2f;
    public float minSpdObstacle = 1f;
    public float minSpdLanding = 5f;

    //component
    private Rigidbody rb;

    //level ref
    public float lineWidth = 5; 
    public LevelState currentState;
    public enum LevelState
    {
        preview,
        play,
        total
    }

    //JumpPad
    public int jumpPadDistance = 2;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        abilityController = GetComponent<AbilityController>();
        cameraController = GetComponent<CameraController>();
        ApplyPiecesStats();
        
    }

    void ApplyPiecesStats()
    {
        engineMaxSpeed = engineMaxSpeed * InventoryManager.Instance.maxSpeedConcrete*4;
        minSpdIce = minSpdIce * InventoryManager.Instance.maxSpeedIce*4;
        minSpdSand = minSpdSand * InventoryManager.Instance.maxSpeedSand*4;
        minSpdBumps = minSpdBumps * InventoryManager.Instance.maxSpeedBump*4;
        engineAccelerationConcrete = engineAccelerationConcrete * InventoryManager.Instance.accelerationConcrete*4;
        engineAccelerationIce = engineAccelerationIce * InventoryManager.Instance.accelerationIce*4;
        engineAccelerationSand = engineAccelerationSand * InventoryManager.Instance.accelerationSand*4;
        engineAccelerationBumps = engineAccelerationBumps * InventoryManager.Instance.accelerationBump*4;
        changingLaneSpeedLossConcrete = changingLaneSpeedLossConcrete / (InventoryManager.Instance.gripConcrete*2);
        changingLaneSpeedLossIce = changingLaneSpeedLossIce / (InventoryManager.Instance.gripIce*2);
        changingLaneSpeedLossSand = changingLaneSpeedLossSand / (InventoryManager.Instance.gripSand*2);
        changingLaneSpeedLossBumps = changingLaneSpeedLossBumps / (InventoryManager.Instance.gripBump*2);
        minSpdObstacle += minSpdObstacle * InventoryManager.Instance.resistance*4;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started && carState == (uint)CarState.idle && currentState == LevelState.play)
        {
            movementInput = context.ReadValue<Vector2>();
            carState = (uint)CarState.changing_lane;
        }
        else if (currentState == LevelState.preview)
        {
            movementInput = context.ReadValue<Vector2>();
            cameraController.SetCameraDirection(movementInput);
        }
    }
    public void OnSpace(InputAction.CallbackContext context)
    {
        if (context.started && currentState == LevelState.preview)
        {
            currentState = LevelState.play;

            //reset cam position
            cameraController.ResetCameraPosition();

            //passer en game
            //Debug.Log("play");
            ChunkManager.Instance.InitLD();
        }
        else if (currentState == LevelState.play && ChunkManager.Instance.isFinished)
        {
            currentState = LevelState.preview;
            TourneyManager.Instance.NextLevel();
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
                if (!inObtsacle) CarAccelerate();


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
                case "Car":

                    break;
                case "ChunkBarrel":
                case "ChunkBarrel(Clone)":
                    //abilityController.StopAbility();
                    CarInObstacle(minSpdObstacle);
                    inObtsacle = true;

                    break;
                case "ChunkWaterHole":
                case "ChunkWaterHole(Clone)":
                    if (abilityController.currentAbilityChassis != Abilities.Swim)
                    {
                        CarInObstacle(minSpdObstacle);
                        inObtsacle = true;
                    }
                    break;
                case "ChunkTreeTrunk":
                case "ChunkTreeTrunk(Clone)":
                    CarInObstacle(minSpdObstacle);
                    inObtsacle = true;

                    break;
                case "ChunkJunk":
                case "ChunkJunk(Clone)":
                    CarInObstacle(minSpdObstacle);
                    inObtsacle = true;

                    break;
                case "ChunkLaunchingPad":
                case "ChunkLaunchingPad(Clone)":
                    CarJumping();

                    break;
                case "ChunkIce":
                case "ChunkIce(Clone)":
                    if (abilityController.currentAbilityTire != Abilities.Nail)
                    {
                        CarInSurface(minSpdIce);
                        currentSurface = Surface.Ice;
                        inObtsacle = true;
                    }

                    break;
                case "ChunkSand":
                case "ChunkSand(Clone)":
                    CarInSurface(minSpdSand);
                    inObtsacle = true;
                    currentSurface = Surface.Sand;
                    break;
                default:
                    Debug.Log(module[0].gameObject.name);
                    CarInSurface(minSpdIce);
                    inObtsacle = true;
                    break;
            }
        }
        else
        {
            currentSurface = Surface.Concrete;
            inObtsacle = false;
            collideWithModule = false;
        }
    }

    public void CarAccelerate()
    {
        if (abilityController.currentAbilityEngine != Abilities.Turbo)
        {
            switch (currentSurface)
            {
                case Surface.Concrete:
                    ChunkManager.Instance.speedActu += engineAccelerationConcrete * Time.deltaTime;
                    if (ChunkManager.Instance.speedActu > engineMaxSpeed)
                    {
                        ChunkManager.Instance.speedActu = engineMaxSpeed;
                    }
                    break;
                case Surface.Ice:
                    ChunkManager.Instance.speedActu += engineAccelerationIce * Time.deltaTime;
                    if (ChunkManager.Instance.speedActu > minSpdIce)
                    {
                        ChunkManager.Instance.speedActu = minSpdIce;
                    }
                    break;
                case Surface.Sand:
                    ChunkManager.Instance.speedActu += engineAccelerationSand * Time.deltaTime;
                    if (ChunkManager.Instance.speedActu > minSpdSand)
                    {
                        ChunkManager.Instance.speedActu = minSpdSand;
                    }
                    break;
                case Surface.Bumps:
                    ChunkManager.Instance.speedActu += engineAccelerationBumps * Time.deltaTime;
                    if (ChunkManager.Instance.speedActu > minSpdBumps)
                    {
                        ChunkManager.Instance.speedActu = minSpdBumps;
                    }
                    break;
                default:
                    break;
            }
            
        }
    }

    public void CarDecelerate()
    {
        switch (currentSurface)
        {
            case Surface.Concrete:
                ChunkManager.Instance.speedActu -= changingLaneSpeedLossConcrete * Time.deltaTime;
                if (ChunkManager.Instance.speedActu < 0)
                {
                    ChunkManager.Instance.speedActu = 0;
                }
                break;
            case Surface.Ice:
                ChunkManager.Instance.speedActu -= changingLaneSpeedLossIce * Time.deltaTime;
                if (ChunkManager.Instance.speedActu < 0)
                {
                    ChunkManager.Instance.speedActu = 0;
                }
                break;
            case Surface.Sand:
                ChunkManager.Instance.speedActu -= changingLaneSpeedLossSand * Time.deltaTime;
                if (ChunkManager.Instance.speedActu < 0)
                {
                    ChunkManager.Instance.speedActu = 0;
                }
                break;
            case Surface.Bumps:
                ChunkManager.Instance.speedActu -= changingLaneSpeedLossBumps * Time.deltaTime;
                if (ChunkManager.Instance.speedActu < 0)
                {
                    ChunkManager.Instance.speedActu = 0;
                }
                break;
            default:
                break;
        }
        
    }
    public void CarLanding()
    {
        if (abilityController.currentAbilityChassis != Abilities.Suspension)
        {
            ChunkManager.Instance.speedActu = minSpdLanding;
        }

    }

    public void CarInSurface(float minSpd)
    {
        if (abilityController.currentAbilityTire != Abilities.Dolorean)
        {
            ChunkManager.Instance.speedActu -= surfaceSpeedLoss * Time.deltaTime;
            if (ChunkManager.Instance.speedActu < minSpd)
            {
                ChunkManager.Instance.speedActu = minSpd;
            }
        }

    }
    public void CarInObstacle(float minSpd)
    {
        if (abilityController.currentAbilityChassis != Abilities.Bumper && !collideWithModule)
        {
            
            collideWithModule = true;
            ChunkManager.Instance.speedActu = minSpd;
        }
        else
        {
            CarAccelerate();
        }

    }
    public void CarJumping()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + abilityController.jumpHeight);

        if (abilityController.currentAbilityEngine != Abilities.Turbo)
        {
            ChunkManager.Instance.modulesToCrossLaunchPad = ChunkManager.Instance.totalNbOfLineActu + jumpPadDistance;
        }
        else
        {
            ChunkManager.Instance.modulesToCrossLaunchPad = ChunkManager.Instance.totalNbOfLineActu + (int)(jumpPadDistance * abilityController.turboMultiplier);
        }

    }

    public void EndJump()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - abilityController.jumpHeight);
        CarLanding();
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
        Gizmos.DrawCube(transform.position, transform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
    }
}
