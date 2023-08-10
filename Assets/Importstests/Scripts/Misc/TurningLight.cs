using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningLight : MonoBehaviour
{
    [Header("References y variables")]
    [SerializeField] GameObject LightObj;
    public bool IsOn;

    void Start()
    {

        if (IsOn)
        {
            LightObj.SetActive(true);
        }
        else
        {
            LightObj.SetActive(false);
        }

    }

    private void Updatelight()
    {
        if (IsOn)
        {
            LightObj.SetActive(true);
        }
        else if (IsOn ==false)
        {
            LightObj.SetActive(false);
        }

    }

    public void Switchlight()
    {
        if(IsOn == true)
        {
            IsOn = false;
        }
        else if(IsOn == false)
        {
            IsOn = true;
        }

        Updatelight();
    }
}
