using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDeath : CharacterBaseState
{
    public override void Start(Character character)
    {
        character.CharacterAnimator.SetBool("isDestroy", true);
        character.CharacterAnimator.SetFloat("deathSpeed", character.Statistics.DeathSpeed);
    }

    public override void Update(Character character)
    {
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
            return;
        
        
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            character.CharacterAnimator.SetBool("isDestroy", false);
            character.ChangeCurrentState(Character.Behaviour_State.IDLE_STATE);
        }
    }
    public override void End(Character character)
    {
        character.DestroyCharacter();
    }
}
