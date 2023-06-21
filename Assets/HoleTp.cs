using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTp : MonoBehaviour
{

    [SerializeField] Transform Tp_Point;
    [SerializeField] GameObject Player;
    [SerializeField] string Playertag;
    [SerializeField] GameManager manager;
    [SerializeField] float TimesToTp;
    float timesTouched;
    [SerializeField] Transform Tp_Escape;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
        Player = manager.playerInstance;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Playertag)
        {
            if (timesTouched < TimesToTp)
            {
                Player.transform.position = Tp_Point.position;
                timesTouched++;
            }
            else
            {
                Player.transform.position = Tp_Escape.position;
            }


        }
    }

}
