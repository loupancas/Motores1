using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class junctionboxscript : Enemy
{
    public UnityEvent JunctionDeath;
    public UnityEvent JunctionStart;
    public GameManager GameManager;


    protected override void Attack()
    {
        //unused
    }

    protected override void Death()
    {
        JunctionDeath.Invoke();


    }

    protected override void introduction()
    {
        //unused
    }

    protected override void Move()
    {
        //unused
    }

    protected override void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        JunctionStart.Invoke();
        player = GameManager.playerInstance.transform;
       
    }

    
}
