using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    public CarPiece linkedPiece;

    public void assignPiece()
    {
        switch (linkedPiece.type)
        {
            case Type.Engine:
                GameManager.Instance.GetComponent<CustomizeCar>().engine = linkedPiece;
                break;
            case Type.Tire:
                GameManager.Instance.GetComponent<CustomizeCar>().tire = linkedPiece;
                break;
            case Type.Chassis:
                GameManager.Instance.GetComponent<CustomizeCar>().chassis = linkedPiece;
                break;
            default:
                break;
        }
        GameManager.Instance.GetComponent<CustomizeCar>().UpdateVisual();
    }
}
