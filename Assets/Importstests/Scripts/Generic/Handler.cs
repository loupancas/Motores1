using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handler <T>
{
    T[] myArray;
    int current = 0;
    
    public Handler(T[]_array)
    {
        myArray = _array;
        current = 0;

    }

    public void Nex()
    {
        current++;
        if(current>=myArray.Length)
        {
            current = 0;
        }
    }

    public T GetCurrent()
    {
        return myArray[current];
    }




}
