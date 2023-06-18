using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Bomb : Usable
{
    public Transform pointToDrop;
    public _bomb bombModel;
    public override void Use()
    {
        print("drop bomb");
        _bomb bomb = Instantiate(bombModel);

        bomb.throwBomb(pointToDrop.position, pointToDrop.forward);
    }
}
