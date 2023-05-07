using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraholderposition : MonoBehaviour
{

    public Transform cameraposition;
    private void Update()
    {
        this.transform.position = cameraposition.position;
    }

}
