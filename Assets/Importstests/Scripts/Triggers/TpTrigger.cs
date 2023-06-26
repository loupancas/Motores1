using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpTrigger : MonoBehaviour
{
    [SerializeField] Transform Tp_Point;
    [SerializeField] GameObject Player;
    [SerializeField] string Playertag;
    [SerializeField] GameManager manager;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
        Player = manager.playerInstance;

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == Playertag)
        {
            Player.transform.position = Tp_Point.position;

        }
    }

}



