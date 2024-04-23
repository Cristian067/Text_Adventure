using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public RoomSO currentRoom;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("w");
        }

        Instance = this; 
    }

    // Start is called before the first frame update
    
}
