﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Ability abilityOne;
    public Ability abilityTwo;
    public Ability abilityThree;
    public Ability abilityFour;

    public Image abilityOneIcon;
    public Image abilityTwoIcon;
    public Image abilityThreeIcon;
    public Image abilityFourIcon;

    public Animator anim;

    private void Start()
    {
        UpdateAbilityIcons();
    }

    public void NewAbility(Ability newAbility, int abilitySlot)
    {
        switch(abilitySlot)
        {
            case 1:
                abilityOne = newAbility;
                break;
            case 2:
                abilityTwo = newAbility;
                break;
            case 3:
                abilityThree = newAbility;
                break;
            case 4:
                abilityFour = newAbility;
                break;
        }

        UpdateAbilityIcons();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!abilityOne.onCooldown)
            {
                anim.SetTrigger("Ability1");
                abilityOne.ActivateAbility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!abilityTwo.onCooldown)
            {
                anim.SetTrigger("Ability2");
                abilityTwo.ActivateAbility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!abilityThree.onCooldown)
            {
                anim.SetTrigger("Ability3");
                abilityThree.ActivateAbility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!abilityFour.onCooldown)
            {
                anim.SetTrigger("Ability4");
                abilityFour.ActivateAbility();
            }
        }
    }

    void UpdateAbilityIcons()
    {
        abilityOneIcon.sprite = abilityOne.abilityIcon;
        abilityTwoIcon.sprite = abilityTwo.abilityIcon;
        abilityThreeIcon.sprite = abilityThree.abilityIcon;
        abilityFourIcon.sprite = abilityFour.abilityIcon;
    }
}
