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
}
