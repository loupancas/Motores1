using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GardenHandler : MonoBehaviour
{

    public List<GameObject> Enemys = new List<GameObject>();

    public UnityEvent LeverTriggers_1, LeverTriggers_2, LeverTriggers_3, LeverTriggers_4;
    public void EventChangeAllEnemyColors()
    {
        foreach(var enemy in Enemys)
        {
            enemy.GetComponent<ChangeColor>().ChangeColorEvent();

        }
    }

    public void TriggerEvent1()
    {
        LeverTriggers_1.Invoke();
    }

    public void TriggerEvent2()
    {
        LeverTriggers_2.Invoke();
    }

    public void TriggerEvent3()
    {
        LeverTriggers_3.Invoke();
    }

    public void TriggerEvent4()
    {
        LeverTriggers_4.Invoke();
    }
}
