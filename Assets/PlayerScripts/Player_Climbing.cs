using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Climbing : MonoBehaviour
{
    [Header("References")]

    public Transform orientation;
    public Rigidbody rb;
    public LayerMask walls;
    public LayerMask Ground;
    public Player_Move player_Move;

    [Header("Climbing")]
    [SerializeField] float climbspeed;
    [SerializeField] float maxclimbtime;
    private float climbtime;

    private bool IsClimbing;

    [Header("climbjumping")]
    [SerializeField] float climbjumpforce;
    [SerializeField] float climbjumpback;

    public KeyCode climbjump = KeyCode.Space;
    [SerializeField] int climbjumps;
    int climbjumpsleft;

    [Header("Exiting wall")]
    public bool exitingwall;
    [SerializeField] float exitingwalltime;
    private float exitwalltimer;


    public float minwallnormalchange; 
 
    [Header("Detection")]
    public float detectionlenght;
    public float spherecastradius;
    public float maxwallangle;
    private float currentwallangle;

    private RaycastHit frontwallhit;
    private bool wallInFront;

    private Transform lastwall;
    private Vector3 lastwallnormal;
    public float minwallnormalanglechange;



    private void Update()
    {
        Wallcheck();
        StateMachine(); 

        if(IsClimbing && !exitingwall)
        {
            WallClimbMovement(); 
        }


    }

    private void StateMachine()
    {
        //comenzar a trepar
        if(wallInFront && Input.GetKey(KeyCode.W) && currentwallangle < maxwallangle && !exitingwall)
        {
            if (!IsClimbing && climbtime > 0)
            {
                StartWallClimb();
            }
            //timer
            if (climbtime > 0)
            {
                climbtime -= Time.deltaTime;
            }

            if (climbtime <= 0)
            {
                EndWallClimb();
            }

        }
        //estado salir del muro
        else if(exitingwall)
        {
             if(IsClimbing)
             {
                EndWallClimb();

                if(exitwalltimer > 0) 
                { 
                    exitwalltimer-= Time.deltaTime;    
                }
                else if(exitwalltimer <= 0)
                {
                    exitingwall = false; 
                }
             }
        }


        //estado - nulo
        else
        {
            if (IsClimbing) EndWallClimb();
        }

        //estado salto del muro

        if( wallInFront && Input.GetKeyDown(climbjump) && climbjumpsleft > 0)
        {
            ClimbJumping();
        }

    }

    private void Wallcheck()
    {
        //calculo spherecast para detectar un muro y guardarlo en una variable (here be dragons)
        wallInFront = Physics.SphereCast(transform.position, spherecastradius, orientation.forward, out frontwallhit, detectionlenght, walls);

        //limitar el angulo maximo para escalar
        currentwallangle = Vector3.Angle(orientation.forward, -frontwallhit.normal);


        //calculo para ver si el jugador golpeo un nuevo muro
        bool newWall = frontwallhit.transform != lastwall || Mathf.Abs(Vector3.Angle(lastwallnormal, frontwallhit.normal)) > minwallnormalanglechange; 


        if((wallInFront && newWall) || player_Move.grounded) //check para resetear los saltos
        {
            climbtime = maxclimbtime;
            climbjumpsleft = climbjumps;
            exitingwall = false;
        }
    }

    private void StartWallClimb()
    {

        IsClimbing = true; //set bools
        player_Move.climbing= true;

        lastwall = frontwallhit.transform; //guardar la info del muro agarrado, importante para futuro
        lastwallnormal= frontwallhit.normal;

    }

    private void EndWallClimb() //finalizar subida
    {
        IsClimbing = false;
        player_Move.climbing = false;
    }

    private void WallClimbMovement() //movimiento de subir
    {
        rb.velocity= new Vector3 (rb.velocity.x,climbspeed,rb.velocity.z);
    }

    public void ClimbJumping()
    {
        exitingwall = true;
        exitwalltimer = exitingwalltime;

        //fuerza de salto
         Vector3 forcetoapply = transform.up * climbjumpforce + frontwallhit.normal *climbjumpback *5f;

        rb.velocity= new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forcetoapply, ForceMode.Impulse); //aplucar salto

        climbjumpsleft--;
    }

}
