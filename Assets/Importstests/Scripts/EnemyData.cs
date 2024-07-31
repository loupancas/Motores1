using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    //TPFinal - Lourdes Pando - Struct posiciones de enemigos
    public Enemy enemy;
    public Vector3 position;

    public EnemyData(Enemy enemy, Vector3 position)
    {
        this.enemy = enemy;
        this.position = position;
    }
}