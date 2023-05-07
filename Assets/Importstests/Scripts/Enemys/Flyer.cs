using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;
public class Flyer : Enemy
{
    [SerializeField] GameObject target;
    [SerializeField] Transform monitoringPoint;
    float distance;

    [Header("Speeds")]
    [SerializeField] float speed;
    [SerializeField] float speedRecoil;
    [SerializeField] float speedAttack;
    [SerializeField] float speedRotation;
    [SerializeField] float speedElevation;

    [Header("Ranges")]
    [SerializeField] float visionRange;
    [SerializeField] float attackRange;
    [SerializeField] float recoilDistance;

    [Header("Circular Movement")]
    [SerializeField] float angle = 0;
    [SerializeField] float radius;
    [SerializeField] float heightMax;
    [SerializeField] float heightMin, y;

    [Header("Boolean Vars")]
    [SerializeField] bool up_down = true;
    [SerializeField] bool hitting = false;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.playerInstance;
        y = monitoringPoint.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        ComportamientoFlyer();
    }


    void ComportamientoFlyer()
    {
        
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > visionRange)
        {
            //Volar en circulos alrededor del punto de monitoreo.
            Monitoring();
            
        }
        else
        {
            if (distance > attackRange)
            {
                //perseigue al player hasta alcanzarlo o hasta que este fuera de su rango de visión
                Persecution();
            }
            else
            {
                //Atacar al jugador
                Attack();
            }
        }
    }

    void Persecution()
    {
        var dir = target.transform.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
    }

    protected override void Attack()
    {
        base.Attack();
        //golpear al jugador, y hacer un retroceso, esperar y volver a atacar.
        var dir = target.transform.position - transform.position;
        
        if (hitting)
        {
            if(distance < recoilDistance)
            {
                transform.position -= dir * speedRecoil * Time.deltaTime;
            }
            else
            {
                hitting = false;
            }
        }
        else
        {
            transform.position += dir * speedAttack * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si golpea al jugador retrocede
        Debug.Log(other.gameObject.name);
        hitting = true;
        if (other.gameObject.name == target.gameObject.name)
        {
            
            Debug.Log("volador golpea al player");
        }
    }
    
    void Monitoring()
    {
        //movimiento circular alrededor del eje Y del punto de monitoreo
        float x = monitoringPoint.position.x + Mathf.Cos(angle) * radius;
        float z = monitoringPoint.position.z + Mathf.Sin(angle) * radius;
        //movimiento sobre el eje Y del flyer.
        if (up_down)
        {
            if (transform.position.y < monitoringPoint.position.y + heightMax)
            {
                y += speedElevation * Time.deltaTime;
            }
            else
            {
                Debug.Log("Vamo a bajar");
                up_down = false;
            }

        }
        else
        {
            if (transform.position.y > monitoringPoint.position.y - heightMin)
            {
                y -= speedElevation * Time.deltaTime;
            }
            else
            {
                up_down = true;
                Debug.Log("Vamo a subir");
            }
        }

        transform.position = new Vector3(x, y, z);

        angle += speedRotation * Time.deltaTime;
    }

}
