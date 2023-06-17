using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_SlowDown : BuffItem
{
    Player_Move playerMove;

    protected override void Start()
    {
        base.Start();

        playerMove = GameManager.instance.player.GetComponent<Player_Move>();
    }

    protected override void OnBegin()
    {
        Debug.Log("SNARE");
        playerMove.Snare(true);
    }

    protected override void OnEnd()
    {
        playerMove.Snare(false);
    }

    protected override void OnTick(float timeDelta)
    {
        // no lo usas
    }
}
