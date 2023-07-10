using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreofMass : MonoBehaviour
{
    public Transform CentrePoint;

    public void Start()
    {
        CentrePoint = this.transform;
    }
}
