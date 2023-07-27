using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : Interactible
{
    [Header("References")]
    public GameManager manager;
    public GameObject player;
    public Animator animator;
    

    [Header("Variables")]
    [SerializeField] float DistancetoTrigger;
    [SerializeField] bool triggered;
    [SerializeField] KeyCode ActivateKey;


    protected override void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = manager.playerInstance;
        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if(Input.GetKeyDown(ActivateKey))
            {
                InternalLogic();
            }
        }
    }

    private void InternalLogic()
    {
        if (!triggered)
        {
            Activate();
            triggered = true;
        }
        else
        {
            deactivate();
            triggered= false;
        }
       

    }

    protected override void Activate()
    {
        animator.SetBool("LeverUp", true);
        print("he sido prendido uwu");
        ActivateEvent.Invoke();
    }

    protected override void deactivate()
    {
        animator.SetBool("LeverUp", false);
        print("he sido apagado ewe");
        DeactivateEvent.Invoke();
    }

    protected override void onsight()
    {
        //
    }

}
