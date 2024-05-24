using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public RoomSO currentRoom;

    private List<ItemSO> itemsInRoom = new List<ItemSO>();
    private List<string> itemDescriptionsInRoom = new List<string>();
    private List<ItemSO> usableItems = new List<ItemSO>();

    private Dictionary<string, RoomSO> exitsDictionary = new Dictionary<string, RoomSO>();
    private Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    private Dictionary<string, ActionResponseSO> useDictionary = new Dictionary<string, ActionResponseSO>();


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("w");
        }

        Instance = this; 
    }

    public List<string> GetExitDescriptionInRoom()
    {
        List<string> exitDescription = new List<string>();
        foreach(Exit exit in currentRoom.exits)
        {
            exitDescription.Add(exit.description);
            exitsDictionary.Add(exit.direction, exit.room);
        }
        return exitDescription;
    }


    private void SetItemsInRoom()
    {
        foreach (ItemSO item in currentRoom.items)
        {
            if (!Inventory.Instance.IsItemInInventory(item.itemName))
            {
                itemsInRoom.Add(item);
                itemDescriptionsInRoom.Add(item.description);
            }
            foreach (Interaction interaction in item.interactions)
            {
                if (interaction.inputAction.keyword.Equals("examinar"))
                {
                    examineDictionary.Add(item.itemName, interaction.responseDescription);
                }
            }
        }
    }

    public List<string> GetItemDescription()
    {
        SetItemsInRoom();
        return itemDescriptionsInRoom;
    }



   

    public void TryToChangeRoom(string direction)
    {
        if (exitsDictionary.TryGetValue(direction, out var room))
        {
            GameManager.Instance.UpdateLogList($"Vas hacia {direction}");
            ChangeRoom(room);
        }
        else
        {
            GameManager.Instance.UpdateLogList($"No hay nada en {direction}");
        }

    }

    public string TryToExamineItem(string item)
    {

        if (examineDictionary.ContainsKey(item))
        {
            return examineDictionary[item];
        }
        return $"No puedes examinar {item}";
    }

    public string TryToTakeItem(string item)
    {
        if (!takeDictionary.ContainsKey(item))
        {
            RemoveItemInRoom(GetItemInRoomFromName(item));
            Inventory.Instance.AddItemToInventory(item);
            
        }
        return null;
    }

    public void ClearExits()
    {
        exitsDictionary.Clear();
    }
    public void ClearItems()
    {
        itemsInRoom.Clear();
        itemDescriptionsInRoom.Clear();
        examineDictionary.Clear();
        //takedirectory == clear
    }

    public ItemSO GetItemInRoomFromName(string itemName)
    {

        foreach(ItemSO item in currentRoom.items)
        {

            if (itemName.Equals(item.itemName))
            {
                return item;
            }

        }
        return null;

    }

    private ItemSO GetUsableItemFromName(string itemName)
    {
        foreach (ItemSO item in usableItems)
        {
            if (itemName.Equals(item.itemName))
            {
                return item;
            }
        }
        return null;
    }

    private void RemoveItemInRoom(ItemSO item)
    {
        itemsInRoom.Remove(item);
    }

    public void ChangeRoom(RoomSO newroom)
    {
        currentRoom = newroom;

        GameManager.Instance.DisplayRoomText();
    }

}
