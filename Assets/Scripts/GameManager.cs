using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public const string New_Line = "\n";

    private List<string> logList = new List<string>();

    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private InputActionSO[] inputActionsArray;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("w");
        }

        Instance = this;
    }
    void Start()
        {
            DisplayRoomText();
        }
    
    public InputActionSO[] GetInputActions()
    {
        return inputActionsArray;
    }

    public void DisplayRoomText()
    {
        ClearAllCollections();
        

        string roomDescription = RoomManager.Instance.currentRoom.description + New_Line;
        string exitDescription = string.Join(New_Line, RoomManager.Instance.GetExitDescriptionInRoom());
        string itemDescription = string.Join(New_Line, RoomManager.Instance.GetItemDescription());

        string fullText = New_Line + roomDescription + New_Line + exitDescription;
        UpdateLogList(fullText);
    }

    public void UpdateLogList(string addText)
    {
        logList.Add(addText);
        DisplayText();

    }

    public void ClearAllCollections()
    {
        RoomManager.Instance.ClearExits();
        RoomManager.Instance.ClearItems();
    }

    private void DisplayText()
    {
        displayText.text = string.Join(New_Line, logList.ToArray());
    }

}
