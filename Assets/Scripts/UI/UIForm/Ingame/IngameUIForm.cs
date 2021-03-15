using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IngameUIForm : UIForm
{
    Canvas uiCanvas = null;

    GameObject char_ArrSlotPrefab = null;
    List<CharacterUI_ArrangementSlot> char_ArrSlots = new List<CharacterUI_ArrangementSlot>();

    public override void Awake()
    {
        base.Awake();

        uiCanvas = UiObject.GetComponent<Canvas>();

        uiCanvas.worldCamera = UIManager.Instance.CurrentCamera;

        char_ArrSlotPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/ui/ingame", "Character_ArrangementSlot");

        for (int i = 0; i < 4; i++)
        {
            var tempObj = GameObject.Instantiate(char_ArrSlotPrefab, UiObject.transform);
            var char_ArrSlot = tempObj.GetComponent<CharacterUI_ArrangementSlot>();

            tempObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-244 + 164 * i, 141);
            char_ArrSlot.UiFormRectTransform = UiObject.GetComponent<RectTransform>();

            char_ArrSlots.Add(tempObj.GetComponent<CharacterUI_ArrangementSlot>());
        }
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
