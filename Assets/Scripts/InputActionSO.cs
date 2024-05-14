
using UnityEngine;

public abstract class InputActionSO : ScriptableObject
{

    public string keyword;
    public abstract void RespondToInput(string[] separatedInput);

}
