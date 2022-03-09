using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float cameraSpeed = 10;
    public float chunkWidth = 5;

    public Vector3 originPos;
    public Vector3 minPos;
    public Vector3 maxPos;
    private Vector2 currentDirection;

    private Rigidbody camRb;
    private CarController carController;

    private void Start()
    {
        originPos = cam.transform.position;
        camRb = cam.GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();

        //get line chunks scale
        chunkWidth = carController.lineWidth;

        //get camera position range coordinate
        minPos = originPos;
        maxPos = new Vector3(originPos.x, originPos.y, originPos.z + ChunkManager.Instance.totalNbOfLine * chunkWidth);

    }

    private void Update()
    {
        if(carController.currentState == CarController.LevelState.preview)
        {
            MoveCamera();
        }
    }

    public void SetCameraDirection(Vector2 movementInput)
    {
        currentDirection = movementInput;
    }

    public void MoveCamera()
    {
        //Debug.Log("oui");
        if (currentDirection.x > 0 && cam.transform.position.z > maxPos.z) // up
        {
            camRb.velocity = new Vector3(camRb.velocity.x, camRb.velocity.y, camRb.velocity.z - (cameraSpeed * Time.deltaTime));

            if (cam.transform.position.z > maxPos.z)
            {
                camRb.velocity = new Vector3(camRb.velocity.x, camRb.velocity.y, 0);
                cam.transform.position = maxPos;
                currentDirection = Vector2.zero;
            }
        }
        else if (currentDirection.y < 0 && cam.transform.position.z < minPos.z) //down
        {
            camRb.velocity = new Vector3(camRb.velocity.x, camRb.velocity.y, camRb.velocity.z + (cameraSpeed * Time.deltaTime));

            if (cam.transform.position.z < minPos.z)
            {
                camRb.velocity = new Vector3(camRb.velocity.x, camRb.velocity.y, 0);
                cam.transform.position = minPos;
                currentDirection = Vector2.zero;
            }
        }
    }

    public void ResetCameraPosition()
    {
        cam.transform.position = originPos;
    }
}
