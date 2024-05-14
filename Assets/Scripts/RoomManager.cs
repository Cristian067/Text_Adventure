using System.Collections;
using System.Collections.Generic;
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
                if (interaction.inputAction.keyword.Equals("examine"))
                {
                    examineDictionary.Add(item.itemName, interaction.responseDescription);
                }
            }
        }
    }



    public void ChangeRoom(RoomSO newroom)
    {
        currentRoom = newroom;
        GameManager.Instance.DisplayRoomText();
    }

    public void TryToChangeRoom(string direction)
    {
        if (exitsDictionary.TryGetValue(direction, out var room))
        {
            GameManager.Instance.UpdateLogList($"You go to the {direction}");
            ChangeRoom(room);
        }
        else
        {
            GameManager.Instance.UpdateLogList($"There isn't nothing in {direction}");
        }

    }

    public string TryToExamineItem(string item)
    {

        if (examineDictionary.Equals(item))
        {
            return examineDictionary[item];
        }
        return $"you can't examine {item}";
    }

    public void ClearExits()
    {
        exitsDictionary.Clear();
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
    
}
