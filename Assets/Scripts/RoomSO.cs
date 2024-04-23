using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/room")]

public class RoomSO : ScriptableObject

{
    public string name;
    [TextArea] public string description;

}
