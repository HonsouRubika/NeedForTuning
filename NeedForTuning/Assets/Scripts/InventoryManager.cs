using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<CarPiece> ownedPiecesEngine;
    public List<CarPiece> ownedPiecesTire;
    public List<CarPiece> ownedPiecesChassis;
    public CarPiece engine;
    public CarPiece tire;
    public CarPiece chassis;

    [Header("Stats")]
    [Header("MaxSpeed")]

    public float maxSpeedConcrete;

    public float maxSpeedSand;

    public float maxSpeedIce;
    
    public float maxSpeedBump;

    [Header("Acceleration")]

    public float accelerationConcrete;

    public float accelerationSand;

    public float accelerationIce;
    
    public float accelerationBump;

    [Header("Grip")]

    public float gripConcrete;

    public float gripSand;
    public float gripIce;
    
    public float gripBump;

    [Header("Resistance")]
    [Range(0, 5)]
    public float resistance;
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
    [ContextMenu ("launch Stats")]
    public void Stats()
    {
        Debug.Log(maxSpeedConcrete);
        maxSpeedConcrete = (float)((engine.maxSpeedConcrete + tire.maxSpeedConcrete + chassis.maxSpeedConcrete) / 3)/5;
        maxSpeedIce = (float)((engine.maxSpeedIce + tire.maxSpeedIce + chassis.maxSpeedIce) / 3)/5;
        maxSpeedSand = (float)((engine.maxSpeedSand + tire.maxSpeedSand + chassis.maxSpeedSand) / 3)/5;
        maxSpeedBump = (float)((engine.maxSpeedBump + tire.maxSpeedBump + chassis.maxSpeedBump) / 3)/5;
        accelerationConcrete = (float)((engine.accelerationConcrete + tire.accelerationConcrete + chassis.accelerationConcrete) / 3)/5;
        accelerationIce = (float)((engine.accelerationIce + tire.accelerationIce + chassis.accelerationIce) / 3)/5;
        accelerationSand = (float)((engine.accelerationSand + tire.accelerationSand + chassis.accelerationSand) / 3)/5;
        accelerationBump = (float)((engine.accelerationBump + tire.accelerationBump + chassis.accelerationBump) / 3)/5;
        gripConcrete = (float)((engine.gripConcrete + tire.gripConcrete + chassis.gripConcrete) / 3)/5;
        gripIce = (float)((engine.gripIce + tire.gripIce + chassis.gripIce) / 3)/5;
        gripSand = (float)((engine.gripSand + tire.gripSand + chassis.gripSand) / 3)/5;
        gripBump = (float)((engine.gripBump + tire.gripBump + chassis.gripBump) / 3)/5;
        resistance = (float)((engine.resistance + tire.resistance + chassis.resistance) / 3)/5;
    }
}
