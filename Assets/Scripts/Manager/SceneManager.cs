using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using UnityScene = UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

namespace Manager
{
    public class SceneManager : Singleton<SceneManager>, BaseManager
    {
        public enum SCENE_KIND
        {
            TITLE,
            MAINMENUSCENE,
            INGAME
        }

        /// <summary>
        /// 씬 이름과 에셋번들 이름 저장 Dictionary
        /// </summary>
        private Dictionary<SCENE_KIND, string> sceneBundleNameDct = new Dictionary<SCENE_KIND, string>();

        /// <summary>
        /// 현재 씬 관리자
        /// </summary>
        private BaseScene currentScene = null;

        bool isSceneChanging = false;
        bool isCurrentSceneStart = false;

        public event Action<UnityScene.Scene, UnityScene.LoadSceneMode> sceneLoadEvent;

        public void Start()
        {
            UnityScene.SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            sceneBundleNameDct[SCENE_KIND.TITLE] = "";
            sceneBundleNameDct[SCENE_KIND.MAINMENUSCENE] = "";
            sceneBundleNameDct[SCENE_KIND.INGAME] = "scene/ingame";

            ChangeCurrentScene(SCENE_KIND.TITLE);

            CurrentSceneStart();
        }

        /// <summary>
        /// 씬 로드 시 호출 함수
        /// </summary>
        /// <param name="arg0">바뀔 씬</param>
        /// <param name="arg1">씬 모드</param>
        private void SceneManager_sceneLoaded(UnityScene.Scene arg0, UnityScene.LoadSceneMode arg1)
        {
            isSceneChanging = false;

            sceneLoadEvent?.Invoke(arg0, arg1);
        }

        public void Destroy()
        {
            currentScene.Destroy();
        }

        public void FixedUpdate()
        {
            if(isCurrentSceneStart)
                currentScene.FixedUpdate();
        }

        public void LateUpdate()
        {
            if(isCurrentSceneStart)
                currentScene.LateUpdate();
        }

        public void Update()
        {
            if(isCurrentSceneStart)
                currentScene.Update();
        }

        public void ChangeScene(SCENE_KIND scene, bool isSceneChangeEffect = true)
        {
            if (isSceneChanging == true)
                return;

            currentScene.Destroy();

            AssetBundleData assetBundleData = new AssetBundleData(false);

            assetBundleData.onComplete += () =>
            {
                MainManager.Instance.StartCoroutine(LoadSceneAsync(scene));
            };

            AssetBundleManager.Instance.AssetBundleLoad(sceneBundleNameDct[scene], assetBundleData);

            isSceneChanging = true;
        }

        IEnumerator LoadSceneAsync(SCENE_KIND scene)
        {
            AsyncOperation asyncOper = UnityScene.SceneManager.LoadSceneAsync(
                AssetBundleManager.Instance.GetBundleSceneName(sceneBundleNameDct[scene], scene.ToString())
                );

            ChangeCurrentScene(scene);

            while(AssetBundleManager.Instance.IsAssetBundleLoadComplete() == false
                 || asyncOper.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
            CurrentSceneStart();

            yield return null;
        }

        private void ChangeCurrentScene(SCENE_KIND sceneKind)
        {
            switch (sceneKind)
            {
                case SCENE_KIND.TITLE:
                    currentScene = TitleScene.Instance;
                    break;
                case SCENE_KIND.MAINMENUSCENE:
                    break;
                case SCENE_KIND.INGAME:
                    currentScene = IngameScene.Instance;
                    break;
            }

            isCurrentSceneStart = false;
            currentScene.Load();
        }

        private void CurrentSceneStart()
        {
            currentScene.Start();

            isCurrentSceneStart = true;
        }
    }
}