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

    private void DisplayRoomText()
    {
        string fullText = RoomManager.Instance.currentRoom.description + New_Line;
        UpdateLogList(fullText);
    }

    public void UpdateLogList(string addText)
    {
        logList.Add(addText);
        DisplayText();

    }

    private void DisplayText()
    {
        displayText.text = string.Join(New_Line, logList.ToArray());
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
