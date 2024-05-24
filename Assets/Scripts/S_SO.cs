using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/S")]
public class S_SO : ScriptableObject
{

    public string sName;
    [TextArea] public string description;
    public Color color;
    public string type;
    public Interaction[] interactions;

}
