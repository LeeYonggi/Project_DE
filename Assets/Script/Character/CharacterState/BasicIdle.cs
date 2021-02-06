using UnityEngine;

// 기본 가만히 있는 상태
public class BasicIdle : CharacterBaseState
{
    public override void Start(Character character)
    {
        CheckAndChangeMoveState(character);
    }

    public override void Update(Character character)
    {
        CheckAndChangeMoveState(character);
    }

    private void CheckAndChangeMoveState(Character character)
    {
        if (Mathf.Abs(character.GetMoveMagnitude()) != 0.0f)
            character.ChangeCurrentState(Character.Behaviour_State.MOVE_STATE);

        character.CharacterAnimator.SetFloat("moveMent", character.GetMoveMagnitude());
    }

    public override void End(Character character)
    {
    }
}

