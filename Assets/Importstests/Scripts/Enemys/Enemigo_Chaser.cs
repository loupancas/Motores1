using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Chaser : Enemy

{
    public Transform waypoint1;
    public Transform waypoint2;
    public Transform waypointT;

    public int cantidadDmg = 10;

    private void Awake()
    {
        waypointT = waypoint1;
    }


    protected override void introduction()
    {
        base.introduction();
        Debug.Log("Hola, este es el enemigo 1");
    }

    protected override void Move()
    {
        base.Move();

        if(Vector3.Distance(transform.position,player.position)>rangodeAlerta)
        {
            if(Vector3.Distance(transform.position, waypoint1.position)<0.01f)
            {
                waypointT = waypoint2;
            }
            if (Vector3.Distance(transform.position, waypoint2.position) < 0.01f)
            {
                waypointT = waypoint1;
            }
            transform.position = Vector3.MoveTowards(transform.position, waypointT.position, velocity * Time.deltaTime);


        }


    }


    void OnTriggerEnter(Collider other)
    {
        var damageable = (IEnemyAttack)other.GetComponent<Player>();//

        if (damageable != null)
        {
            damageable.ContactAttack(cantidadDmg);
        }



    }




}
