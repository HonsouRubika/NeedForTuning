using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Abilities {Spring,AutoGearbox,Dolorean,Grip,Nail,Swim,Suspension,Turbo,Bumper}
public class AbilityController : MonoBehaviour
{
    private CarController car;
    [HideInInspector] public CarPiece abilityEngine;
    [HideInInspector] public CarPiece abilityTire;
    [HideInInspector] public CarPiece abilityChassis;
    private Abilities currentAbility;

    [Header ("Abilities")]
    public float turboMultiplier;
    private void Start()
    {
        car = GetComponent<CarController>();
    }
    
    private void TriggerAbility(PieceAbility ability)
    {
        switch (abilityEngine.ability.name)
        {
            case "Spring":
                Spring(ability);
                currentAbility = Abilities.Spring;
                break;
            case "AutoGearbox":
                AutoGearbox(ability);
                currentAbility = Abilities.AutoGearbox;
                break;
            case "Bumper":
                Bumper(ability);
                currentAbility = Abilities.Bumper;
                break;
            case "Dolorean":
                Dolorean(ability);
                currentAbility = Abilities.Dolorean;
                break;
            case "Grip":
                Grip(ability);
                currentAbility = Abilities.Grip;
                break;
            case "Nail":
                Nail(ability);
                currentAbility = Abilities.Nail;
                break;
            case "Suspension":
                Suspension(ability);
                currentAbility = Abilities.Suspension;
                break;
            case "Swim":
                Swim(ability);
                currentAbility = Abilities.Swim;
                break;
            case "Turbo":
                Turbo(ability,true);
                currentAbility = Abilities.Turbo;
                break;
        }
    }
    
    public void StopAbility()
    {
        switch (currentAbility)
        {
            case Abilities.Spring:
            break;
            case Abilities.AutoGearbox:
            break;
            case Abilities.Bumper:
            break;
            case Abilities.Dolorean:
            break;
            case Abilities.Grip:
            break;
            case Abilities.Nail:
            break;
            case Abilities.Suspension:
            break;
            case Abilities.Swim:
            break;
            case Abilities.Turbo:
            Turbo(null,false);
            break;

            
        }
    }
    #region Trigger Ability
    private void ClickEngine()
    {
        TriggerAbility(abilityEngine.ability);
    }

    private void ClickTire()
    {
        TriggerAbility(abilityTire.ability);
    }


    private void ClickChassis()
    {
        TriggerAbility(abilityChassis.ability);
    }
    #endregion

    #region Engine Ability
    private void Grip(PieceAbility ability)
    {

    }

    private void AutoGearbox(PieceAbility ability)
    {

    }

    private void Turbo(PieceAbility ability,bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            
            ChunkManager.Instance.speedActu = car.engineMaxSpeed * turboMultiplier;
        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
        }
        
    }
    #endregion

    #region Tire Ability
    private void Spring(PieceAbility ability)
    {

    }

    private void Dolorean(PieceAbility ability)
    {

    }

    private void Nail(PieceAbility ability)
    {

    }
    #endregion

    #region Chassis Ability
    private void Bumper(PieceAbility ability)
    {

    }

    private void Swim(PieceAbility ability)
    {

    }

    private void Suspension(PieceAbility ability)
    {

    }
    #endregion
}
