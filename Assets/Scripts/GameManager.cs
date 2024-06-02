using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public const string NEW_LINE = "\n";
    public const string ASTERISK = "*";
    public const string SPACE = " ";

    private List<string> logList = new List<string>();

    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private InputActionSO[] inputActionsArray;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance");
        }

        Instance = this;
    }

    private void Start()
    {
        DisplayFullRoomText();
    }

    public InputActionSO[] GetInputActions()
    {
        return inputActionsArray;
    }

    public void DisplayFullRoomText()
    {
        ClearAllCollectionsForNewRoom();
        
        string roomDescription = RoomManager.Instance.currentRoom.description + NEW_LINE;
        string exitDescriptions = string.Join(NEW_LINE, RoomManager.Instance.GetExitDescriptionsInRoom());
        string itemDescriptions = string.Join(NEW_LINE, RoomManager.Instance.GetItemDescriptionsInRoom());
        
        // Opcional: si itemDescriptions es "", no mostrar + NEW_LINE + itemDescriptions
        string fullText = roomDescription + exitDescriptions + NEW_LINE + itemDescriptions;
        UpdateLogList(fullText);
    }

    public void UpdateLogList(string stringToAdd)
    {
        logList.Add(stringToAdd + NEW_LINE);
        DisplayLoggedText();
    }

    private void DisplayLoggedText()
    {
        displayText.text = string.Join(NEW_LINE, logList);
    }

    private void ClearAllCollectionsForNewRoom()
    {
        RoomManager.Instance.ClearExits();
        RoomManager.Instance.ClearItems();
    }

    public void DisplayInventory()
    {
        string message = "You look in your bag. Inside you find:" + NEW_LINE;
        string separator = ASTERISK + SPACE;
        message += separator + string.Join(NEW_LINE + separator, Inventory.Instance.GetInventory());
        
        UpdateLogList(message);
    }
}
