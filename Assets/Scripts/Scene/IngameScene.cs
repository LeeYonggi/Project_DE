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

        AssetBundleManager.Instance.AssetBundleLoad("prefab/ui/ingame", new AssetBundleData(false));
    }

    public void Start()
    {
        UIManager.Instance.CreateUIForm("prefab/ui/ingame", "IngameUIForm");
    }

    public void Destroy()
    {
    }

    public void FixedUpdate()
    {
    }

    public void LateUpdate()
    {
    }


    public void Update()
    {
    }
}
