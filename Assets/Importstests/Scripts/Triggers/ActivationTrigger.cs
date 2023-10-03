using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActivationTrigger : MonoBehaviour
{
    [Header("References y variables")]
    public  GameManager manager;
    public GameObject player;
    public UnityEvent triggerEvent;
    [SerializeField] bool Singleton, IsSingleton;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = manager.playerInstance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player || other.CompareTag("Player") )
        {
            if(Singleton == false && IsSingleton == true)
            {
                triggerEvent.Invoke();
                Singleton = true;
            }
            else if (IsSingleton== false)
            {
                triggerEvent.Invoke();
            }

        }


    }



}
