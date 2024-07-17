using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.WeaponHolder
{/// <summary>
/// Clase WeaponHolder - 
/// Las armas se clasifican segun su ID, y son almacenadas segun el mismo. </summary>
    public class WeaponHolder: MonoBehaviour
    {
        //estos deberian ser objetos Weapon
        public Weapon[] weaponholder;
        public Weapon weapon;
        public Transform weaponpos;
        public new Transform camera;
        public int actualID = 0;
        public Handler<Weapon> weapons;
        //public WeaponHandler.Handler<WeaponHandler.Weapon> armory;
        /// <summary>
        /// Constructor de WeaponHolder
        /// </summary>
        /// <param name="_weaponpos">Posición respecto al player del WeaponHolder.</param>
        /// <param name="_camera">Usado para la dirección de ataque.</param>
        public void Initialize(Transform _weaponpos, Transform _camera)
        {
            weaponpos = _weaponpos;
            camera = _camera;
            weaponholder = new Weapon[10];
            weapons = new Handler<Weapon>(weaponholder);
            weapon = weapons.Select(0);
        }

        /// <summary>
        /// AddWeapon - 
        /// Añade un arma al weaponholder.
        /// </summary>
        /// <param name="ID">ID del arma que determina su posición en el WeaponHolder</param>
        /// <param name="_weapon">El arma.</param>
        public void AddWeapon(int ID, Weapon _weapon)
        {
            if (ID >= 0 && ID < weaponholder.Length)
            {
                weapons.AddItem(ID, _weapon);
                weaponholder[ID] = _weapon;
            }
            else
            {
                Debug.LogError($"ID {ID} fuera de los límites del array de armas de tamaño {weaponholder.Length}");
            }
        }
        /// <summary>
        /// RemoveWeapon - 
        /// Elimina el arma del weaponholder, reemplazandola con un Null.
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveWeapon(int ID)
        {

            if (ID >= 0 && ID < weaponholder.Length)
            {
                weapons.SetNullItem(ID);
                weaponholder[ID] = null;
            }
            else
            {
                Debug.LogError($"ID {ID} fuera de los límites del array de armas de tamaño {weaponholder.Length}");
            }
        }
        /// <summary>
        /// ChangeWeapon - 
        /// A traves del ID se selecciona el arma que utiliza el player.
        /// Si no hay arma en el ID recibido, no hace nada.
        /// </summary>
        /// <param name="ID">ID enviado por el player para determinar el arma elegida.</param>
        public void ChangeWeapon(int ID)
        {
            if (ID >= 0 && ID < weaponholder.Length && weaponholder[ID] != null)
            {
                if (weapon != null)
                {
                    weapon.gameObject.SetActive(false);
                }
                weapon = weaponholder[ID];
                weapon.gameObject.SetActive(true);
                actualID = ID;
            }
            else
            {
                Debug.LogError($"ID {ID} fuera de los límites del array de armas de tamaño {weaponholder.Length} o no hay arma en el índice especificado");
            }
        }

      
    }
}

namespace Entities
{
    public class Handler<T> where T : Weapon
    {
        private T[] myArray;
        private int current;

        public Handler(T[] _array)
        {
            myArray = _array;
            current = -1;
        }

        public T Select(int index)
        {
            if (index >= 0 && index < myArray.Length)
            {
                current = index;
                return myArray[current];
            }
            else
            {
                Debug.LogError($"Index {index} fuera de los límites del array de tamaño {myArray.Length}");
                return default(T);
            }
        }

        public void AddItem(int index, T item)
        {
            if (index >= 0 && index < myArray.Length)
            {
                myArray[index] = item;
            }
            else
            {
                Debug.LogError($"Index {index} fuera de los límites del array de tamaño {myArray.Length}");
            }
        }

        public void SetNullItem(int index)
        {
            if (index >= 0 && index < myArray.Length)
            {
                myArray[index] = null;
            }
            else
            {
                Debug.LogError($"Index {index} fuera de los límites del array de tamaño {myArray.Length}");
            }
        }

        public void UseItem()
        {
            if (current >= 0 && current < myArray.Length && myArray[current] != null)
            {
                myArray[current].Attack();
            }
            else
            {
                Debug.LogError("No hay un arma seleccionada");
            }
        }
    }
}