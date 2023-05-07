using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rangodeAlerta;
    public LayerMask playerMask;
    bool alerta;
    public float velocity;
    


    [SerializeField] private string enemyName;
    [SerializeField] private float moveSpeed;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;

    public Transform player; // target player

    private void Start()
    {
        healthPoint = maxHealthPoint;
        introduction();
    }

    private void Update()
    {
        Move();

        if(healthPoint<=0)
        {
            Death();
        }

        Attack();
            
    }

    protected virtual void introduction()
    {
        Debug.Log("Mi nombre es" + enemyName + "HP" + healthPoint + "moveSpeed" + moveSpeed);
    }

    protected virtual void Move()
    {
        alerta=Physics.CheckSphere(transform.position, rangodeAlerta,playerMask);

        if(alerta==true)
        {
            Vector3 posPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(posPlayer);
            transform.position = Vector3.MoveTowards(transform.position, posPlayer,velocity*Time.deltaTime);

        }
             
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangodeAlerta);
    }


    protected virtual void Attack()
    {
        Debug.Log(enemyName + "esta atacando");
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }




}
