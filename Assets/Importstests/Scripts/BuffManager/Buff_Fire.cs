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

    float timerFire;
    public float fireRate = 0.1f;
    protected override void OnTick(float timeDelta)
    {
        if (timerFire < fireRate)
        {
            timerFire = timerFire + 1 * timeDelta;
        }
        else
        {
            timerFire = 0;
            player.TakeDamage(1);
        }


    }
}
