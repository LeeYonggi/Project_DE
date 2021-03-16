using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngameScene : Singleton<IngameScene>, BaseScene
{
    // 배치불가지역 효과
    GameObject unplaceableArea = null;

    public void Load()
    {
        AssetBundleData bundle = new AssetBundleData(false);
        
        bundle.onComplete += () => {
            GameObject.Instantiate(AssetBundleManager.Instance.GetAsset<GameObject>("prefab/background/environment1", "Environment1"));
        };

        AssetBundleManager.Instance.AssetBundleLoad("prefab/background/environment1", bundle);

        AssetBundleManager.Instance.AssetBundleLoad("prefab/ui/ingame", new AssetBundleData(false));

        AssetBundleManager.Instance.AssetBundleLoad("prefab/effect/play", new AssetBundleData(false));
    }

    public void Start()
    {
        UIManager.Instance.CreateUIForm("prefab/ui/ingame", "IngameUIForm");

        GameObject unplaceableAreaPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/effect/play", "UnplaceableArea");

        unplaceableArea = GameObject.Instantiate(unplaceableAreaPrefab);

        unplaceableArea.SetActive(false);
    }

    public void Destroy()
    {
        DestroyInstance();
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


    public void PlaceObjforCard(bool active)
    {
        if(unplaceableArea)
            unplaceableArea.SetActive(active);
    }
}
