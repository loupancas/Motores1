using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;

public abstract class LifeEntity : PlayObject
{
    Life life;
    [SerializeField] int initialLife = 100;

    public override void Initialize()
    {
        base.Initialize();
        life = new Life(initialLife);
    }

    public void TakeDamage(int damage)
    {
        life.Live -= damage;
    }


}
