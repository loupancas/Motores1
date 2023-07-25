using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DroneScript : Enemy
{
    [Header("References")]
    public GameManager manager;
    public CharacterController characterControl;
    public GameObject Player;
    public ParticleSystem particle;
    public AIState state;
    public UnityEvent FiringEvent;
    public Animator animator;
    public SFXEnemyManager Sounds;
   

    [Header("Variables")]
    public float aggrorange;
    public float movespeed,maxmovespeed;
    public int energy, maxenergy;
    public float energyrefill,gainenergycount,timebetweendodges;
    public bool shooting, moved,acting;
    public float rotationspeed, ofsetplayer;
    public string currstatename = "Move";
    public float switchIA = 0;
    public bool dodging,canDodge;
    private int randomizemove,randomnoise;
    


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
        Sounds = GetComponent<SFXEnemyManager>();
        Player = manager.playerInstance;
        player = manager.playerInstance.transform;
        state = AIState.asleep;
        energy = maxenergy;

        maxmovespeed = movespeed;

        dodgepulse = dodgetimer;
    }

    public void Update()
    {
        statemachine();
        Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookrotation, rotationspeed * Time.deltaTime);

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

                randomnoise = Random.Range(0, 6);


                if (switchIA < 2)
                {
                    switchIA++;
                }
                else
                {
                    switchIA = 0;
                }

                energyrefill = 0;
                
                if(randomnoise == 6)
                {
                    Sounds.PlaySFX("idle1");
                }
                else if(randomnoise == 3)
                {
                    Sounds.PlaySFX("idle2");
                }

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

        else if(energy > 1 && switchIA == 0)
        {
            state = AIState.pursue;
            randomizemove = Random.Range(0, 2); //randomizar el movimiento
            Move();
            
        }

        else if (energy > 1 && switchIA == 1)
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
        //Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        //transform.rotation = Quaternion.Slerp(this.transform.rotation, lookrotation, rotationspeed * Time.deltaTime);
        FiringEvent.Invoke();
        Invoke("Chargesound", 0f);
        Invoke("Firesound", 1.19f);
        print("drone has fired");
    }

    private void Chargesound()
    {
        Sounds.PlaySFX("charge");
    }
    private void Firesound()
    {
        Sounds.PlaySFX("fire");
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
        if (moved== false && randomizemove == 0) // chase player
        {
            movetarget = new Vector3(player.transform.position.x + Random.Range(-ofsetplayer, ofsetplayer), player.transform.position.y + Random.Range(0,ofsetplayer), player.transform.position.z + Random.Range(-ofsetplayer, ofsetplayer));
            energy--;
            movespeed = maxmovespeed;
            moved = true;
        }

        else if(moved==false && randomizemove == 1) // dodge in place
        {
            movetarget = new Vector3(this.transform.position.x + Random.Range(-ofsetplayer*2, ofsetplayer*2), player.transform.position.y + Random.Range(0, ofsetplayer * 1.2f), this.transform.position.z + Random.Range(-ofsetplayer * 2, ofsetplayer * 2));
            movespeed = movespeed * 1.5f ; energy--;energyrefill +=0.5f ; moved = true;
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
       
    }

    public Vector3 Dodgetarget()
    {
        movetarget = new Vector3(this.transform.position.x + Random.Range(-ofsetplayer, ofsetplayer), player.transform.position.y + Random.Range(0, ofsetplayer), this.transform.position.z + Random.Range(-ofsetplayer, ofsetplayer));
        return movetarget;
    }


}
