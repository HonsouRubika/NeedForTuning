using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public CarPiece engine;
    public CarPiece tire;
    public CarPiece chassis;
    public Transform enginePos;

    public void ConfirmSelection()
    {
        InventoryManager.Instance.engine = engine;
        InventoryManager.Instance.tire = tire;
        InventoryManager.Instance.chassis = chassis;
        InventoryManager.Instance.Stats();
    }

    public void instantiateCards()
    {
        //todo
    }

    public void UpdateVisual()
    {
        Destroy(GameManager.Instance.car.transform.GetChild(0));
        GameObject newChassis = Instantiate(chassis.piecePrefab, GameManager.Instance.car.transform.position, chassis.piecePrefab.transform.rotation, GameManager.Instance.car.transform);
        Instantiate(engine.piecePrefab, newChassis.transform.GetChild(1).transform.position, engine.piecePrefab.transform.rotation, newChassis.transform.GetChild(1).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(2).transform.position, tire.piecePrefab.transform.rotation, newChassis.transform.GetChild(2).transform);
        Instantiate(tire.piecePrefab, newChassis.transform.GetChild(3).transform.position, tire.piecePrefab.transform.rotation, newChassis.transform.GetChild(3).transform);

    }
}
