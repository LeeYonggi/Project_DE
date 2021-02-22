using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngameScene : Singleton<TitleScene>, BaseScene
{
    public void Load()
    {
        AssetBundleData bundle = new AssetBundleData(false);
        
        bundle.onComplete += () => {
            GameObject.Instantiate(AssetBundleManager.Instance.GetAsset<GameObject>("prefab/background/environment1", "Environment1"));
        };

        AssetBundleManager.Instance.AssetBundleLoad("prefab/background/environment1", bundle);
    }

    public void Start()
    {
        IngameManager.Instance.Start();
    }

    public void Destroy()
    {
        IngameManager.Instance.Destroy();
    }

    public void Update()
    {
        IngameManager.Instance.Update();
    }

    public void FixedUpdate()
    {
        IngameManager.Instance.FixedUpdate();
    }

    public void LateUpdate()
    {
        IngameManager.Instance.LateUpdate();
    }
    
}
