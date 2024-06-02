using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Shoot")]
public class ShootSO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToShootBow(separatedInput[1]);
    }
}
