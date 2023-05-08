using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo3 : Enemy
{
    [SerializeField] GameObject character;
    [SerializeField] BulletEnemy bullet;
    [SerializeField] float shotspeed;
    [SerializeField] float timer;
    [SerializeField] float rangeAttack;

    private void Update()
    {
        Attack();
    }
    public GameObject projectile;
    protected override void introduction()
    {
        //base.introduction();
        Debug.Log("Hola, este es el enemigo 3");
    }

    protected override void Attack()
    {
        //base.Attack();
        transform.forward = character.transform.position - transform.position;
        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (character != null)
            {
                BulletEnemy newbullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newbullet.transform.localPosition = transform.position;
                newbullet.transform.rotation = transform.rotation;

            }
            timer = shotspeed;
        }

    }
}
