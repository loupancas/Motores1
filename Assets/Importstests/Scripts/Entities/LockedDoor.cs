using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("references")]
    GameManager manager;
    Rigidbody rb;

    [Header("variables")]
    public bool IsLocked;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        manager = FindObjectOfType<GameManager>();

        if(IsLocked == false)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
    }


    public void Unlock()
    {
        if(IsLocked == true)
        {
            IsLocked= false;
        }

        UpdateRb();
    }

    public void Lock()
    {
        if(IsLocked== false)
        {
            IsLocked= true;
        }

        UpdateRb();
    }

    private void UpdateRb()
    {
        if(IsLocked== true)
        {
            rb.isKinematic= true;
        }
        else
        {
            rb.isKinematic= false;
        }
    }


}
