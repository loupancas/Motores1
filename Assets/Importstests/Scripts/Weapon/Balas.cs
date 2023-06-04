using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    public LayerMask playerMask;
    public bool isEnemyBullet;
    public int damage;

    private void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider collision) // las bullets se destruiran cuando colisionen con otros objetos que tengan collider, recordar que deben tener RigidBody
    {
        var damageable = collision.GetComponent<IBulletDamage>();

        if (isEnemyBullet)
        {
            //var player = (Player)damageable; // convierte a player

            if (damageable is Player)
            {
                var player = (Player)damageable;

                if (player != null)
                {
                    if (damageable != null)
                    {
                        damageable.BulletDmg(damage);
                    }

                }
            }




        }
        else
        {
            var enemy = (Enemy)damageable; // convierte a Enemy
            if (enemy != null)
            {
                if (damageable != null)
                {
                    damageable.BulletDmg(damage);
                }

            }

        }
    }
}
