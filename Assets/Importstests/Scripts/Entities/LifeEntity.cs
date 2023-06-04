using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;
using System;

public abstract class LifeEntity : PlayObject
{
    protected Life life;
    [SerializeField] int initialLife = 100;
    [SerializeField] int maxLife = 100;
    public event Action DeathEvent = delegate { };
    public override void Initialize()
    {
        base.Initialize();
        life = new Life(maxLife, initialLife);
    }

    public virtual void TakeDamage(int damage) // funcion a ser llamada para quitar vida
    {
        if (initialLife > 0)
        {
            life.Live -= damage;
        }


        Debug.Log("la vida restante es" + life.Live);


        if (life.Live <= 0)
        {
            DeathEvent.Invoke();
        }

    }



    public virtual void Health(int healthAmount)
    {

        if (life.Live < maxLife)
        {
            life.Live += healthAmount;
        }
        if (initialLife > maxLife)
        {
            life.Live = maxLife;
        }

    }


    public void subscribeToDeath(Action _callback)
    {
        DeathEvent += _callback;

    }

    public void DesubscribeToDeath(Action _callback)
    {
        DeathEvent -= _callback;

    }



}
