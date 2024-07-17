using Entities.WeaponHolder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    [Header("Referencias")]
    [SerializeField] protected Transform spawnpoint;
    [SerializeField] protected LayerMask layermask;
    protected BoxCollider col;
    protected Player playerref;
    //private WeaponHolder weaponHolder;

    [Header("Caracteristicas Arma")]
    [SerializeField] protected int ID;
    [SerializeField] protected int maxbullet;
    [SerializeField] protected int bullets;
    [SerializeField] protected float scope;
    [SerializeField] protected float strenght;
    [SerializeField] protected bool isenabled;
    [SerializeField] protected bool onground;

    /// <summary>
    /// Start Weapon - 
    /// Se toma la referencia del componente player del player
    /// y el collider de la propia arma.
    /// </summary>
    protected virtual void Start()
    {
        playerref = GameManager.instance.player;
        //weaponHolder = playerref.GetComponent<WeaponHolder>(); // Obtener referencia al WeaponHolder
        col = GetComponent<BoxCollider>();
    }

    public int Bullet
    {
        get
        {
            return bullets;
        }
        set
        {
            if (value < 0)
            {
                bullets = 0;
            }
            else if (value > maxbullet)
            {
                bullets = maxbullet;
            }
            else
            {
                bullets = value;
            }
        }
    }

    /// <summary>
    /// Recharge - 
    /// Se añade balas a la variable bullets. 
    /// </summary>
    /// <param name="recharge">Cantidad de balas a añadir.</param> 
    ///  
    public virtual void Recharge(int recharge)
    {
        bullets += recharge;
    }

    public virtual void Attack()
    {

    }
    /// <summary>
    /// OnTriggerEnter Weapon
    /// </summary>
    /// Si el player es quien toca el arma,
    /// Se almacena en el WeaponHolderm con el ID del arma.
    /// <param name="other"></param>
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            onground = false;
            WeaponHolder weaponHolder = playerref.GetComponent<WeaponHolder>();
            // Obtener el componente Handler<Weapon> y agregar este arma
            //var weaponHandler = playerref.GetComponent<WeaponHandler.Handler<Weapon>>();
            if (weaponHolder != null)
            {
                weaponHolder.AddWeapon(ID, this);
                col.enabled = false;
                Debug.Log("Arma recogida");
               
                //PickUpWeapon(this); // Remove weapon from the ground
            }
            else
            {
                Debug.LogError("No se encontró el componente en el player.");
            }
        }
    }

   

  
}

