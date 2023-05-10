using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.WeaponHolder
{
    public class WeaponHolder
    {
        public GameObject[] weaponholder;
        public GameObject weapon;
        public Transform weaponpos;
        public Transform camera;
        public int actualID = 0;
        public WeaponHolder(Transform _weaponpos, Transform _camera)
        {
            weaponpos = _weaponpos;
            camera = _camera;
            weaponholder = new GameObject[10];
            weapon = weaponholder[0];
        }

        public void AddWeapon(int ID, GameObject _weapon)
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
                        weaponholder[actualID].SetActive(false);
                    }
                    
                    weaponholder[ID].SetActive(true);                    
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
