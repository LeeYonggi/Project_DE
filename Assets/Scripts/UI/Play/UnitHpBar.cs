using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpBar : MonoBehaviour
{
    private Character parentChar = null;
    private Slider slider = null;
    
    private float maxHp = default;
    private float curHp = default;
    
    public void InitHpBar(Character parent)
    {
        parentChar = parent;
        curHp = maxHp = parent.Statistics.Hp;
        slider = transform.GetComponent<Slider>();
        slider.value = slider.maxValue = maxHp;
        slider.minValue = 0;
    }

    public void ChangeCurHp(float changeHp)
    {
        slider.value = curHp = changeHp;
    }
}
