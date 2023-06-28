using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler class 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Handler <T>
{
    T[] myArray;
    int current = 0;
    /// <summary>
    /// Contructor de la clase
    /// </summary>
    /// <param name="_array"></param>
    public Handler(T[] _array)
    {
        myArray = _array;
        current = 0;

    }
    /// <summary>
    /// Add Item - 
    /// Añade un objeto generico al arreglo en cierto index</summary>
    /// Destino en el array del nuevo objeto<param name="index"></param>
    /// Objeto a insertar en el array.<param name="item"></param>
    public void AddItem(int index, T item)
    {
        myArray[index] = item;
    }
    /// <summary>
    /// Set Null Item
    /// - Vuelve null el espacio del array indicado.</summary>
    /// <param name="index"></param>
    public void SetNullItem(int index)
    {
        myArray[index] = default(T);
    }
    /// <summary>
    /// Apunta al siguiente index del array.
    /// </summary>
    public void Next()
    {
        current++;
        if(current>=myArray.Length)
        {
            current = 0;
        }
    }
    /// <summary>
    /// Select - 
    /// Devuelve el objeto con el index indicado</summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T Select(int index)
    {
        return myArray[index];
    }

    /// <summary>
    /// GetCurrent
    /// - Retorna el objeto actualmente apuntado.</summary>
    /// <returns></returns>
    public T GetCurrent()
    {
        return myArray[current];
    }


    /// <summary>
    /// ArraySize
    /// - Retorna el el ultimo index del  array.</summary>
    /// <returns></returns>
    public int ArraySize()
    {
        return myArray.Length - 1;
    }


}
