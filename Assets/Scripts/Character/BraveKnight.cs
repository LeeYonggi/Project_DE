using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveKnight : Character
{
    // Start is called before the first frame update
    void Start()
    {
        base.CharacterStart(new CharacterStatistics("BraveKnight", 100, 6, 10, 2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        base.ObjectUpdate();
    }

    protected override void CharacterAnimation()
    {
        stateDic[Behaviour_State.ATTACK_STATE] = new KnightAttack();
        stateDic[Behaviour_State.MOVE_STATE] = new KnightMove();
    }
}
