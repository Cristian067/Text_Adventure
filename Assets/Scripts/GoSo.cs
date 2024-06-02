using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Go")]
public class GoSO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToChangeRoom(separatedInput[1]); // suponemos que separatedInput[1] = direction
    }
}
