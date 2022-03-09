using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Header("CURSOR")]
    public Texture2D cursor;
    public Texture2D cursorClicked;
    private bool clicking;

    private CursorControls controls;

    private Camera mainCamera;

    [Header("CARD INTERACTION")]
    public bool canInteractWithCards = false;
    [SerializeField]private GameObject cardHit;

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        //Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
        clicking = true;
    }

    private void EndedClick()
    {
        ChangeCursor(cursor);
        clicking = false;
    }


    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                //Debug.Log("3D Hit: " + hit.collider.tag);
                if (hit.collider.tag == "Card")
                {
                    Debug.Log("So6");
                    if (canInteractWithCards)
                    {
                        Debug.Log("Showing The Card");
                        cardHit = hit.collider.gameObject;
                        cardHit.GetComponent<Animator>().SetBool("upScale", true);
                        if (clicking)
                        {
                            cardHit.GetComponent<CardBehaviour>().assignPiece();
                        }
                    }                    
                }                
            }                                
        }
        if (hit.collider == null)
        {
            if (cardHit != null)
            {
                Debug.Log("Cho7");
                cardHit.GetComponent<Animator>().SetBool("upScale", false);
                cardHit = null;
            }
        }

        RaycastHit[] hits = Physics.RaycastAll(ray, 200);
    }
}
