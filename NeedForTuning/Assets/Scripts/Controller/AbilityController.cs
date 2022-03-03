using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [HideInInspector] public CarPiece abilityEngine;
    [HideInInspector] public CarPiece abilityTire;
    [HideInInspector] public CarPiece abilityChassis;

    private void TriggerAbility(PieceAbility ability)
    {
        switch (abilityEngine.ability.name)
        {
            case "Spring":
                Spring(ability);
                break;
            case "AutoGearbox":
                AutoGearbox(ability);
                break;
            case "Bumper":
                Bumper(ability);
                break;
            case "Dolorean":
                Dolorean(ability);
                break;
            case "Grip":
                Grip(ability);
                break;
            case "Nail":
                Nail(ability);
                break;
            case "Suspension":
                Suspension(ability);
                break;
            case "Swim":
                Swim(ability);
                break;
            case "Turbo":
                Turbo(ability);
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

    private void Turbo(PieceAbility ability)
    {

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
