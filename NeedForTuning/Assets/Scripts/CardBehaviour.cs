using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (GameManager.Instance.car != null) GameManager.Instance.GetComponent<CustomizeCar>().GarageUpdate();
        else if (TemporaryManager.Instance != null && GameManager.Instance.nbOfTurney > 0) SceneManager.LoadScene("TourneySelection");
    }
}
