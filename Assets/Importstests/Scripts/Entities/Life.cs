using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.LifeSystem
{

public class Life 
{
    private int life;
    public int lifeMax;

    public int Live
    {
        get
        {
            return life;
        }
        set
        {
            if(value<0)
            {
                life = 0;
            }
            else if (value > lifeMax)
            {
                life = lifeMax;
            }
            else
            {
                life = value;
            }
        }

    }




    public Life(int _maxlife) //constructor
    {
        life = _maxlife;
        lifeMax= _maxlife;


    }

    public Life(int _maxlife,int _currentLife) //constructor se puede usar porque tiene una sobrecarga _currentlife
    {
        life = _currentLife;
        lifeMax = _maxlife;

    }





}

}
