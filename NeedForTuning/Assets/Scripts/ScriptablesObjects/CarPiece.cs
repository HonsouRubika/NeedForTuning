using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Piece", menuName ="CarPiece")]
public class CarPiece : ScriptableObject
{
    public new string name;
    public Sprite image;

    [Header("MaxSpeed")]
    [Range(0,5)]
    public int maxSpeedConcrete;
    [Range(0, 5)]
    public int maxSpeedSand;
    [Range(0, 5)]
    public int maxSpeedIce;
    [Range(0, 5)]
    public int maxSpeedBump;

    [Header("Acceleration")]
    [Range(0, 5)]
    public int accelerationConcrete;
    [Range(0, 5)]
    public int accelerationSand;
    [Range(0, 5)]
    public int accelerationIce;
    [Range(0, 5)]
    public int accelerationBump;

    [Header("Adhesion")]
    [Range(0, 5)]
    public int adhesionConcrete;
    [Range(0, 5)]
    public int adhesionSand;
    [Range(0, 5)]
    public int adhesionIce;
    [Range(0, 5)]
    public int adhesionBump;

    [Header("Resistance")]
    [Range(0, 5)]
    public int resistance;
    

    
}
