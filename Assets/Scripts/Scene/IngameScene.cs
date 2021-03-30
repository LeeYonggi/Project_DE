using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngameScene : Singleton<IngameScene>, BaseScene
{
    // 배치불가지역 효과
    private GameObject unplaceableArea = null;
    // 배치가능지역 충돌처리
    private List<GameObject> placeColliderList = new List<GameObject>();
    // 배치가능지역 좌표
    private List<Vector3> placePointList = new List<Vector3>();

    Camera ingameMainCamera = null;

    private Dictionary<string, GameObject> deployEffectList = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> fieldObjectPrefabs = new Dictionary<string, GameObject>();

    private List<GameObject> objectPredictionList = new List<GameObject>();

    private List<GameObject> activeFieldObjects = new List<GameObject>();

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

        // 배치관련 오브젝트
        GameObject unplaceableAreaPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/effect/play", "UnplaceableArea");
        GameObject placeableColiiderPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/effect/play", "PlaceableCollider");

        unplaceableArea = GameObject.Instantiate(unplaceableAreaPrefab);

        for (int i = 0; i < 3; i++)
        {
            placeColliderList.Add(GameObject.Instantiate(placeableColiiderPrefab));
            placeColliderList[i].transform.position = new Vector3(-6 + i * 6, 0.13f, -1.61f);
            placePointList.Add(new Vector3(-6 + i * 6, 0.0f, -9.2f));
        }

        ingameMainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();

        unplaceableArea.SetActive(false);

        fieldObjectPrefabs.Add("BraveKnight", AssetBundleManager.Instance.GetAsset<GameObject>("prefab/character/knight/bravekinght", "BraveKnight"));

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

            MeshRenderer[] meshRenderers = obj.Value.GetComponentsInChildren<MeshRenderer>();

            for(int i = 0; i < meshRenderers.Length; i++)
            {
                string currentMatName = meshRenderers[i].sharedMaterial.name;
                Material transparentMat = AssetBundleManager.Instance.GetAsset<Material>("materials/transparent", $"{currentMatName}_Transparent");

                meshRenderers[i].sharedMaterial = transparentMat;
            }

            SkinnedMeshRenderer[] skinnedMeshRenderers = obj.Value.GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                string currentMatName = skinnedMeshRenderers[i].sharedMaterial.name;
                Material transparentMat = AssetBundleManager.Instance.GetAsset<Material>("materials/transparent", $"{currentMatName}_Transparent");

                skinnedMeshRenderers[i].sharedMaterial = transparentMat;
            }
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

    public Vector3 GetPlacePoint(GameObject hitPlaceCollider)
    {
        for(int i = 0; i < placeColliderList.Count; i++)
        {
            if(placeColliderList[i] == hitPlaceCollider)
            {
                return placePointList[i];
            }
        }
        return Vector3.zero;
    }

    public void CreateCharacter(CharacterSlotData characterSlotData, Vector3 createPos)
    {
        GameObject characterObject = GameObject.Instantiate(fieldObjectPrefabs[characterSlotData.characterStatistics.Name]);

        characterObject.transform.position = createPos;

        activeFieldObjects.Add(characterObject);
    }
}
