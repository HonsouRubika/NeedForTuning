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
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesEngine.Count; i++)
                {
                    switch (i)
                    {
                        case 1:
                            Instantiate(InventoryManager.Instance.ownedPiecesEngine[i].cardPrefab, cardPosEngine.position + new Vector3(-offsetCard,0, 0), cardPosEngine.rotation * Quaternion.Euler(0,0,15f), cardPosEngine);
                            break;
                        case 2:
                            Instantiate(InventoryManager.Instance.ownedPiecesEngine[i].cardPrefab, cardPosEngine.position + new Vector3(offsetCard,0, 0), cardPosEngine.rotation * Quaternion.Euler(0, 0, -15f), cardPosEngine);
                            break;
                        case 0:
                            Instantiate(InventoryManager.Instance.ownedPiecesEngine[i].cardPrefab, cardPosEngine.position, cardPosEngine.rotation, cardPosEngine);
                            break;
                    }
                    
                }
                break;
            case 2:
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesTire.Count; i++)
                {
                    switch (i)
                    {
                        case 1:
                            Instantiate(InventoryManager.Instance.ownedPiecesTire[i].cardPrefab, cardPosTire.position + new Vector3(-offsetCard, 0, 0), cardPosTire.rotation * Quaternion.Euler(0, 0, 15f), cardPosTire);
                            break;                                           
                        case 2:                                              
                            Instantiate(InventoryManager.Instance.ownedPiecesTire[i].cardPrefab, cardPosTire.position + new Vector3(offsetCard, 0, 0), cardPosTire.rotation * Quaternion.Euler(0, 0, -15f), cardPosTire);
                            break;                                           
                        case 0:                                              
                            Instantiate(InventoryManager.Instance.ownedPiecesTire[i].cardPrefab, cardPosTire.position, cardPosTire.rotation, cardPosTire);
                            break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < InventoryManager.Instance.ownedPiecesChassis.Count; i++)
                {
                    switch (i)
                    {
                        case 1:
                            Instantiate(InventoryManager.Instance.ownedPiecesChassis[i].cardPrefab, cardPosChassis.position + new Vector3(-offsetCard, 0, 0), cardPosChassis.rotation * Quaternion.Euler(0, 0, 15f), cardPosChassis);
                            break;
                        case 2:
                            Instantiate(InventoryManager.Instance.ownedPiecesChassis[i].cardPrefab, cardPosChassis.position + new Vector3(offsetCard, 0, 0), cardPosChassis.rotation * Quaternion.Euler(0, 0, -15f), cardPosChassis);
                            break;
                        case 0:
                            Instantiate(InventoryManager.Instance.ownedPiecesChassis[i].cardPrefab, cardPosChassis.position, cardPosChassis.rotation, cardPosChassis);
                            break;
                    }
                }
                break;
            default:
                break;
        }
        
       
        

    }

    [ContextMenu("boom")]
    public void UpdateVisual()
    {
        
        Destroy(GameManager.Instance.car.transform.GetChild(0).gameObject);
        GameObject newChassis = Instantiate(chassis.piecePrefab, GameManager.Instance.car.transform.position, GameManager.Instance.car.transform.rotation, GameManager.Instance.car.transform);
        Instantiate(engine.piecePrefab, newChassis.transform.GetChild(2).transform.position, newChassis.transform.GetChild(2).transform.rotation, newChassis.transform.GetChild(2).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(3).transform.position, newChassis.transform.GetChild(3).transform.rotation, newChassis.transform.GetChild(3).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(4).transform.position, newChassis.transform.GetChild(4).transform.rotation, newChassis.transform.GetChild(4).transform);

    }
}
