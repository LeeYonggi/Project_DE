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

    public override void Awake()
    {
        base.Awake();

        uiCanvas = UiObject.GetComponent<Canvas>();

        uiCanvas.worldCamera = UIManager.Instance.CurrentCamera;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
