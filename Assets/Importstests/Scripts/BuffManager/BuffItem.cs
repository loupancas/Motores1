using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffItem : MonoBehaviour
{
    [SerializeField] string id = "default"; // id del buff, usado par nombrarlo
    public string ID { get { return id; } }

    bool isActive = false;
    [SerializeField] bool autoTime = false;
    float timer;
    [SerializeField] float time_to_end = 2f;
    Action<string> OnFinish; // accion de finalizado de buff

    protected virtual void Start()
    {
        //si se necesita overridear
    }

    public void SUbscribeToFInish(Action<string> _OnFinish) // transformamos la accion string _onfinish entregada por el buff manager a la variable interna onfinish
    {
        OnFinish = _OnFinish;
    }

    public void Begin() //inicio del buff 
    {
        isActive = true;
        OnBegin();
    }
    public void End() // finalizado del buff
    {
        isActive = false;
        OnEnd();
    }
    private void Update()
    {
        if (isActive)
        {
            OnTick(Time.deltaTime); // auto worn off del buff

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
                    OnFinish.Invoke(id); // llamado del evento onfinish del buff
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
