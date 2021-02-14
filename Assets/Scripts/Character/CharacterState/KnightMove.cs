using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KnightMove : CharacterBaseState
{
    public override void Start(Character character)
    {
     
    }

    public override void Update(Character character)
    {
        Vector3 nextPosition = character.transform.position + character.MoveVector * Time.deltaTime * character.Statistics.Speed;

        character.MRigidbody.MovePosition(nextPosition);

        character.CharacterAnimator.SetFloat("moveMent", character.GetMoveMagnitude());
    }

    public override void End(Character character)
    {

    }
}
