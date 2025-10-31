using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextDisplayer126 : MonoBehaviour
{
    public TMPro.TMP_Text text;

    public string defaultText;

    float nextCharTimer = 0;
    public float nextCharDelayMs = 15;

    [SerializeField] ScriptableText[] scriptableTexts = { };

    void OnEnable()
    {

        ShowText(defaultText);
    }


    void LateUpdate()
    {
        //Billboard (always face the camera)
        text.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

        if (text.maxVisibleCharacters <= text.text.Length)
        {
            if (nextCharTimer < nextCharDelayMs * .001f)
            {
                nextCharTimer += Time.deltaTime;
            }
            else
            {
                nextCharTimer = 0;
                text.maxVisibleCharacters++;
            }

        }
    }

    public void ShowText(string text)
    {
        this.text.maxVisibleCharacters = 0;
        this.nextCharTimer = 0;
        this.text.text = text;
    }

    public void ShowTextWithID(string id)
    {
        foreach (var st in scriptableTexts)
        {
            if (st.ID == id)
            {
                this.ShowText(st.text);
            }
        }
    }

    [System.Serializable]
    class ScriptableText
    {
        public string ID = "<ID>";
        [Multiline()]
        public string text;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextDisplayer126))]
    public class CustomInspector : Editor
    {
        public string dbgText;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            dbgText = GUILayout.TextField(dbgText);
            if (GUILayout.Button("Custom Text"))
            {
                (target as TextDisplayer126).ShowTextWithID(dbgText);
            }
        }
    }
    #endif
}
