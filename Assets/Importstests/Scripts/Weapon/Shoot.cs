using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Weapon
{
    [Header("Caracteristicas Shotgun")]
    public GameObject bullet;
    public float shotRate = 0.5f;
    private float shotRateTime = 0; // contador
    protected override void Start()
    {
        base.Start();
        ID = 0;
    }

    public override void Attack()
    {
        base.Attack();
        if (!onground)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.time > shotRateTime && bullets != 0)
                {
                    GameObject newBullet;
                    newBullet = Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
                    newBullet.GetComponent<Rigidbody>().AddForce(spawnpoint.forward * strenght); // vector desde nuestro punto spwan
                    shotRateTime = Time.time + shotRate;
                    bullets--;
                    Destroy(newBullet, 4);

                }
            }
        }
    }

}
