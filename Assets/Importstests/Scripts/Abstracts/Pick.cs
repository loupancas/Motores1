using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Pick : MonoBehaviour
{
   
    public void pick()
    {
        onPick();
    }

    protected abstract void onPick();

}


