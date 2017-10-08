﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBreath : Ability
{
    public override void ActivateAbility()
    {
        StartCoroutine(ActivateFireBreath());
    }

    IEnumerator ActivateFireBreath()
    {
        onCooldown = true;

        PlayerMovement.canMove = false;
        yield return new WaitForSeconds(2f);
        PlayerMovement.canMove = true;

        StartCoroutine(Cooldown());
    }
}
