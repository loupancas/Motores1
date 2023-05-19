using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;

public abstract class LifeEntity : PlayObject
{
    protected Life life;
    [SerializeField] int initialLife = 100;
    [SerializeField] int maxLife = 100;
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

}
