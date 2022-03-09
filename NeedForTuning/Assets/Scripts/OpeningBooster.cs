using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningBooster : MonoBehaviour
{
    public CursorController cursorController;
    public TemporaryManager temporaryManager;

    public GameObject button;

    public GameObject card01;
    public GameObject card02;
    public GameObject card03;

    [Header("Pieces in Booster")]
    public CarPiece pieceEngine;
    public CarPiece pieceTire;
    public CarPiece pieceChassis;

    [Space(20)]
    public Transform initialCardPos;
    public Transform targetCard01;
    public Transform targetCard03;

    private Animator anim;
    [Range(100,1000)] public float speed = 100.0f;
    private bool canOpenTheBooster = false;
    private bool openBooster = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (openBooster)
        {
            float step = speed * Time.deltaTime;

            card01.transform.position = Vector3.MoveTowards(card01.transform.position, targetCard01.position, step);
            card03.transform.position = Vector3.MoveTowards(card03.transform.position, targetCard03.position, step);
        }

        if (temporaryManager.indexText == 6)
        {
            canOpenTheBooster = true;
        }
    }

    public void ShowingBooster()
    {
        anim.SetBool("moveBooster", true);
        StartCoroutine(ShowingTheBooster());
        Debug.Log("Showing Booster");
    }

    IEnumerator ShowingTheBooster()
    {
        yield return new WaitForSeconds(1f);
        temporaryManager.DisplayText(temporaryManager.textBook[temporaryManager.indexText]);
    }

    public void OpenBooster()
    {
        if (canOpenTheBooster)
        {
            anim.SetBool("openBooster", true);
            button.SetActive(false);

            card01 = Instantiate(pieceEngine.cardPrefab, initialCardPos.position, pieceEngine.cardPrefab.transform.rotation);

            card02 = Instantiate(pieceTire.cardPrefab, initialCardPos.position, pieceTire.cardPrefab.transform.rotation);
            card03 = Instantiate(pieceChassis.cardPrefab, initialCardPos.position, pieceChassis.cardPrefab.transform.rotation);

            InventoryManager.Instance.AddPiece(pieceEngine, InventoryManager.Instance.ownedPiecesEngine);
            InventoryManager.Instance.AddPiece(pieceTire, InventoryManager.Instance.ownedPiecesTire);
            InventoryManager.Instance.AddPiece(pieceChassis, InventoryManager.Instance.ownedPiecesChassis);
            StartCoroutine(BoosterDisapear());
        }        
    }

    IEnumerator BoosterDisapear()
    {
        yield return new WaitForSeconds(1.5f);
        openBooster = true;
        yield return new WaitForSeconds(1f);
        cursorController.canInteractWithCards = true;
    }
}
