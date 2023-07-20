using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DroneScript : Enemy
{
    [Header("References")]
    public GameManager manager;
    public GameObject Player;
    public ParticleSystem particle;
    public AIState state;
    public UnityEvent FiringEvent;
    public Animator animator;
   

    [Header("Variables")]
    public float aggrorange;
    public float movespeed;
    public int energy, maxenergy;
    public float energyrefill,gainenergycount,timebetweendodges;
    public bool shooting, moved,acting;
    public float rotationspeed, ofsetplayer;
    public string currstatename = "Move";
    public float switchIA = 0;
    public bool dodging,canDodge;
    


    public enum AIState
    {
        asleep,
        Idle,
        pursue,
        attack,
        dodge,
        die,
    }
     
    protected override void Start()
    {
        base.Start();
        manager = FindObjectOfType<GameManager>();
        Player = manager.playerInstance;
        player = manager.playerInstance.transform;
        state = AIState.asleep;
        energy = maxenergy;

        dodgepulse = dodgetimer;
    }
    public void Update()
    {
        statemachine();
        
    }
    public void FixedUpdate()
    {
        if(state== AIState.Idle || state == AIState.die) 
        {
            return;
        }

        if (energy < maxenergy)
        {
            if (energyrefill < gainenergycount)
            {
                energyrefill = energyrefill + 1 * Time.deltaTime;
            }
            else
            {
                energy++;

                dodging = false;

                if (switchIA < 2)
                {
                    switchIA++;
                }
                else
                {
                    switchIA = 0;
                }

                energyrefill = 0;
              
            }
        }



    }

    public void statemachine()
    {
       if(state == AIState.die)
       {
            return;
       }
       
        float distanceplayer = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceplayer > aggrorange)
        {
            state = AIState.Idle;
        }

        else if(energy > 1 && switchIA == 0 && dodging!)
        {
            state = AIState.pursue;
            Move();
            
        }

        else if (energy > 1 && switchIA == 1 && dodging!)
        {
            state = AIState.attack;
            Attack();
            energy--;

        }

        else if (dodging == true && energy > 1)
        {
            Dodge();
        }

    }
    protected override void Attack()
    {
        rotationspeed = 30f;
        moved = false;
        Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookrotation, rotationspeed * Time.deltaTime);
        FiringEvent.Invoke();
        print("drone has fired");
    }


    protected override void Death()
    {
        //Destroy(this.gameObject);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true; rb.velocity = Vector3.zero;
    }

    protected override void introduction()
    {

    }

    Vector3 movetarget;
    protected override void Move()
    {

        if (moved== false)
        {
            movetarget = new Vector3(player.transform.position.x + Random.Range(-ofsetplayer, ofsetplayer), player.transform.position.y + Random.Range(0,ofsetplayer), player.transform.position.z + Random.Range(-ofsetplayer, ofsetplayer));
            energy--;
            moved = true;
        }
        rotationspeed = 10f;
        transform.position = Vector3.MoveTowards(transform.position,movetarget, movespeed * Time.deltaTime);
        Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation,lookrotation, rotationspeed * Time.deltaTime);

    }

    [Header("Dodge Values")]
   
    public float dodgetimer;
    public float dodgepulse;

    public void Dodge()
    {
        if(dodgepulse < dodgetimer)
        {
            dodgepulse = dodgepulse + 1* Time.deltaTime;
        }
        else
        {
            movetarget = Dodgetarget();
            energy--;
            dodgepulse = 0;
        }
        movetarget = Dodgetarget();
        rotationspeed = 10f;
        transform.position = Vector3.MoveTowards(transform.position, movetarget, movespeed * Time.deltaTime);
        Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookrotation, rotationspeed * Time.deltaTime);

    }

    public void BeingLookedByPlayer()
    {
        state = AIState.dodge;
        print("drone ha esquivado");
        dodging= true;
    }

    public Vector3 Dodgetarget()
    {
        movetarget = new Vector3(this.transform.position.x + Random.Range(-ofsetplayer, ofsetplayer), player.transform.position.y + Random.Range(0, ofsetplayer), this.transform.position.z + Random.Range(-ofsetplayer, ofsetplayer));
        return movetarget;
    }


}
