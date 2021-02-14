using System;
using System.Collections;
using System.Collections.Generic;
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
    private Dictionary<string, AssetBundle> assetBundleDct = new Dictionary<string, AssetBundle>();

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

    public void Release()
    {
        
    }

    public void Initialize()
    {

    }

    public void AssetBundleLoad(string bundleName)
    {
        loadingBundleList.Add(bundleName);

        if(loadingBundleList.Count == 1)
        {
            MainManager.instance.StartCoroutine(AssetBundleLoadCoroutine());
        }
    }

    public void AssetBundleLoad(List<string> bundleNameList)
    {
        loadingBundleList.AddRange(bundleNameList);

        MainManager.instance.StartCoroutine(AssetBundleLoadCoroutine());
    }

    private IEnumerator AssetBundleLoadCoroutine()
    {
        while(loadingBundleList.Count > 0)
        {
            string bundlePath = $"{Application.persistentDataPath}/Android/";

            var request = AssetBundle.LoadFromFileAsync(bundlePath + loadingBundleList[0]);

            yield return request;

            AssetBundle bundle = request.assetBundle;

            assetBundleDct.Add(loadingBundleList[0], bundle);

            if (loadingBundleList[0] == "loaddata")
                loadDataLoaded?.Invoke();

            loadingBundleList.RemoveAt(0);
        }

        yield break;
    }
}
