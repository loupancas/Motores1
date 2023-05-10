using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public bool isEnemyBullet;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision) // las naranjas se destruiran cuando colisionen con otros objetos que tengan collider, recordar que deben tener RigidBody
    {

        var B = collision.gameObject.GetComponent<bullet>();

        if (B != null) // si choca una naranja con otra naranja sale de la función y no lo elimina
        {
            return;
        }

        if (!isEnemyBullet)
        {
            Enemy Rompible = collision.gameObject.GetComponent<Enemy>(); // esta trayendo el codigo 


            if (Rompible)
            {
                Debug.Log("el daño realizado es" + damage);
                Rompible.golpe(damage); // llama a la funcion golpe del otro script

            }

            if (collision.gameObject.tag != "Player") // se destruirá con todo lo que no sea el PJ o sea los enemigos
            {

                Destroy(gameObject);

            }
        }

    }



}
