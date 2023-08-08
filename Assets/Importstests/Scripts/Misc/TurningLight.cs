using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningLight : MonoBehaviour
{
    [Header("References y variables")]
    [SerializeField] Light Lightcomp;
    public bool IsOn;

    void Start()
    {
        Lightcomp = GetComponentInChildren<Light>();
        if (IsOn)
        {
            Lightcomp.enabled = true;
        }
        else
        {
            Lightcomp.enabled = false;
        }

    }

    private void Updatelight()
    {
        if (IsOn)
        {
            Lightcomp.enabled = true;
        }
        else
        {
            Lightcomp.enabled = false;
        }

    }

    public void Switchlight()
    {
        if(IsOn)
        {
            IsOn = false;
        }
        else if(IsOn)
        {
            IsOn= true;
        }
        Updatelight();
    }
}
