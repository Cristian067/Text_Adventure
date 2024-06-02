using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Room")]
public class RoomSO : ScriptableObject
{
    public string roomName;
    [TextArea] public string description;
    public Exit[] exits;
    public ItemSO[] items;
}
