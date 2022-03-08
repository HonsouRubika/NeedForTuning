using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public CarPiece engine;
    public CarPiece tire;
    public CarPiece chassis;

    
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

    
}
