using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IBulletDamage
{
    public float rangodeAlerta;
    public LayerMask playerMask;
    bool alerta;
    public float velocity;


    [SerializeField] private Image healthbarsprite;
    [SerializeField] private string enemyName;
    [SerializeField] private float moveSpeed;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;
    public Transform player; // target player


    public bool puedeEliminarse;
    public float danoCritico;// se personaliza el dano critico según la vida total del enemigo
    [SerializeField] private Color cambiarColor = Color.red;
    [SerializeField] private Color colorInicial = Color.white;
    public Renderer rend;
    public bool recibeDano;
    float timer = 0;
    [SerializeField] float time_duration = 1f;






    protected virtual void Start()
    {
        healthPoint = maxHealthPoint;



        introduction();

    }

    private void Update()
    {
        Move();

        updateHealthbar(maxHealthPoint, healthPoint);
        //if(healthPoint<=0)
        //{
        //Death();
        //}

        Attack();


        if (recibeDano)
        {
            if (timer < time_duration)
            {
                timer = timer + 1 * Time.deltaTime;
            }
            else
            {
                timer = 0;
                recibeDano = false;
                rend.material.color = colorInicial;
            }
        }



    }

    protected virtual void introduction()
    {
        Debug.Log("Mi nombre es" + enemyName + "HP" + healthPoint + "moveSpeed" + moveSpeed);
    }

    protected virtual void Move()
    {
        alerta = Physics.CheckSphere(transform.position, rangodeAlerta, playerMask);

        if (alerta == true)
        {
            Vector3 posPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(posPlayer);
            transform.position = Vector3.MoveTowards(transform.position, posPlayer, velocity * Time.deltaTime);

        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangodeAlerta);
    }


    protected virtual void Attack()
    {
        //Debug.Log(enemyName + "esta atacando");
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void updateHealthbar(float maxhealth, float currenthealth)
    {
        healthbarsprite.fillAmount = currenthealth / maxhealth;

    }


    public void BulletDmg(int damageRecibido) // en cada golpe se irá perdiendo vida hasta que finalmente el enemigo se destruye , esto es una funcion publica
    {


        healthPoint = healthPoint - damageRecibido;



        if (healthPoint <= 0)
        {

            Debug.Log("Haz eliminado al enemigo");
            Death();
        }
        else
        {
            timer = 0;
            recibeDano = true;
            rend.material.color = cambiarColor;
        }




    }




}
