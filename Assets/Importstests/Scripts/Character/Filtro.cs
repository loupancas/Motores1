using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tools;

public class Filtro : MonoBehaviour
{
    [SerializeField] Enemigo_Chaser chaser;
    [SerializeField] Enemigo_Shooter dispara;
    [SerializeField] Enemigo_Teletransport teletransporte;
    [SerializeField] Flyer flyer;
    Enemy[] enemies;


    private void Start()
    {
        enemies = new Enemy[4];

        var eC = Instantiate(chaser);
        var eD = Instantiate(dispara);
        var eT = Instantiate(teletransporte);
        var eF = Instantiate(flyer);

        enemies[0] = eC;
        enemies[1] = eD;
        enemies[2] = eT;
        enemies[3] = eF;



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CheckClosest();
        }
    }

    public void CheckClosest()
    {
        Enemy mostClose = enemies.GetMostClosest(this.transform.position, Filtrar);

        Debug.Log(mostClose.gameObject.name);
    }

    bool Filtrar(Enemy Enem)
    {
        return true;

       
    }
}
