using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.WeaponHolder
{
    public class WeaponHolder
    {
        //estos deberian ser objetos Weapon
        public Weapon[] weaponholder;
        public Weapon weapon;
        public Transform weaponpos;
        public Transform camera;
        public int actualID = 0;
        public WeaponHolder(Transform _weaponpos, Transform _camera)
        {
            weaponpos = _weaponpos;
            camera = _camera;
            weaponholder = new Weapon[10];
            weapon = weaponholder[0];
        }

        public void AddWeapon(int ID, Weapon _weapon)
        {
               
            weaponholder[ID] = _weapon;
            ChangeWeapon(ID);
        }
        public void RemoveWeapon(int ID)
        {
            weaponholder[ID] = null;
        }

        public void ChangeWeapon(int ID)
        {
            if ((ID <= weaponholder.Length - 1) && ID >= 0)
            {
                if(weaponholder[ID] != null)
                {
                    if(weaponholder[actualID] != null)
                    {
                        weaponholder[actualID].gameObject.SetActive(false);
                    }
                    
                    weaponholder[ID].gameObject.SetActive(true);                    
                    weapon = weaponholder[ID];
                    Debug.Log("arma " + weapon);
                    actualID = ID;
                }
                else
                {
                    Debug.Log("No hay arma pai");
                }
            }

        }

    }

}
