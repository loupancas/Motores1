using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
    }

    [SerializeField] BuffItem[] buffsDB;
    Dictionary<string, BuffItem> registry = new Dictionary<string, BuffItem>();

    private void Start()
    {
        for (int i = 0; i < buffsDB.Length; i++)
        {
            registry.Add(buffsDB[i].ID, buffsDB[i]);
            buffsDB[i].SUbscribeToFInish(OnFinish);
        }
    }


    public void ExecuteBuff(string ID)
    {
        if (registry.ContainsKey(ID))
        {
            registry[ID].Begin();
        }
    }

    void OnFinish(string ID)
    {
        if (registry.ContainsKey(ID))
        {
            registry[ID].End();
        }
    }


}
