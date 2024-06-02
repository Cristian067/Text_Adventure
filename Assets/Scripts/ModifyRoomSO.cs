using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Action Responses/Modify Room")]
public class ModifyRoomSO : ActionResponseSO
{
    public RoomSO roomModified;
    
    public override bool DoActionResponse()
    {
        if (RoomManager.Instance.currentRoom.roomName.Equals(requiredString))
        {
            RoomManager.Instance.ChangeRoom(roomModified);
            return true;
        }

        return false;
    }
}
