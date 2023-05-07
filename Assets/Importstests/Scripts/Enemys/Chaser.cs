using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public int Rutina;
    public float cronometro;
    public Animator animacion;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    
    void Start()
    {
        animacion = GetComponent<Animator>();
        target = GameObject.Find("Character");
    }

    public void Comportamiento()
    {
        if(Vector3.Distance(transform.position,target.transform.position)>5)
        {
            animacion.SetBool("correr", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                Rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (Rutina)
            {
                case 0: //enemigo quieto
                    animacion.SetBool("caminar", false);
                    break;
                case 1: //direccion desplazamiento
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0); // donde gira eje y
                    Rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime); // se movera hacia adelante
                    animacion.SetBool("caminar", true);
                    break;


            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 &&!atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animacion.SetBool("caminar", false);
                animacion.SetBool("correr", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animacion.SetBool("atacar", false);
            }
            else 
            {
                animacion.SetBool("caminar", false);
                animacion.SetBool("correr", false);
                animacion.SetBool("atacar", true);
                atacando = true;

            }
                           


        }

    }



    void Update()
    {
        Comportamiento();
    }

    public void Cancela_Animacion_Ataque()
    {
        animacion.SetBool("atacar", false);
        atacando = false;
    }


}
