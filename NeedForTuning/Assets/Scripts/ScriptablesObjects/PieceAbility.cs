using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Piece Ability", menuName ="Piece Ability",order = 50)]
public class PieceAbility : ScriptableObject
{
    public new string name;
    public Sprite image;

    public int efficiencyConcrete;
    public int efficiencySand;
    public int efficiencyIce;
    public int efficiencyBump;
    public int moduleDistance;
    public int utilisationNumber;
}
