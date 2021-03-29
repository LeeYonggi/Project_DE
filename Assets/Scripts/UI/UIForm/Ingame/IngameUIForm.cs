using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

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
            var charUISlot = CharacterUI_ArrangementSlot.Create(
                       UiObject.transform, UiObject.GetComponent<RectTransform>(),
                       new Vector2(-244 + 164 * i, 141), 
                       new CharacterSlotData(null, null));

            char_ArrSlots.Add(charUISlot);
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
