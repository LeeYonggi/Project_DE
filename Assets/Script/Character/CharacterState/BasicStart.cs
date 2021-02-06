using UnityEngine;
using System;
using System.Collections;

public class BasicStart : CharacterBaseState
{
    public override void Start(Character character)
    {
        character.StartCoroutine(WaitCharacterStart(character));
    }

    public override void Update(Character character)
    {
    }

    public override void End(Character character)
    {
    }

    // 생성 시간 기다림
    IEnumerator WaitCharacterStart(Character character)
    {
        float startTime = character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(startTime);

        character.ChangeCurrentState(Character.Behaviour_State.IDLE_STATE);
    }
}
