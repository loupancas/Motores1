using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Shooter : Enemy
{
    [SerializeField] GameObject character;
    [SerializeField] Balas balas;
    [SerializeField] float shotspeed;
    [SerializeField] float time;
    [SerializeField] float rangeAttack;
    [SerializeField] Transform shootPoint;
    public int cantidadDmg = 10;
    private void Update()
    {
        Attack();
        Vector3 dir = character.transform.position - this.transform.position; // mirar al character
        transform.forward = dir;
    }
    public GameObject projectile;
    protected override void introduction()
    {
        //base.introduction();
        Debug.Log("Hola, este es el enemigo 3");
    }

    protected override void Attack()
    {
        if (time > 0) time -= Time.deltaTime;
        if (time <= 0)
        {
            if (character != null)
            {
                Balas newbullet = Instantiate(balas, shootPoint.position, Quaternion.identity);
                newbullet.isEnemyBullet = true;
                newbullet.transform.position = shootPoint.position;
                Vector3 forwardaux = shootPoint.forward;
                forwardaux.y = 0;

                newbullet.transform.forward = shootPoint.forward;

            }
            time = shotspeed;
        }

    }
}
