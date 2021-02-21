using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct AssetBundleData
{
    public bool isDontDestroySceneLoad;

    public event Action onComplete;

    public void OnComplete()
    {
        onComplete?.Invoke();
    }

    public AssetBundleData(bool isDontDestroySceneLoad)
    {
        this.isDontDestroySceneLoad = isDontDestroySceneLoad;
        onComplete = null;
    }

}

public class AssetBundleLoader
{
    private AssetBundle assetBundle = null;

    public AssetBundleData assetBundleData = new AssetBundleData(false);
    public AssetBundle AssetBundle { get => assetBundle; set => assetBundle = value; }

    public AssetBundleLoader(AssetBundleData assetBundleData)
    {
        this.assetBundleData = assetBundleData;
    }
}

