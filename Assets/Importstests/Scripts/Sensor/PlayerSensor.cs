using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] UnityEvent EV_OnplayerEnter;
    [SerializeField] UnityEvent EV_OnplayerExit;
    
    private void OnTriggerEnter(Collider other)
    {
       
        
            if (isPlayer(other))
            {
                print("main player");
                EV_OnplayerEnter.Invoke();
            }

        




    }

    private void OnTriggerExit(Collider other)
    {
        
            if (isPlayer(other))
            {
                print("player exit");
                EV_OnplayerExit.Invoke();
            }
        




    }

    
    bool isPlayer(Collider col)
    {
        Player c = col.GetComponent<Player>();

        if (c == GameManager.instance.GetPlayer())
        {
            return true;

        }

        return false;
    }
}
