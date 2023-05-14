using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playermodel : MonoBehaviour
{


    public GameObject player;
    public GameObject playerorientation;
    public List<SkinnedMeshRenderer> renderers;

    
    public void Start()
    {

        
        foreach (var renderer in renderers)
        {
           renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
            
        
    }


    private void FixedUpdate()
    {
     
            this.transform.rotation = playerorientation.transform.rotation;
        
        
            this.transform.position = player.transform.position;
       
         


    }




}
