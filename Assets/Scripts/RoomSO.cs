using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/room")]

public class RoomSO : ScriptableObject

{
    public string roomName;
    [TextArea] public string description;
    public Exit[] exits;
    public ItemSO[] items;

}
