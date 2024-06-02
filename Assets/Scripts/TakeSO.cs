using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Take")]
public class TakeSO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        string response = RoomManager.Instance.TryToTakeItem(separatedInput[1]);
        GameManager.Instance.UpdateLogList(response);
    }
}
