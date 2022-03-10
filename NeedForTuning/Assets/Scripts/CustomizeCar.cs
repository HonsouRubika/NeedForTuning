using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public CarPiece engine;
    public CarPiece tire;
    public CarPiece chassis;
    public Transform cardPosEngine;
    public Transform cardPosTire;
    public Transform cardPosChassis;
    public float offsetCard;

    public void ConfirmSelection()
    {
        InventoryManager.Instance.engine = engine;
        InventoryManager.Instance.tire = tire;
        InventoryManager.Instance.chassis = chassis;
        InventoryManager.Instance.Stats();
    }

    public void instantiateCards(int index)
    {
        switch (index)
        {
            case 1:
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesEngine.Count - 1; i++)
                {
                    Instantiate(InventoryManager.Instance.ownedPiecesEngine[i].cardPrefab, cardPosEngine.position + new Vector3(offsetCard*i,0,0), cardPosEngine.rotation, cardPosEngine);
                }
                break;
            case 2:
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesTire.Count - 1; i++)
                {
                    Instantiate(InventoryManager.Instance.ownedPiecesTire[i].cardPrefab, cardPosTire.position, cardPosTire.rotation, cardPosTire);
                }
                break;
            case 3:
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesChassis.Count - 1; i++)
                {
                    Instantiate(InventoryManager.Instance.ownedPiecesChassis[i].cardPrefab, cardPosChassis.position, cardPosChassis.rotation, cardPosChassis);
                }
                break;
            default:
                break;
        }
        
       
        

    }

    [ContextMenu("boom")]
    public void UpdateVisual()
    {
        Vector3 angle = new Vector3(0, 90, 0);

        Destroy(GameManager.Instance.car.transform.GetChild(0).gameObject);
        GameObject newChassis = Instantiate(chassis.piecePrefab, GameManager.Instance.car.transform.position, Quaternion.Euler(angle), GameManager.Instance.car.transform);
        Instantiate(engine.piecePrefab, newChassis.transform.GetChild(2).transform.position, newChassis.transform.GetChild(2).transform.rotation, newChassis.transform.GetChild(2).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(3).transform.position, newChassis.transform.GetChild(3).transform.rotation, newChassis.transform.GetChild(3).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(4).transform.position, newChassis.transform.GetChild(4).transform.rotation, newChassis.transform.GetChild(4).transform);

    }
}
