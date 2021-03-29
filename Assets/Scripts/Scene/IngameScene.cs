using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngameScene : Singleton<IngameScene>, BaseScene
{
    // 배치불가지역 효과
    GameObject unplaceableArea = null;
    // 배치가능지역 충돌처리
    List<GameObject> placeColliderList = new List<GameObject>();

    Camera ingameMainCamera = null;

    private Dictionary<string, GameObject> deployEffectList = new Dictionary<string, GameObject>();
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

        AssetBundleManager.Instance.AssetBundleLoad("prefab/character/knight/bravekinght", new AssetBundleData(false));
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

        DeployEffectInit();
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
    /// 인게임 카메라에서 마우스좌표로 쏴주는 Raycast의 결과 반환
    /// </summary>
    /// <returns>Raycast로 맞은 물체들 반환</returns>
    public RaycastHit[] CameraMousePointRaycast()
    {
        Ray ray = ingameMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        return hit;
    }

    /// <summary>
    /// 배치효과 오브젝트들 초기화
    /// </summary>
    private void DeployEffectInit()
    {
        var prefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/character/knight/bravekinght", "BraveKnight_Model");
        
        deployEffectList.Add("BraveKnight", GameObject.Instantiate(prefab));

        foreach(var obj in deployEffectList)
        {
            obj.Value.SetActive(false);
        }
    }

    public void SetDeployEffectPos(string key, Vector3 pos)
    {
        deployEffectList[key].transform.position = pos;
    }

    public void SetDeployEffectActive(string key, bool param)
    {
        deployEffectList[key].SetActive(param);
    }
}
