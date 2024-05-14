using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{

    public string itemName;
    [TextArea] public string description;
    public Interaction[] interactions;
   
}
