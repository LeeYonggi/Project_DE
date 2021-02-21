using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

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
        AssetBundleManager.Instance.Initialize();
        SceneManager.Instance.Start();
        //UIManager.Instance.Start();
    }

    IEnumerator TempBundleManager()
    {
        var bundleManager = new AssetBundles.AssetBundleManager();
        bundleManager.UseStreamingAssetsFolder();
        var initializeAsync = bundleManager.InitializeAsync();

        yield return initializeAsync;

        if (initializeAsync.Success)
        {
            AssetBundles.AssetBundleAsync bundle2 = bundleManager.GetBundleAsync("prefab/background/background_public");
            AssetBundles.AssetBundleAsync bundle = bundleManager.GetBundleAsync("prefab/background/environment1");
            yield return bundle2;
            yield return bundle;

            if(bundle.AssetBundle)
            {
                Instantiate(bundle.AssetBundle.LoadAsset<GameObject>("environment1"));
                //bundleManager.UnloadBundle(bundle.AssetBundle);
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
        SceneManager.Instance.Update();
        UIManager.Instance.Update();
    }

    private void FixedUpdate()
    {
        SceneManager.Instance.FixedUpdate();
        UIManager.Instance.FixedUpdate();
    }

    private void LateUpdate()
    {
        SceneManager.Instance.LateUpdate();
        UIManager.Instance.LateUpdate();
    }
}
