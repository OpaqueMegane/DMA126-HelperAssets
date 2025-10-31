using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class YarnHelper126 : MonoBehaviour
{

    private void Awake()
    {
        DialogueRunner dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();
    }

    public static void ChangeVariable(string action)
    {
        try
        {

            string variableName = null;
            var parts = action.Split('+', '-', '=', '*', '/');
            variableName = parts[0].Trim();
            var op = action;
            foreach (var part in parts)
            {
                op = op.Replace(part, "");
            }
  

            DialogueRunner dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();

            float finalValue = 0;

            dialogueRunner.VariableStorage.TryGetValue(variableName, out finalValue);


            string numPortion = parts[1].Trim();
            float operand = 0;
            if (float.TryParse(numPortion, out operand))
            {
            }
            else if (dialogueRunner.VariableStorage.TryGetValue(numPortion, out operand))
            {
            }
            else
            {
                Debug.LogError($"Couldn't parse");
            }

            if (op == "+")
            {
                finalValue += operand;
            }
            else if (op == "-")
            {
                finalValue -= operand;
            }
            else if (op == "*")
            {
                finalValue *= operand;
            }
            else if (op == "/")
            {
                if (operand == 0)
                {
                    finalValue = 0;
                }
                else
                {
                    finalValue /= operand;
                }
            }
            else if (op == "=")
            {
                finalValue = operand;
            }

            dialogueRunner.VariableStorage.SetValue(variableName, finalValue);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}
