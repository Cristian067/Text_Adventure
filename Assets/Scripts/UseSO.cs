using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Use")]
public class UseSO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToUseItem(separatedInput[1]);
    }
}
