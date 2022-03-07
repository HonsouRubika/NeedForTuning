using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public CarPiece engine;
    public CarPiece tire;
    public CarPiece chassis;

    [Header ("Stats")]
    [Header("MaxSpeed")]
    [Range(0, 5)]
    public int maxSpeedConcrete;
    [Range(0, 5)]
    public int maxSpeedSand;
    [Range(0, 5)]
    public int maxSpeedIce;
    //[Range(0, 5)]
    //public int maxSpeedBump;

    [Header("Acceleration")]
    [Range(0, 5)]
    public int accelerationConcrete;
    [Range(0, 5)]
    public int accelerationSand;
    [Range(0, 5)]
    public int accelerationIce;
    //[Range(0, 5)]
    //public int accelerationBump;

    [Header("Grip")]
    [Range(0, 5)]
    public int gripConcrete;
    [Range(0, 5)]
    public int gripSand;
    [Range(0, 5)]
    public int gripIce;
    //[Range(0, 5)]
    //public int gripBump;

    [Header("Resistance")]
    [Range(0, 5)]
    public int resistance;
    public void ConfirmSelection()
    {
        ChunkManager.Instance.abilityCar.abilityEngine = engine;
        ChunkManager.Instance.abilityCar.abilityTire = tire;
        ChunkManager.Instance.abilityCar.abilityChassis = chassis;
    }

    public void instantiateCards()
    {
        //todo
    }

    public void Stats()
    {
        maxSpeedConcrete = (engine.maxSpeedConcrete + tire.maxSpeedConcrete + chassis.maxSpeedConcrete) / 3;
        maxSpeedIce = (engine.maxSpeedIce + tire.maxSpeedIce + chassis.maxSpeedIce) / 3;
        maxSpeedSand = (engine.maxSpeedSand + tire.maxSpeedSand + chassis.maxSpeedSand) / 3;
        //maxSpeedBump = (engine.maxSpeedBump + tire.maxSpeedBump + chassis.maxSpeedBump) / 3;
        accelerationConcrete = (engine.accelerationConcrete + tire.accelerationConcrete + chassis.accelerationConcrete) / 3;
        accelerationIce = (engine.accelerationIce + tire.accelerationIce + chassis.accelerationIce) / 3;
        accelerationSand = (engine.accelerationSand + tire.accelerationSand + chassis.accelerationSand) / 3;
        //accelerationBump = (engine.accelerationBump + tire.accelerationBump + chassis.accelerationBump) / 3;
        gripConcrete = (engine.gripConcrete + tire.gripConcrete + chassis.gripConcrete) / 3;
        gripIce = (engine.gripIce + tire.gripIce + chassis.gripIce) / 3;
        gripSand = (engine.gripSand + tire.gripSand + chassis.gripSand) / 3;
        //gripBump = (engine.gripBump + tire.gripBump + chassis.gripBump) / 3;
        resistance = (engine.resistance + tire.resistance + chassis.resistance) / 3;
    }
}
