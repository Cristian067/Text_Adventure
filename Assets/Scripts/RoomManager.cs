using System;
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
    private List<ItemSO> shootableItems = new List<ItemSO>();


    private Dictionary<string, RoomSO> exitsDictionary = new Dictionary<string, RoomSO>();
    private Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    private Dictionary<string, ActionResponseSO> useDictionary = new Dictionary<string, ActionResponseSO>();
    private Dictionary<string, ActionResponseSO> shootDictionary = new Dictionary<string, ActionResponseSO>();


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance");
        }

        Instance = this;
    }

    public List<string> GetExitDescriptionsInRoom()
    {
        List<string> exitDescriptions = new List<string>();
        foreach (Exit exit in currentRoom.exits)
        {
            exitDescriptions.Add(exit.description);
            exitsDictionary.Add(exit.direction, exit.room);
        }

        return exitDescriptions;
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
                if (interaction.inputAction.keyWord.Equals("examinar"))
                {
                    examineDictionary.Add(item.itemName, interaction.responseDescription);
                }
                else if (interaction.inputAction.keyWord.Equals("coger"))
                {
                    takeDictionary.Add(item.itemName, interaction.responseDescription);
                }
                else if (interaction.inputAction.keyWord.Equals("usar"))
                {
                    if (!usableItems.Contains(item))
                    {
                        usableItems.Add(item);
                    } 
                } 
                else if (interaction.inputAction.keyWord.Equals("disparar"))
                {
                    if (!shootableItems.Contains(item))
                    {
                        shootableItems.Add(item);
                    }
                }
            }
        }
    }
    
    public List<string> GetItemDescriptionsInRoom()
    {
        SetItemsInRoom();
        return itemDescriptionsInRoom;
    }

    public void TryToChangeRoom(string direction)
    {
        if (exitsDictionary.TryGetValue(direction, out var room))
        {
            GameManager.Instance.UpdateLogList($"Vas hacie el {direction}");
            ChangeRoom(room);
        }
        else
        {
            GameManager.Instance.UpdateLogList($"No hey ningun caminio hacia el {direction}");
        }
    }

    public string TryToExamineItem(string item)
    {
        Debug.Log(item);

        // TODO: Comprobar si también está en el inventario y es examinable
        if (examineDictionary.ContainsKey(item))
        {
            return examineDictionary[item];
        }

        return $"No puedes examinar {item}";
    }
    
    public string TryToTakeItem(string item)
    {
        if (takeDictionary.ContainsKey(item))
        {
            RemoveItemFromRoom(GetItemInRoomFromName(item));
            Inventory.Instance.AddItemToInventory(item);
            SetUseDictionary();
            SetShootDictionary();
            return takeDictionary[item];
        }

        return $"No puedes coger {item}";
    }

    public void TryToUseItem(string itemToUse)
    {
        if (Inventory.Instance.IsItemInInventory(itemToUse))
        {
            if (useDictionary.TryGetValue(itemToUse, out ActionResponseSO actionResponse))
            {
                bool actionResult = actionResponse.DoActionResponse();
                if (!actionResult)
                {
                    GameManager.Instance.UpdateLogList("Nothing happens...");
                }
            }
            else
            {
                GameManager.Instance.UpdateLogList($"You can't use the {itemToUse}");
            }
        }
        else
        {
            GameManager.Instance.UpdateLogList($"There's no {itemToUse} in your inventory to use");
        }
    }

    public void TryToShootBow(string target)
    {
        if (Inventory.Instance.IsItemInInventory("arco"))
        {
            if (shootDictionary.TryGetValue(target, out ActionResponseSO actionResponse))
            {
                bool actionResult = actionResponse.DoActionResponse();
                if (!actionResult)
                {
                    GameManager.Instance.UpdateLogList("Nothing happens...");
                }
            }
            else
            {
                GameManager.Instance.UpdateLogList($"No puedes disparar a {target}");
            }
        }
        else
        {
            GameManager.Instance.UpdateLogList($"No tienes nada con que disparar");
        }

    }

    private void SetUseDictionary()
    {
        foreach (string itemInInventory in Inventory.Instance.GetInventory())
        {
            ItemSO item = GetUsableItemFromName(itemInInventory);
            if (item == null)
            {
                continue;
            }

            foreach (Interaction interaction in item.interactions)
            {
                // Se supone que solo una interaction (y solo una) va a tener actionResponse (asociada a inputAction use)
                if (interaction.actionResponse == null)
                {
                    continue;
                }
                
                // Se supone que si llegamos a esta condición, estamos con la interaction de use
                if (!useDictionary.ContainsKey(itemInInventory))
                {
                    useDictionary.Add(itemInInventory, interaction.actionResponse);
                }
            }
        }
    }

    private void SetShootDictionary()
    {
        foreach (string itemInInventory in Inventory.Instance.GetInventory())
        {
            ItemSO item = GetShootableItemFromName(itemInInventory);
            if (item == null)
            {
                continue;
            }

            foreach (Interaction interaction in item.interactions)
            {
                // Se supone que solo una interaction (y solo una) va a tener actionResponse (asociada a inputAction use)
                if (interaction.actionResponse == null)
                {
                    continue;
                }

                // Se supone que si llegamos a esta condición, estamos con la interaction de use
                if (!shootDictionary.ContainsKey(itemInInventory))
                {
                    shootDictionary.Add(itemInInventory, interaction.actionResponse);
                }
            }
        }
        // Se supone que si llegamos a esta condición, estamos con la interaction de use
        
            
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
        takeDictionary.Clear();
        shootDictionary.Clear();
    }

    private ItemSO GetItemInRoomFromName(string itemName)
    {
        foreach (ItemSO item in currentRoom.items)
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


    private ItemSO GetShootableItemFromName(string itemName)
    {
        foreach (ItemSO item in shootableItems)
        {
            if (itemName.Equals(item.itemName))
            {
                return item;
            }
        }

        return null;
    }


    private void RemoveItemFromRoom(ItemSO item)
    {
        itemsInRoom.Remove(item);
    }

    public void ChangeRoom(RoomSO newRoom)
    {
        currentRoom = newRoom;
        GameManager.Instance.DisplayFullRoomText();
    }
    
}
