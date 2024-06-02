using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponseSO : ScriptableObject
{
    public string requiredString;
    public abstract bool DoActionResponse();
}
