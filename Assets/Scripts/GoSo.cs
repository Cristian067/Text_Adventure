
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Go")]
public class GoSo : InputActionSO
{

    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToChangeRoom(separatedInput[1]);
    }
    
}
