using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardKeyPressed : MonoBehaviour
{
    // InputField that the keyboard should write to
    public TMP_InputField InputField = null;

    // set Value of buttons in Editor e.g. "A", "B", "1", "Space"
    public string Value;
    private TextMeshProUGUI Text;
    private Button Button;
    private int CaretPosition = 0;

    private void Awake()
    {
        // set Button to GameObjects Button Component and find + assign InputField
        Button = GetComponent<Button>();
        InputField = GameObject.Find("CodeInputField").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        // set Text of buttons and add EventListener to Button
        Text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Text.text = Value;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(AppendValue);
    }

    private void AppendValue()
    {
        // get caretPosition from InputField
        CaretPosition = InputField.caretPosition;

        // check for value and add Space or Letter/Number to InputField or remove last character + update caretPosition
        if (Value == "Space")
        {
            InputField.text = InputField.text.Insert(CaretPosition, " ");
            CaretPosition += 1;

            InputField.caretPosition = CaretPosition;
        }
        else if (Value == "Back")
        {
            if (CaretPosition > 0) 
            {
                InputField.text = InputField.text.Remove(CaretPosition-1, 1);
                CaretPosition -= 1;
            }

            InputField.caretPosition = CaretPosition;
        }
        else 
        {
            InputField.text = InputField.text.Insert(CaretPosition, Value);
            CaretPosition += Value.Length;

            InputField.caretPosition = CaretPosition;
        }
    }
}
