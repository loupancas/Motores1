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
    //Enemy[] enemies;
    EnemyData[] enemies;


    private void Start()
    {
        enemies = new EnemyData[4];

        var eC = Instantiate(chaser);
        var eD = Instantiate(dispara);
        var eT = Instantiate(teletransporte);
        var eF = Instantiate(flyer);

        enemies[0] = new EnemyData(eC, eC.transform.position);
        enemies[1] = new EnemyData(eD, eD.transform.position);
        enemies[2] = new EnemyData(eT, eT.transform.position);
        enemies[3] = new EnemyData(eF, eF.transform.position);



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
        EnemyData mostClose = enemies.GetClosestEnemy(this.transform.position, Filtrar);
        Debug.Log(mostClose.enemy.gameObject.name);
    }

    bool Filtrar(EnemyData Enem)
    {
        return true;
    }
}
