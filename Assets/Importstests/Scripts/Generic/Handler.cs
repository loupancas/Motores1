using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler class 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Handler <T>
{
    private T[] myArray;
    private int current;

    /// <summary>
    /// Constructor de la clase Handler.
    /// </summary>
    /// <param name="_array">Array de elementos tipo T.</param>
    public Handler(T[] _array)
    {
        myArray = _array;
        current = 0;
    }

    /// <summary>
    /// Añade un objeto genérico al arreglo en cierto índice.
    /// </summary>
    /// <param name="index">Destino en el array del nuevo objeto.</param>
    /// <param name="item">Objeto a insertar en el array.</param>
    public void AddItem(int index, T item)
    {
        myArray[index] = item;
    }

    /// <summary>
    /// Vuelve nulo el espacio del array indicado.
    /// </summary>
    /// <param name="index">Índice del array a ser nulo.</param>
    public void SetNullItem(int index)
    {
        myArray[index] = default(T);
    }

    /// <summary>
    /// Apunta al siguiente índice del array.
    /// </summary>
    public void Next()
    {
        current++;
        if (current >= myArray.Length)
        {
            current = 0;
        }
    }

    /// <summary>
    /// Devuelve el objeto con el índice indicado.
    /// </summary>
    /// <param name="index">Índice del objeto a seleccionar.</param>
    /// <returns>Objeto tipo T en el índice especificado.</returns>
    public T Select(int index)
    {
        return myArray[index];
    }

    /// <summary>
    /// Retorna el objeto actualmente apuntado.
    /// </summary>
    /// <returns>Objeto tipo T actualmente seleccionado.</returns>
    public T GetCurrent()
    {
        return myArray[current];
    }

    /// <summary>
    /// Retorna el último índice del array.
    /// </summary>
    /// <returns>Último índice del array.</returns>
    public int ArraySize()
    {
        return myArray.Length - 1;
    }

}
