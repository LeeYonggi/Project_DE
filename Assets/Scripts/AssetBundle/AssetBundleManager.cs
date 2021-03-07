using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.Events;

public class AssetBundleManager : Singleton<AssetBundleManager>
{
    /// <summary>
    /// 에셋번들 저장 테이블
    /// </summary>
    private Dictionary<string, AssetBundleLoader> assetBundleDct = new Dictionary<string, AssetBundleLoader>();

    /// <summary>
    /// 로드중인 번들 리스트
    /// </summary>
    private List<string> loadingBundleList = new List<string>();

    /// <summary>
    /// 씬 변경에 영향을 받지 않는 번들 리스트
    /// </summary>
    private List<string> unAffectedSceneChangeBundleList = new List<string>();

    /// <summary>
    /// loaddata 로드 시 발생하는 이벤트
    /// </summary>
    private UnityEvent loadDataLoaded = new UnityEvent();


    private readonly string aosDownloadPath = "/Android/";
    private readonly string windowDownloadPath = "/Window/";
    private readonly string iosDownloadPath = "/IOS/";


    public void Initialize()
    {

    }

    /// <summary>
    /// 에셋번들 로드
    /// </summary>
    /// <param name="bundleName">에셋번들 이름 ex.)"prefab/background/background_public"</param>
    /// <param name="assetBundleData">AssetBundle custom data</param>
    public void AssetBundleLoad(string bundleName, AssetBundleData assetBundleData)
    {
        if(assetBundleDct.ContainsKey(bundleName))
        {
            assetBundleDct[bundleName].assetBundleData = assetBundleData;
            return;
        }

        assetBundleDct.Add(bundleName, new AssetBundleLoader(assetBundleData));
        loadingBundleList.Add(bundleName);

        if(loadingBundleList.Count == 1)
        {
            MainManager.Instance.StartCoroutine(AssetBundleLoadCoroutine());
        }
    }

    /// <summary>
    /// 에셋번들 로드 묶어서 처리
    /// </summary>
    /// <param name="bundleDataList">bundleName과 AssetBundleData를 묶은 리스트</param>
    public void AssetBundleLoad(List<KeyValuePair<string, AssetBundleData>> bundleDataList)
    {
        for (int i = 0; i < bundleDataList.Count; i++)
            AssetBundleLoad(bundleDataList[i].Key, bundleDataList[i].Value);
    }

    /// <summary>
    /// 에셋번들을 로드하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator AssetBundleLoadCoroutine()
    {
        while(loadingBundleList.Count > 0)
        {
#if UNITY_EDITOR
            string bundlePath = $"{Application.streamingAssetsPath}{windowDownloadPath}";
#elif UNITY_ANDROID
            string bundlePath = $"{Application.persistentDataPath}{aosDownloadPath}";
#elif UNITY_IOS || UNITY_IPHONE
            string bundlePath = $"{Application.persistentDataPath}{iosDownloadPath}";
#else
            string bundlePath = $"{Application.streamingAssetsPath}{windowDownloadPath}";
#endif

            var request = AssetBundle.LoadFromFileAsync(bundlePath + loadingBundleList[0]);

            yield return request;

            AssetBundle bundle = request.assetBundle;

            assetBundleDct[loadingBundleList[0]].AssetBundle = bundle;
            assetBundleDct[loadingBundleList[0]].assetBundleData.OnComplete();

            loadingBundleList.RemoveAt(0);
        }

        yield break;
    }

    public T GetAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
        if (assetBundleDct.ContainsKey(bundleName))
        {
            T temp = null;

            try
            {
                temp = assetBundleDct[bundleName].AssetBundle.LoadAsset<T>(assetName);
            }
            catch (Exception e)
            {
                Debug.LogError($"{bundleName}: {e.Message}");
            }
            return temp;
        }
        else
        {
            Debug.LogError($"AssetBundle: {bundleName} is not found. Please load after assetbundle loaded");

            return null;
        }
    }

    public string GetBundleSceneName(string bundleName, string sceneName)
    {
        if(assetBundleDct.ContainsKey(bundleName) == false)
        {
            throw new Exception($"{bundleName} is not include in dictionary");
        }

        string[] scenePaths = assetBundleDct[bundleName].AssetBundle.GetAllScenePaths();
        string name = Path.GetFileNameWithoutExtension(scenePaths[0]);

        return name;
    }

    /// <summary>
    /// 애셋번들 로드가 완료 되었는가.
    /// </summary>
    /// <returns></returns>
    public bool IsAssetBundleLoadComplete()
    {
        return loadingBundleList.Count == 0;
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
