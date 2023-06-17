using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrapScript : MonoBehaviour
{
    public GameObject beartrap;
    public GameObject player;
    [SerializeField] Player_Move player_Move;
    public Player playerlife;

    public MeshRenderer renderero;
    public bool activated;


    public void Start()
    {
       
       player = GameObject.Find("PlayerObj");

       player_Move = player.GetComponent<Player_Move>();

        playerlife = player.GetComponent<Player>();

    }
    public void StartTrap()
    {

        if (!activated)
        {
            player_Move.debuffname = "ensnare";
            player_Move.debuffduration = 1.5f;
            player_Move.debuffpower = 1f;

            GetComponent<Renderer>().material.color = Color.red;

            playerlife.TakeDamage(10);
            //player_Move.debuff();

            BuffManager.instance.ExecuteBuff("Snare");
            BuffManager.instance.ExecuteBuff("Fire");

        }
        else
        {
            return;
        }
       
        

    }
   
}
