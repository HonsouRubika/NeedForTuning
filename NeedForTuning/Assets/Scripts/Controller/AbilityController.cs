using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Images")]
    public Image engineButtonImage;
    public Image ChassisButtonImage;
    public Image TireButtonImage;

    [Header("Abilities")]
    public float turboMultiplier;
    public float jumpHeight;
    public float AutoGearBoxRatio;
    private float GripLaneChange;
    private int utilisationEngine;
    private int utilisationTire;
    private int utilisationChassis;
    private float cdEngine;
    private float cdTire;
    private float cdChassis;
    private bool engineRdy = true;
    private bool tireRdy = true;
    private bool chassisRdy = true;

    public Text abilityEngineButton;
    public Text abilityTireButton;
    public Text abilityChassisButton;

    private void Start()
    {
        car = GetComponent<CarController>();
        AttributePieces();
    }
    void AttributePieces()
    {
        abilityEngine = InventoryManager.Instance.engine;
        abilityTire = InventoryManager.Instance.tire;
        abilityChassis = InventoryManager.Instance.chassis;
        UpdateButtons();
    }

    void UpdateButtons()
    {
        abilityEngineButton.text = GameManager.Instance.car.GetComponent<AbilityController>().abilityEngine.ability.name;
        abilityTireButton.text = GameManager.Instance.car.GetComponent<AbilityController>().abilityTire.ability.name;
        abilityChassisButton.text = GameManager.Instance.car.GetComponent<AbilityController>().abilityChassis.ability.name;
    }
    private void Update()
    {
        ReloadCooldown();
    }

    private void ReloadCooldown()
    {
        if (!engineRdy)
        {
            if (cdEngine < abilityEngine.ability.cooldown)
            {
                cdEngine += Time.deltaTime;
                engineButtonImage.fillAmount = cdEngine / abilityEngine.ability.cooldown;
            }
            else
            {
                engineRdy = true;
                cdEngine = 0;
            }
        }

        if (!chassisRdy)
        {
            if (cdChassis < abilityEngine.ability.cooldown)
            {
                cdChassis += Time.deltaTime;
                ChassisButtonImage.fillAmount = cdChassis / abilityEngine.ability.cooldown;
            }
            else
            {
                chassisRdy = true;
                cdChassis = 0;
            }
        }

        if (!tireRdy)
        {
            if (cdTire < abilityEngine.ability.cooldown)
            {
                cdTire += Time.deltaTime;
                TireButtonImage.fillAmount = cdTire / abilityEngine.ability.cooldown;
            }
            else
            {
                tireRdy = true;
                cdTire = 0;
            }
        }
    }

    private void TriggerAbility(PieceAbility ability)
    {


        switch (ability.name)
        {
            case "Spring":
                Spring(ability, true);
                tireRdy = false;
                currentAbilityTire = Abilities.Spring;
                break;
            case "AutoGearbox":
                AutoGearbox(ability, true);
                engineRdy = false;
                currentAbilityEngine = Abilities.AutoGearbox;
                break;
            case "Bumper":
                Bumper(ability, true);
                chassisRdy = false;
                currentAbilityChassis = Abilities.Bumper;
                break;
            case "Dolorean":
                Dolorean(ability, true);
                tireRdy = false;
                currentAbilityTire = Abilities.Dolorean;
                break;
            case "Grip":
                Grip(ability, true);
                engineRdy = false;
                currentAbilityEngine = Abilities.Grip;
                break;
            case "Nail":
                Nail(ability, true);
                tireRdy = false;
                currentAbilityTire = Abilities.Nail;
                break;
            case "Suspension":
                Suspension(ability, true);
                chassisRdy = false;
                currentAbilityChassis = Abilities.Suspension;
                break;
            case "Swim":
                Swim(ability, true);
                chassisRdy = false;
                currentAbilityChassis = Abilities.Swim;
                break;
            case "Turbo":
                Turbo(ability, true);
                engineRdy = false;
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
        if (utilisationEngine < abilityEngine.ability.utilisationNumber && engineRdy)
        {
            utilisationEngine++;
            TriggerAbility(abilityEngine.ability);
        }
        else
        {
            engineButtonImage.color = Color.gray;
        }
        
    }

    public void ClickTire()
    {
        if (utilisationTire < abilityEngine.ability.utilisationNumber && tireRdy)
        {
            utilisationTire++;
            TriggerAbility(abilityTire.ability);
        }
            

    }


    public void ClickChassis()
    {
        if (utilisationChassis < abilityEngine.ability.utilisationNumber && chassisRdy)
        {
            Debug.Log("oogabooga");
            utilisationChassis++;
            TriggerAbility(abilityChassis.ability);
        }
            
    }
    #endregion

    #region Engine Ability
    private void Grip(PieceAbility ability, bool enabled)//Done
    {
        
        if (enabled)
        {
            GripLaneChange = car.changingLaneSpeedLossConcrete;
            ChunkManager.Instance.modulesToCrossEngine = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            car.changingLaneSpeedLossConcrete -= GripLaneChange;
        }
        else
        {
            car.changingLaneSpeedLossConcrete += GripLaneChange;
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            currentAbilityEngine = Abilities.Default;
        }
    }

    private void AutoGearbox(PieceAbility ability, bool enabled)//Done
    {
        if (enabled)
        {
            ChunkManager.Instance.modulesToCrossEngine = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            car.engineAccelerationConcrete *= AutoGearBoxRatio;
        }
        else
        {
            ChunkManager.Instance.speedActu = car.engineMaxSpeed;
            car.engineAccelerationConcrete /= AutoGearBoxRatio;
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
            car.rb.useGravity = false;
            car.isInvicible = true;
            ChunkManager.Instance.modulesToCrossTire = ChunkManager.Instance.totalNbOfLineActu + ability.moduleDistance;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + jumpHeight);
        }
        else
        {
            
            car.isInvicible = false;
            car.rb.useGravity = true;
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - jumpHeight);
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
        Debug.Log("RUNNING");
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
