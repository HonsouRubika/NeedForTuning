using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<CarPiece> ownedPiecesEngine;
    public List<CarPiece> ownedPiecesTire;
    public List<CarPiece> ownedPiecesChassis;

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }


    public void AddPiece(CarPiece myPiece,List<CarPiece> targetList)
    {
        targetList.Add(myPiece);
    }


}
