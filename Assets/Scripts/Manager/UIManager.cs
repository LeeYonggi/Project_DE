using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Manager
{
    public class UIFormObject
    {
        internal bool isStart = false;
        internal UIForm uiForm = null;
    }

    public class UIManager : Singleton<UIManager>, BaseManager
    {
        private Dictionary<string, UIFormObject> uiForms = new Dictionary<string, UIFormObject>();

        private Camera currentCamera = null;

        float escapeDelay = 0.0f;

        public Camera CurrentCamera
        {
            get
            {
                if (currentCamera == null)
                    currentCamera = CreateOrFindUICamera();
                return currentCamera;
            }
            set => currentCamera = value;
        }

        public void Start()
        {
            if (currentCamera == null)
                currentCamera = CreateOrFindUICamera();


            SceneManager.Instance.sceneLoadEvent += Instance_sceneLoadEvent;
        }

        private void Instance_sceneLoadEvent(UnityEngine.SceneManagement.Scene arg1, UnityEngine.SceneManagement.LoadSceneMode arg2)
        {
            if (currentCamera == null)
                currentCamera = CreateOrFindUICamera();

            GameObject[] uiCameras = GameObject.FindGameObjectsWithTag("UICamera");

            for(int i = 0; i < uiCameras.Length; i++)
            {
                if(uiCameras[i] != CurrentCamera.gameObject)
                    GameObject.Destroy(uiCameras[i]);
            }

        }

        public void Destroy()
        {
            foreach(var uiFormObject in uiForms)
            {
                uiFormObject.Value.uiForm.Destroy();

                GameObject.Destroy(uiFormObject.Value.uiForm.UiObject);
            }

            uiForms.Clear();
        }

        public void Update()
        {
            foreach (var uiForm in uiForms)
            {
                if (uiForm.Value.uiForm.IsOpen == false)
                    continue;

                if (uiForm.Value.isStart == false)
                {
                    uiForm.Value.uiForm.Start();
                    uiForm.Value.isStart = true;
                }

                uiForm.Value.uiForm.Update();
            }
        }

        public void FixedUpdate()
        {
            foreach (var uiForm in uiForms)
            {
                if (uiForm.Value.uiForm.IsOpen == false)
                    continue;

                if (uiForm.Value.isStart == false)
                {
                    uiForm.Value.uiForm.Start();
                    uiForm.Value.isStart = true;
                }

                uiForm.Value.uiForm.FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            foreach (var uiForm in uiForms)
            {
                if (uiForm.Value.uiForm.IsOpen == false)
                    continue;

                if (uiForm.Value.isStart == false)
                {
                    uiForm.Value.uiForm.Start();
                    uiForm.Value.isStart = true;
                }

                uiForm.Value.uiForm.LateUpdate();
            }
        }

        private Camera CreateOrFindUICamera()
        {
            GameObject temp = GameObject.FindGameObjectWithTag("UICamera");

            if (temp == null)
            {
                var uiCameraPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/camera", "UICamera");
                temp = GameObject.Instantiate(uiCameraPrefab);
            }

            GameObject.DontDestroyOnLoad(temp);

            return temp.GetComponent<Camera>();
        }

        /// <summary>
        /// UIForm을 만든다
        /// </summary>
        /// <param name="prefabPath">UIForm Prefab Path</param>
        /// <param name="uiFormName">UIForm class Name(정확히 같아야 함)</param>
        public void CreateUIForm(string prefabPath, string uiFormName)
        {
            if (currentCamera == null)
                currentCamera = CreateOrFindUICamera();

            if (uiForms.ContainsKey(uiFormName) == true)
                return;

            GameObject windowPrefab = AssetBundleManager.Instance.GetAsset<GameObject>(prefabPath, uiFormName);

            Type type = Type.GetType(uiFormName);

            if (type == null)
                Debug.LogError($"{uiFormName} isn't classname or not inherited from uiform");

            UIFormObject uiFormObject = new UIFormObject();
            uiFormObject.uiForm = Activator.CreateInstance(type) as UIForm;

            uiForms.Add(uiFormName, uiFormObject);

            GameObject uiObject = GameObject.Instantiate(windowPrefab, currentCamera.transform);

            uiForms[uiFormName].uiForm.UiObject = uiObject;
            uiForms[uiFormName].uiForm.Awake();
        }
    }
}