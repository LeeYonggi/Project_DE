using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngameScene : Singleton<IngameScene>, BaseScene
{
    // ��ġ�Ұ����� ȿ��
    GameObject unplaceableArea = null;

    List<GameObject> placeColliderList = new List<GameObject>();

    Camera ingameMainCamera = null;

    private List<GameObject> objectPredictionList = new List<GameObject>();

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
        GameObject placeableColiiderPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/effect/play", "PlaceableCollider");

        unplaceableArea = GameObject.Instantiate(unplaceableAreaPrefab);

        for (int i = 0; i < 3; i++)
        {
            placeColliderList.Add(GameObject.Instantiate(placeableColiiderPrefab));
            placeColliderList[i].transform.position = new Vector3(-6 + i * 6, 0.13f, -1.61f);
        }

        ingameMainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();

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

    /// <summary>
    /// �ΰ��� ī�޶󿡼� ���콺��ǥ�� ���ִ� Raycast�� ��� ��ȯ
    /// </summary>
    /// <returns>Raycast�� ���� ��ü�� ��ȯ</returns>
    public RaycastHit[] CameraMousePointRaycast()
    {
        Ray ray = ingameMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        return hit;
    }
}
