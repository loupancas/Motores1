using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handler <T>
{
    T[] myArray;
    int current = 0;
    
    public Handler(T[] _array)
    {
        myArray = _array;
        current = 0;

    }

    public void AddItem(int index, T item)
    {
        myArray[index] = item;
    }

    public void SetNullItem(int index)
    {
        myArray[index] = default(T);
    }

    public void Next()
    {
        current++;
        if(current>=myArray.Length)
        {
            current = 0;
        }
    }

    public T Select(int index)
    {
        return myArray[index];
    }

    public T GetCurrent()
    {
        return myArray[current];
    }

    public int ArraySize()
    {
        return myArray.Length - 1;
    }


}
