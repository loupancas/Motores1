using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventario : MonoBehaviour
{
    public static Inventario instance; // singleton
    private void Awake()
    {
        instance = this;
    }
    public static event Action<List<StackItem>> OnInventoryChange;

    public List<StackItem> inventory = new List<StackItem>();
    private Dictionary<ItemData, StackItem> itemDictionary = new Dictionary<ItemData, StackItem>(); // para chequear si existe o no en el inventario

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TryUse(0);
        }
    }


    public void TryUse(int index) //primer indice cero
    {
        if (inventory.Count > 0 && inventory.Count >= index + 1)
        {
            string nameUsable = inventory[index].itemData.UsableName;
            UsableManager.instance.Use(nameUsable);
        }
    }



    public void Add(ItemData itemData)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData.displayName == itemData.displayName)
            {
                inventory[i].addToStack();
                Debug.Log($"{ itemData.displayName} La cantidad total ahora es {inventory.Count + 1}");
                return;
            }
        }

        StackItem newItem = new StackItem(itemData);
        inventory.Add(newItem);
        Debug.Log($"Se añadió { itemData.displayName} por primera vez");

        OnInventoryChange.Invoke(inventory);


    }

    public void RemoveOne(ItemData itemData)
    {
        StackItem stackToRemove = null;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData.displayName == itemData.displayName)
            {
                inventory[i].RemoveFromStack();
                if (inventory[i].StackSize <= 0)
                {
                    stackToRemove = inventory[i];

                }


            }
        }
        if (stackToRemove != null)
        {
            inventory.Remove(stackToRemove);
        }

        OnInventoryChange.Invoke(inventory);
    }

    public void Remove(ItemData itemData)
    {
        StackItem stackToRemove = null;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData.displayName == itemData.displayName)
            {
                stackToRemove = inventory[i];
            }
        }
        inventory.Remove(stackToRemove);
        OnInventoryChange.Invoke(inventory);
    }
}
