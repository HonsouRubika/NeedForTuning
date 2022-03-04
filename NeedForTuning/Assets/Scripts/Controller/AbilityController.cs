using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities {Default,Spring,AutoGearbox,Dolorean,Grip,Nail,Swim,Suspension,Turbo,Bumper}
public class AbilityController : MonoBehaviour
{
    private CarController car;
    /*[HideInInspector]*/ public CarPiece abilityEngine;
    /*[HideInInspector]*/ public CarPiece abilityTire;
    /*[HideInInspector]*/ public CarPiece abilityChassis;
    public Abilities currentAbility;

    [Header ("Abilities")]
    public float turboMultiplier;
    public float jumpHeight;

    private void Start()
    {
        car = GetComponent<CarController>();
    }
    
    private void TriggerAbility(PieceAbility ability)
    {
        if (currentAbility == Abilities.Default)
        {
            switch (ability.name)
            {
                case "Spring":
                    Spring(ability, true);
                    currentAbility = Abilities.Spring;
                    break;
                case "AutoGearbox":
                    AutoGearbox(ability, true);
                    currentAbility = Abilities.AutoGearbox;
                    break;
                case "Bumper":
                    Bumper(ability, true);
                    currentAbility = Abilities.Bumper;
                    break;
                case "Dolorean":
                    Dolorean(ability, true);
                    currentAbility = Abilities.Dolorean;
                    break;
                case "Grip":
                    Grip(ability, true);
                    currentAbility = Abilities.Grip;
                    break;
                case "Nail":
                    Nail(ability, true);
                    currentAbility = Abilities.Nail;
                    break;
                case "Suspension":
                    Suspension(ability, true);
                    currentAbility = Abilities.Suspension;
                    break;
                case "Swim":
                    Swim(ability, true);
                    currentAbility = Abilities.Swim;
                    break;
                case "Turbo":
                    Turbo(ability, true);
                    currentAbility = Abilities.Turbo;
                    break;
            }
        }
        
    }
    
    public void StopAbility()
    {
        switch (currentAbility)
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
                Turbo(null,false);
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
    private void Grip(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            currentAbility = Abilities.Default;
        }
    }

    private void AutoGearbox(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            currentAbility = Abilities.Default;
        }
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
            currentAbility = Abilities.Default;
        }
        
    }
    #endregion

    #region Tire Ability
    private void Spring(PieceAbility ability, bool enabled)
    {
        
        if (enabled)
        {
            
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + jumpHeight);
        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - jumpHeight);
            currentAbility = Abilities.Default;
        }
    }

    private void Dolorean(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbility = Abilities.Default;
        }
    }

    private void Nail(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbility = Abilities.Default;
        }
    }
    #endregion

    #region Chassis Ability
    private void Bumper(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbility = Abilities.Default;
        }
    }

    private void Swim(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbility = Abilities.Default;
        }
    }

    private void Suspension(PieceAbility ability, bool enabled)
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCross = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
        }
        else
        {
            currentAbility = Abilities.Default;
        }
    }
    #endregion
}
