using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputHandle : MonoBehaviour
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
            if (inputAction.keyword.Equals(separatedInput[0]))
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

