using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    public ItemData bombData;


    public void Collect()
    {

        Inventario.instance.Add(bombData);
        Destroy(gameObject);

    }

}
