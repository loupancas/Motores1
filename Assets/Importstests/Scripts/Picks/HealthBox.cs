using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : Pick
{

    public int HealthNum =10;
    protected override void onPick()
    {
        GameManager.instance.GetPlayer().Health(HealthNum);  
    }

    
}
