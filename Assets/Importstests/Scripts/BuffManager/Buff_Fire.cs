using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Fire : BuffItem
{
    Player player;

    protected override void Start()
    {
        base.Start();

        player = GameManager.instance.player;
    }

    protected override void OnBegin()
    {

    }
       

    protected override void OnEnd()
    {
        
    }

    float timer;
    public float fireRate = 0.1f;
    protected override void OnTick(float timeDelta)
    {
        if (timer < fireRate)
        {
            timer = timer + 1 * timeDelta;
        }
        else
        {
            timer = 0;
            player.TakeDamage(1);
        }


    }
}
