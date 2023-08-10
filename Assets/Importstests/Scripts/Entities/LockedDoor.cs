using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("references")]
    GameManager manager;
    Rigidbody rb;
    AudioSource AudioS;

    [Header("variables")]
    public bool IsLocked;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        AudioS = GetComponent<AudioSource>();

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

            AudioS.Play();
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
