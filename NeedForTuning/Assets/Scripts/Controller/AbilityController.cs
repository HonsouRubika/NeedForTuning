using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities { Default, Spring, AutoGearbox, Dolorean, Grip, Nail, Swim, Suspension, Turbo, Bumper }
public class AbilityController : MonoBehaviour
{
    private CarController car;
    /*[HideInInspector]*/
    public CarPiece abilityEngine;
    /*[HideInInspector]*/
    public CarPiece abilityTire;
    /*[HideInInspector]*/
    public CarPiece abilityChassis;
    public Abilities currentAbilityEngine;
    public Abilities currentAbilityTire;
    public Abilities currentAbilityChassis;

    [Header("Abilities")]
    public float turboMultiplier;
    public float jumpHeight;
    public float AutoGearBoxRatio;
    private float GripLaneChange;

    private void Start()
    {
        car = GetComponent<CarController>();
    }

    private void TriggerAbility(PieceAbility ability)
    {


        switch (ability.name)
        {
            case "Spring":
                Spring(ability, true);
                currentAbilityTire = Abilities.Spring;
                break;
            case "AutoGearbox":
                AutoGearbox(ability, true);
                currentAbilityEngine = Abilities.AutoGearbox;
                break;
            case "Bumper":
                Bumper(ability, true);
                currentAbilityChassis = Abilities.Bumper;
                break;
            case "Dolorean":
                Dolorean(ability, true);
                currentAbilityTire = Abilities.Dolorean;
                break;
            case "Grip":
                Grip(ability, true);
                currentAbilityEngine = Abilities.Grip;
                break;
            case "Nail":
                Nail(ability, true);
                currentAbilityTire = Abilities.Nail;
                break;
            case "Suspension":
                Suspension(ability, true);
                currentAbilityChassis = Abilities.Suspension;
                break;
            case "Swim":
                Swim(ability, true);
                currentAbilityChassis = Abilities.Swim;
                break;
            case "Turbo":
                Turbo(ability, true);
                currentAbilityEngine = Abilities.Turbo;
                break;
        }


    }

    public void StopAbility(Abilities abilities)
    {
        
        switch (abilities)
        {
            case Abilities.Spring:
                Spring(null, false);
                break;
            case Abilities.AutoGearbox:
                AutoGearbox(null, false);
                break;
            case Abilities.Bumper:
                Bumper(null, false);
                break;
            case Abilities.Dolorean:
                Dolorean(null, false);
                break;
            case Abilities.Grip:
                Grip(null, false);
                break;
            case Abilities.Nail:
                Nail(null, false);
                break;
            case Abilities.Suspension:
                Suspension(null, false);
                break;
            case Abilities.Swim:
                Swim(null, false);
                break;
            case Abilities.Turbo:
                Turbo(null, false);
                break;


        }
    }
    #region Trigger Ability
    public void ClickEngine()
    {
        TriggerAbility(abilityEngine.ability);

    }

    public void ClickTire()
    {
        TriggerAbility(abilityTire.ability);

    }


    public void ClickChassis()
    {
        TriggerAbility(abilityChassis.ability);
    }
    #endregion

    #region Engine Ability
    private void Grip(PieceAbility ability, bool enabled)//Done
    {
        if (enabled)
        {
            GripLaneChange = car.changingLaneSpeedLoss;
            ChunkManager.Instance.modulesToCrossEngine = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            car.changingLaneSpeedLoss -= GripLaneChange;
        }
        else
        {
            car.changingLaneSpeedLoss += GripLaneChange;
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            currentAbilityEngine = Abilities.Default;
        }
    }

    private void AutoGearbox(PieceAbility ability, bool enabled)//Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossEngine = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            car.engineAcceleration *= AutoGearBoxRatio;
        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            car.engineAcceleration /= AutoGearBoxRatio;
            currentAbilityEngine = Abilities.Default;
        }
    }

    private void Turbo(PieceAbility ability, bool enabled)//Done
    {

        if (enabled)
        {

            ChunkManager.Instance.modulesToCrossEngine = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;

            ChunkManager.Instance.speedActu = car.engineMaxSpeed * turboMultiplier;

        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            currentAbilityEngine = Abilities.Default;
        }

    }
    #endregion

    #region Tire Ability
    private void Spring(PieceAbility ability, bool enabled)//Done
    {

        if (enabled)
        {

            ChunkManager.Instance.modulesToCrossTire = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + jumpHeight);
        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - jumpHeight);
            car.CarLanding();
            currentAbilityTire = Abilities.Default;
        }
    }

    private void Dolorean(PieceAbility ability, bool enabled)//Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossTire = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbilityTire = Abilities.Default;
        }
    }

    private void Nail(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossTire = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbilityTire = Abilities.Default;
        }
    }
    #endregion

    #region Chassis Ability
    private void Bumper(PieceAbility ability, bool enabled) //Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossChassis = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbilityChassis = Abilities.Default;
        }
    }

    private void Swim(PieceAbility ability, bool enabled) //Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossChassis = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbilityChassis = Abilities.Default;
        }
    }

    private void Suspension(PieceAbility ability, bool enabled)//Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossChassis = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbilityChassis = Abilities.Default;
        }
    }
    #endregion
}
