using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class UIManager : Singleton<UIManager>, BaseManager
{
    private Dictionary<string, UIForm> uiForms = new Dictionary<string, UIForm>();
    private Dictionary<string, GameObject> objectPacks = new Dictionary<string, GameObject>();

    private GameObject parent = null;
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
    }

    public void Destroy()
    {
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }

    public void LateUpdate()
    {
    }

    private Camera CreateOrFindUICamera()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("UICamera");

        if (temp == null)
        {

        }

        return temp.GetComponent<Camera>();
    }
}
