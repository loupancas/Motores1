using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffItem : MonoBehaviour
{
    [SerializeField] string id = "default";
    public string ID { get { return id; } }

    bool isActive = false;
    [SerializeField] bool autoTime = false;
    float timer;
    [SerializeField] float time_to_end = 2f;
    Action<string> OnFinish;

    protected virtual void Start()
    {
        //si se necesita overridear
    }

    public void SUbscribeToFInish(Action<string> _OnFinish)
    {
        OnFinish = _OnFinish;
    }

    public void Begin()
    {
        isActive = true;
        OnBegin();
    }
    public void End()
    {
        isActive = false;
        OnEnd();
    }
    private void Update()
    {
        if (isActive)
        {
            OnTick(Time.deltaTime);

            if (autoTime)
            {
                if (timer < time_to_end)
                {
                    timer = timer + 1 * Time.deltaTime;
                    OnLerp(timer/time_to_end);
                }
                else
                {
                    timer = 0;
                    OnFinish.Invoke(id);
                }
            }
        }
    }

    protected virtual void OnLerp(float lerpVal)
    {
        //por si se necesita
    }
    protected abstract void OnBegin();
    protected abstract void OnEnd();
    protected abstract void OnTick(float timeDelta);
}
