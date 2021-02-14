using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = new MainManager();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TempBundleManager());
    }

    IEnumerator TempBundleManager()
    {
        var bundleManager = new AssetBundles.AssetBundleManager();
        bundleManager.UseStreamingAssetsFolder();
        var initializeAsync = bundleManager.InitializeAsync();

        yield return initializeAsync;

        if (initializeAsync.Success)
        {
            AssetBundles.AssetBundleAsync bundle = bundleManager.GetBundleAsync("prefab/background/environment1");
            yield return bundle;

            if(bundle.AssetBundle)
            {
                bundleManager.UnloadBundle(bundle.AssetBundle);
            }
            else
            {
                Debug.LogError("Error initializing AssetBundleManager.");
            }
        }

        bundleManager.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
