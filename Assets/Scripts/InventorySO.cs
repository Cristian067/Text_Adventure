using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Inventory")]
public class InventorySO : InputActionSO
{
    public override void RespondToInput(string[] separatedInput)
    {
        GameManager.Instance.DisplayInventory();
    }
}
