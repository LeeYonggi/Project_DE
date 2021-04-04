using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : CharacterBaseState
{
    private float attackDelay = 0.0f;
    private bool isAttack = false;
    private bool isAttackEffect = false;

    private GameObject slashEffect = null;

    public override void Start(Character character)
    {
        attackDelay = character.Statistics.AttackSpeed;
        isAttack = false;

        Vector3 targetVec3 = character.TargetCharacter.transform.position - character.transform.position;

        targetVec3.y = 0;
        character.transform.rotation = Quaternion.LookRotation(targetVec3.normalized);
        character.Model.transform.localRotation = Quaternion.Euler(0, 70, 0);

        character.CharacterAnimator.SetBool("isAttack", true);
        character.CharacterAnimator.SetFloat("attackSpeed", character.Statistics.AttackSpeed);

        isAttackEffect = false;
    }

    public override void Update(Character character)
    {
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("ackshAttack") == false)
            return;

        //if(attackDelay >= 0.2f && isAttackEffect == false)
        //{
        //    GameObject effectPrefab = null;

        //    effectPrefab = Resources.Load<GameObject>("SwordSlashThickBlue");
        //    slashEffect = GameObject.Instantiate(effectPrefab, character.transform);

        //    slashEffect.transform.localPosition = new Vector3(-0.016f, 1.7f, 0.845f);
        //    slashEffect.transform.localRotation = Quaternion.Euler(new Vector3(33, 47, -55));
        //    isAttackEffect = true;
        //}

        attackDelay = character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (attackDelay >= 0.5f && isAttack == false)
        {
            character.TargetCharacter.ComeUnderAttack(character.Statistics);
            isAttack = true;
        }
        else if (attackDelay >= 0.99f)
        {
            character.CharacterAnimator.SetBool("isAttack", false);
            character.ChangeCurrentState(Character.Behaviour_State.IDLE_STATE);
        }
    }

    public override void End(Character character)
    {
        character.Model.transform.localPosition = Vector3.zero;
        character.Model.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
