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

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = manager.playerInstance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player || other.CompareTag("Player"))
        {
            triggerEvent.Invoke();
        }


    }



}
