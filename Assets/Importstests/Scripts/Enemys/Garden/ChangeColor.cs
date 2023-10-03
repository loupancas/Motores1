using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] Color colortochange;
    [SerializeField] GameManager gamemanager;

    [SerializeField] GardenHandler gardenhandler;


    public void Start()
    {
        Object = this.gameObject;

        gardenhandler = FindObjectOfType<GardenHandler>();
        gamemanager= FindObjectOfType<GameManager>();

        gardenhandler.Enemys.Add(this.gameObject);

        
    }

    public void ChangeColorEvent()
    {
        Object.GetComponent<MeshRenderer>().material.color = colortochange;
    }

    public void DestroyThisShite()
    {
        gardenhandler.Enemys.Remove(this.gameObject);

        Destroy(this.gameObject);
    }


}
