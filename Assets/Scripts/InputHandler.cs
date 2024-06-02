using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptUserInput);
    }

    private void AcceptUserInput(string input)
    {
        input = input.ToLower();
        GameManager.Instance.UpdateLogList(input);
        
        string[] separatedInput = GetSeparatedInput(input);
        foreach (InputActionSO inputAction in GameManager.Instance.GetInputActions())
        {
            // Si la primera palabra del input es una de las palabras clave
            if (inputAction.keyWord.Equals(separatedInput[0]))
            {
                inputAction.RespondToInput(separatedInput);
            }
        }
        
        InputComplete();
    }

    private void InputComplete()
    {
        inputField.ActivateInputField();
        inputField.text = null;
    }

    private string[] GetSeparatedInput(string input)
    {
        return input.Split(" ");
    }
}
