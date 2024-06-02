using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Examine")]
public class ExamineSO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        string response = RoomManager.Instance.TryToExamineItem(separatedInput[1]);
        GameManager.Instance.UpdateLogList(response);
    }
}
