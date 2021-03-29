using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class MainManager : MonoBehaviour
{
    private static MainManager instance = new MainManager();

    // 매니져 초기화가 끝났으면
    private bool isManagerInit = false;

    private readonly int fixSreenWidth = 720;
    private readonly int fixSreenHeight = 1280;

    public static MainManager Instance { get => instance; set => instance = value; }

    public int FixSreenWidth => fixSreenWidth;

    public int FixSreenHeight => fixSreenHeight;

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
        TitleScene.BeginLoad();
        StartCoroutine(WaitforAssetLoad());
    }


    void ManagerInitialize()
    {
        SceneManager.Instance.Start();
        UIManager.Instance.Start();
    }

    IEnumerator WaitforAssetLoad()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();

            if (AssetBundleManager.Instance.IsAssetBundleLoadComplete())
            {
                ManagerInitialize();

                isManagerInit = true;
                break;
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isManagerInit)
        {
            SceneManager.Instance.Update();
            UIManager.Instance.Update();
        }
    }

    private void FixedUpdate()
    {
        if(isManagerInit)
        {
            SceneManager.Instance.FixedUpdate();
            UIManager.Instance.FixedUpdate();
        }
    }

    private void LateUpdate()
    {
        if(isManagerInit)
        {
            SceneManager.Instance.LateUpdate();
            UIManager.Instance.LateUpdate();
        }
    }
}
