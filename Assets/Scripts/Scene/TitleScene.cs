using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class TitleScene : Singleton<TitleScene>, BaseScene
{
    public void Destroy()
    {
    }

    public void FixedUpdate()
    {
    }

    public void LateUpdate()
    {
    }

    // Start is called before the first frame update
    public void Start()
    {
        SceneManager.Instance.ChangeScene(SceneManager.SCENE_KIND.INGAME);
    }

    public void Update()
    {
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
