using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Abilities { Default,Spring, Dolorean, Bumper, Suspension, Swim, AutoGearbox, Trubo, Nailed, Grip };

[CreateAssetMenu(fileName = "New Car Piece", menuName ="CarPiece",order = 50)]
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

    [Header("Grip")]
    [Range(0, 5)]
    public int gripConcrete;
    [Range(0, 5)]
    public int gripSand;
    [Range(0, 5)]
    public int gripIce;
    [Range(0, 5)]
    public int gripBump;

    [Header("Resistance")]
    [Range(0, 5)]
    public int resistance;

    [Header("Ability")]
    [SerializeField]
    Abilities abilities;
}
