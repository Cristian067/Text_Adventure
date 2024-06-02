using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private List<string> inventory = new List<string>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }

        Instance = this;
    }

    public List<string> GetInventory()
    {
        return inventory;
    }

    public bool IsItemInInventory(string item)
    {
        return inventory.Contains(item);
    }

    public void AddItemToInventory(string itemToAdd)
    {
        inventory.Add(itemToAdd);
    }
}
