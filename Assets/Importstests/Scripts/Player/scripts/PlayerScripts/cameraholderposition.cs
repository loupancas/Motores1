using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class cameraholderposition : MonoBehaviour
{

    public Transform cameraposition;
    public Camera camerapov;


    [Header("RaycastValues")]
    [SerializeField] private float Raycastlength;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] string ExcludeLayerMask;


    public void Start()
    {
       camerapov = FindObjectOfType<Camera>();
    }


    private void Update()
    {
        this.transform.position = cameraposition.position;
        RaycasterofPOV();
    }

    private void RaycasterofPOV()
    {
        Ray raypov = camerapov.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

     
        if(Physics.Raycast(raypov, out Hit, layerMaskInteract))
        {
                if(Hit.collider.gameObject.TryGetComponent(out DroneScript dronescript))
                {
                    dronescript.BeingLookedByPlayer();   
                }

            print(Hit.collider.name);
        }

        UnityEngine.Debug.DrawLine(transform.position, Hit.point, Color.red);

    }
}
