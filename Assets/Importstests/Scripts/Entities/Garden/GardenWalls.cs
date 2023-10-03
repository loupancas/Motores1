using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenWalls : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] GameObject Door;


    private void Start()
    {
        Door = this.gameObject;
        
        animator=GetComponent<Animator>();
    }

    public void OpenSesame()
    {
        animator.SetTrigger("Open");
        animator.ResetTrigger("Close");


    }


    public void CloseSesame()
    {
        animator.SetTrigger("Close");
        animator.ResetTrigger("Open");

      
    }

}
