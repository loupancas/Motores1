using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapDetectorTouch : MonoBehaviour
{
    public GameObject trapitem;
    public LayerMask player;
    public string Tagplayer;
    public BearTrapScript bearTrapScript;
     



    private void Awake()
    {
        player = LayerMask.GetMask("Player");
        bearTrapScript = GetComponent<BearTrapScript>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Move>() != null)
        {
           
            print("otro detectado");
            return;
        }
        else
        {
            bearTrapScript.StartTrap();
            print("jugador detectado");
            return;
        }

    }

    public void OnTriggerExit(Collider other)
    {
       
        if (other.GetComponent<Player_Move>() != null)
        {
            print("otro detectado");
            return;
        }
        else
        {
            bearTrapScript.activated = false;
            print("jugador dejo");
            GetComponent<Renderer>().material.color = Color.white;
            return;
        }
    }



}
