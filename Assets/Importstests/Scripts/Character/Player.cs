using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;
using Entities.WeaponHolder;

public class Player : LifeEntity, IEnemyAttack, IBulletDamage
{
    public ManagerUI ManagerUI;
    public WeaponHolder weaponholder;

    [SerializeField] KeyCode weapon1 = KeyCode.Alpha1;
    [SerializeField] KeyCode weapon2 = KeyCode.Alpha2;

    //private WeaponHandler.Handler<Weapon> armory;


    protected override void Start()
    {
        base.Start();//life = new Life(100);
        GameManager.instance.playerInstance = gameObject;
        weaponholder = gameObject.AddComponent<WeaponHolder>();
        weaponholder.Initialize(GameManager.instance.weaponpos, GameManager.instance.camPos);

       
    }
    protected override void OnInitialize()
    {
        GameManager.instance.playerInstance = this.gameObject; // se asigna a s� mismo
        GameManager.instance.player=this;
    }
    protected override void OnDeInitialize()
    {
        
    }

    protected override void OnFixedUpdate()
    {
        
    }

    

    protected override void OnPause()
    {
      
    }

    protected override void OnResume()
    {
        
    }

    protected override void OnUpdate()
    {

      
        //if (weaponholder.weapon != null)
        //{
        //    weaponholder.weapon.Attack();
        //    weaponholder.weapon.transform.position = weaponholder.weaponpos.position;
        //    weaponholder.weapon.transform.forward = weaponholder.GetComponent<Camera>().transform.forward;
        //}


        if (Input.GetKeyDown(weapon1))
        {
            weaponholder.ChangeWeapon(0); // Asume que el arma 1 tiene ID 0
        }
        else if (Input.GetKeyDown(weapon2))
        {
            weaponholder.ChangeWeapon(1); // Asume que el arma 2 tiene ID 1
        }

        if (weaponholder.weapon != null)
        {
            weaponholder.weapon.Attack();
            weaponholder.weapon.transform.position = weaponholder.weaponpos.position;
            weaponholder.weapon.transform.forward = weaponholder.camera.forward; // Corrige la obtención de la dirección de la cámara
        }

    }

   

   

    public override void Health(int healQuantity)
    {
        base.Health(healQuantity);

        ManagerUI.UpdateLife(life.Live, life.lifeMax);

        print("se curo");
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        ManagerUI.UpdateLife(life.Live, life.lifeMax);

    }

    public void BulletDmg(int dmg)
    {
        TakeDamage(dmg);
        Debug.Log("ataque bala");
    }

    public void ContactAttack(int dmg)
    {
        TakeDamage(dmg);
        Debug.Log("ataque especial");
    }

}
