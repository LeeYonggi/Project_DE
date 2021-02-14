using System;
using UnityEngine;

public abstract class UIForm
{
    protected GameObject uiObject = null;

    public event Action OpenEvent = null;
    public event Action CloseEvent = null;

    public bool IsOpen { get => uiObject.activeSelf; }

    public virtual void Start()
    {

    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Destroy()
    {
        if (uiObject != null)
        {
            GameObject.Destroy(uiObject);
        }
    }

    public virtual void OpenForm()
    {
        if (uiObject)
            uiObject.SetActive(true);

        OpenEvent?.Invoke();
    }

    public virtual void CloseForm()
    {
        if (uiObject)
            uiObject.SetActive(false);

        CloseEvent?.Invoke();
    }

    protected void AddUIObjectFormPacks(string name)
    {
        
    }
}

