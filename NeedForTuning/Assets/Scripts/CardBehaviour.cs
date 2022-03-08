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
                GetComponentInParent<CustomizeCar>().engine = linkedPiece;
                break;
            case Type.Tire:
                GetComponentInParent<CustomizeCar>().tire = linkedPiece;
                break;
            case Type.Chassis:
                GetComponentInParent<CustomizeCar>().chassis = linkedPiece;
                break;
            default:
                break;
        }
        
    }
}
