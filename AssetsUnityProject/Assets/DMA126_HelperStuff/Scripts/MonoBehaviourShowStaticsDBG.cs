using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

public class MonoBehaviourShowStaticsDBG : MonoBehaviour
{

}

#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviourShowStaticsDBG), true)]
public class StaticFieldShower : Editor
{
    FieldInfo[] _fis = null;
    //Dictionary<FieldInfo, SerializedObject> _fis2 = null;

    public string getLabel(string fieldName)
    {
        return $"{fieldName} (static)";

    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        

        if (_fis == null)
        {
            _fis = target.GetType().GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            //    _fis2 = new();
            //    foreach(var fi in _fis)
            //    {
            //        _fis2[fi] = new SerializedObject()
            //    }
            //}
        }
        if (_fis.Length > 0)
        {
            GUILayout.Space(5);
            GUILayout.Label("__STATIC FIELDS__");
        }

        foreach (var f in _fis)
        {
            if (f.FieldType == typeof(int))
            {
                var prevVal = (int)f.GetValue(null);
                var newVal = EditorGUILayout.IntField(getLabel(f.Name), prevVal);
                if (newVal != prevVal)
                {
                    f.SetValue(null, newVal);
                }
            }

            if (f.FieldType == typeof(float))
            {
                var prevVal = (float)f.GetValue(null);
                var newVal = EditorGUILayout.FloatField(getLabel(f.Name), prevVal);
                if (newVal != prevVal)
                {
                    f.SetValue(null, newVal);
                }
            }

            if (f.FieldType == typeof(bool))
            {
                var prevVal = (bool)f.GetValue(null);
                var newVal = EditorGUILayout.Toggle(getLabel(f.Name), prevVal);
                if (newVal != prevVal)
                {
                    f.SetValue(null, newVal);
                }
            }

            if (f.FieldType == typeof(string))
            {
                var prevVal = (string)f.GetValue(null);
                var newVal = EditorGUILayout.TextField(getLabel(f.Name), prevVal);
                if (newVal != prevVal)
                {
                    f.SetValue(null, newVal);
                }
            }

            if (f.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                var prevVal = (UnityEngine.Object) f.GetValue(null);
                var newVal = EditorGUILayout.ObjectField(getLabel(f.Name), prevVal, f.FieldType, prevVal);
                if (newVal != prevVal)
                {
                    f.SetValue(null, newVal);
                }
            }
        }
    }
}
#endif
