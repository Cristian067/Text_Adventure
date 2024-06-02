using UnityEngine;
using System;

[Serializable]
public class Interaction
{
    public InputActionSO inputAction;
    [TextArea] public string responseDescription;
    public ActionResponseSO actionResponse;
}
