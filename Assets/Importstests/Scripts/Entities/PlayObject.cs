using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayObject : MonoBehaviour
{
    bool isUpdating = false;

    protected virtual void Awake()
    {
    
    }
    protected virtual void Start()
    {
        GameManager.instance.AddPlayObject(this);
    }

    public virtual void Initialize()
    {
        isUpdating = true;
        
        OnInitialize();
    }
    public virtual void DeInitialize()
    {
        isUpdating = false;
        GameManager.instance.RemovePlayObject(this);
        OnDeInitialize();
    }
    public void Pause()
    {
        isUpdating = false;
        OnPause();
    }
    public void Resume()
    {
        isUpdating = true;
        OnResume();
    }

    void Update()
    {
        if(isUpdating)
        {
            OnUpdate();
        }
    }

    void FixedUpdate()
    {
        if (isUpdating)
        {
            OnFixedUpdate();
        }
    }

    protected abstract void OnInitialize();
    protected abstract void OnDeInitialize();

    protected abstract void OnPause();
    protected abstract void OnResume();
    protected abstract void OnUpdate();
    protected abstract void OnFixedUpdate();
}
