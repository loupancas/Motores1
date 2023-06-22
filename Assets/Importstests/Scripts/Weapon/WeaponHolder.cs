using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.WeaponHolder
{/// <summary>
/// Clase WeaponHolder - 
/// Las armas se clasifican segun su ID, y son almacenadas segun el mismo. </summary>
    public class WeaponHolder
    {
        //estos deberian ser objetos Weapon
        public Weapon[] weaponholder;
        public Weapon weapon;
        public Transform weaponpos;
        public Transform camera;
        public int actualID = 0;

        /// <summary>
        /// Constructor de WeaponHolder
        /// </summary>
        /// <param name="_weaponpos">Posici�n respecto al player del WeaponHolder.</param>
        /// <param name="_camera">Usado para la direcci�n de ataque.</param>
        public WeaponHolder(Transform _weaponpos, Transform _camera)
        {
            weaponpos = _weaponpos;
            camera = _camera;
            weaponholder = new Weapon[10];
            weapon = weaponholder[0];
        }

        /// <summary>
        /// AddWeapon - 
        /// A�ade un arma al weaponholder.
        /// </summary>
        /// <param name="ID">ID del arma que determina su posici�n en el WeaponHolder</param>
        /// <param name="_weapon">El arma.</param>
        public void AddWeapon(int ID, Weapon _weapon)
        {
               
            weaponholder[ID] = _weapon;
            ChangeWeapon(ID);
        }
        /// <summary>
        /// RemoveWeapon - 
        /// Elimina el arma del weaponholder, reemplazandola con un Null.
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveWeapon(int ID)
        {
            weaponholder[ID] = null;
        }
        /// <summary>
        /// ChangeWeapon - 
        /// A traves del ID se selecciona el arma que utiliza el player.
        /// Si no hay arma en el ID recibido, no hace nada.
        /// </summary>
        /// <param name="ID">ID enviado por el player para determinar el arma elegida.</param>
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
