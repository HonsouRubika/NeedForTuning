using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Piece", menuName ="CarPiece")]
public class CarPiece : ScriptableObject
{
    public new string name;
    public Sprite image;

    public int maxSpeed;
    public int acceleration;
    public int adhesion;
    public int resistance;
}
