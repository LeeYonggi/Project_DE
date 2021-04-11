using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnitHpBar : MonoBehaviour
{
    [SerializeField] Image blendImage = null;
    private Character parentChar = null;
    private Slider slider = null;
    
    private float maxHp = default;
    private float curHp = default;


    private Tween blendingHpBarImage = null;

    
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
        blendingHpBarImage?.Kill();

        blendImage.color = new Color(1, 1, 1, 0.4f);
        blendingHpBarImage = blendImage.DOColor(new Color(1, 1, 1, 0), 0.5f).OnComplete(() => {
            blendingHpBarImage = null;
        });
    }

    
}
