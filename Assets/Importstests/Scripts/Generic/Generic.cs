using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Weapon[] inventory = { new Weapon("Arma1"), new Weapon("Arma2") };

        //Handler<Weapon> armory = new Handler<Weapon>(inventory);
       // armory.GetCurrent().Shoot();

    }


    public class Handler<T>
    {
        T[] myArray;
        int current = 0;

        public Handler(T[] _array)
        {
            myArray = _array;
            current = 0;

        }

        public void Nex()
        {
            current++;
            if (current >= myArray.Length)
            {
                current = 0;
            }
        }

        public T GetCurrent()
        {
            return myArray[current];
        }




    }

}


