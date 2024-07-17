using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    //private void Start()
    //{
    //    // Creación de ejemplos de armas para el inventario
    //    Weapon[] inventory = { new Weapon(), new Weapon() };
    //    Handler<Weapon> armory = new Handler<Weapon>(inventory);

    //    // Uso de métodos de Handler para demostrar funcionalidad
    //    armory.GetCurrent().Shoots(); // Disparar el arma actual
    //    //armory.Next(); // Cambiar al siguiente arma
    //    armory.GetCurrent().Shoots(); // Disparar el siguiente arma
    //}

    //public class Weapon
    //{
    //    public string Name { get; private set; }

    //    public Weapon(string name)
    //    {
    //        Name = name;
    //    }

    //    public virtual void Shoots()
    //    {
    //        Debug.Log($"{Name} está disparando");
    //    }
    //}

    //public class Handler<T> where T : Weapon
    //{
    //    private T[] myArray;
    //    private int current;

    //    public Handler(T[] _array)
    //    {
    //        myArray = _array;
    //        current = -1;
    //    }

    //    public T Select(int index)
    //    {
    //        if (index >= 0 && index < myArray.Length)
    //        {
    //            current = index;
    //            return myArray[current];
    //        }
    //        else
    //        {
    //            Debug.LogError($"Index {index} fuera de los límites del array de tamaño {myArray.Length}");
    //            return default(T);
    //        }
    //    }

    //    public void AddItem(int index, T item)
    //    {
    //        if (index >= 0 && index < myArray.Length)
    //        {
    //            myArray[index] = item;
    //        }
    //        else
    //        {
    //            Debug.LogError($"Index {index} fuera de los límites del array de tamaño {myArray.Length}");
    //        }

    //        void UseItem()
    //        {
    //            if (item != null)
    //            {
    //                item.Attack();
    //            }
    //            else
    //            {
    //                Debug.LogError("No hay un arma seleccionada");
    //            }
    //        }

    //    }      

    //    public int ArraySize()
    //    {
    //        return myArray.Length;
    //    }    

      

    //}


}


