using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    //patricio malvasio (uso de diccionarios)

    public static BuffManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
    }

    [SerializeField] BuffItem[] buffsDB; // creamos array de buffs referentes (osea, el significado de cada buff)
    Dictionary<string, BuffItem> registry = new Dictionary<string, BuffItem>(); // creacion de diccionario que indica que buffo hay activo


    private void Start()
    {
        for (int i = 0; i < buffsDB.Length; i++) 
        {
            registry.Add(buffsDB[i].ID, buffsDB[i]); // añadimos buffs al registro de buffs activos
            buffsDB[i].SUbscribeToFInish(OnFinish); // subscribimos el buff al evento de terminar
        }
    }


    public void ExecuteBuff(string ID)
    {
        if (registry.ContainsKey(ID))
        {
            registry[ID].Begin(); // llamamos el script del buff para que comienze a correr
        }
    }

    void OnFinish(string ID)
    {
        if (registry.ContainsKey(ID)) //check si contenemos el buff indicado
        {
            registry[ID].End(); // finalizado de buff
        }
    }


}
