using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;


public class Player : LifeEntity
{
    Life life;

    protected override void Start()
    {
       base.Start();//life = new Life(100);

        GameManager.instance.playerInstance = gameObject;
    }
    protected override void OnInitialize()
    {
        GameManager.instance.playerInstance = this.gameObject; // se asigna a sí mismo
        GameManager.instance.player=this;
    }
    protected override void OnDeInitialize()
    {
        
    }

    protected override void OnFixedUpdate()
    {
        
    }

    

    protected override void OnPause()
    {
      
    }

    protected override void OnResume()
    {
        
    }

    protected override void OnUpdate()
    {
        

    }


}
