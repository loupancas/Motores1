using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : Enemy
{
    [Header("References")]
    public GameManager manager;
    public GameObject Player;
    public ParticleSystem particle;
    public AIState state;
   

    [Header("Variables")]
    public float aggrorange;
    public float movespeed;
    public int energy, maxenergy;
    public float energyrefill,gainenergycount;
    public bool shooting, moved,acting;
    public float rotationspeed, ofsetplayer;


    public enum AIState
    {
        Idle,
        pursue,
        attack,
        dodge,
        die,
    }
     
    protected override void Start()
    {
        manager = FindObjectOfType<GameManager>();
        Player = manager.playerInstance;
        player = manager.playerInstance.transform;
        state = AIState.Idle;
        energy = maxenergy;
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
                moved = false;
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

        else if(energy > 1 && shooting == false)
        {
            state = AIState.pursue;
            Move();
            
        }
        else if (energy > 1 && moved == true)
        {
            state = AIState.attack;
            Attack();
            energy--;

        }

    }
    protected override void Attack()
    {

    }


    protected override void Death()
    {

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
            moved = true;
            energy--;
        }

        transform.position = Vector3.MoveTowards(transform.position,movetarget, movespeed * Time.deltaTime);
        Quaternion lookrotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation,lookrotation, rotationspeed * Time.deltaTime);

    }

}
