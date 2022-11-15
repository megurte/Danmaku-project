using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SubEffects
{
    /*public class ReflexScript : MonoBehaviour
    {
        public int choose;
        public string method;
        public MonoScript script;
        public List<string> methodList;

        void Start()
        {
            var type = Type.GetType(script.name);
            var go = new GameObject(script.name, type);
            var component = go.GetComponent(type);

            type.GetMethod(method).Invoke(component, null);
        }
    }*/
}
/*#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ReflexScript))]
    class ReflexScriptEditor : UnityEditor.Editor
    {
        SerializedProperty script;
        SerializedProperty method;
        SerializedProperty choose;
        SerializedProperty methodList;
 
        void OnEnable()
        {
            script = serializedObject.FindProperty("script");
            method = serializedObject.FindProperty("method");
            choose = serializedObject.FindProperty("choose");
            methodList = serializedObject.FindProperty("methodList");
        }
 
        override public void OnInspectorGUI()
        {
            serializedObject.Update();
 
            EditorGUILayout.PropertyField(script, new GUIContent("Script"));
            EditorGUILayout.PropertyField(methodList, new GUIContent("Method List"));

 
            var rv = script.objectReferenceValue;
 
            if (rv != null)
            {
                var allMethods = new List<string>();
                var scriptType = Type.GetType((rv as MonoScript).name);
 
                var bf = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
 
                foreach (var value in scriptType.GetMethods(bf))
                {
                    allMethods.Add(value.Name);
                }
 
                allMethods.Sort();
 
                if (method.stringValue != null)
                {
                    int id = allMethods.IndexOf(method.stringValue);
                    choose.intValue = id >= 0 ? id : 0;
                }
 
                if (allMethods.Count > 0)
                {
                    choose.intValue = EditorGUILayout.Popup("Method", choose.intValue, allMethods.ToArray());
                    method.stringValue = allMethods[choose.intValue];
                }
                else
                    EditorGUILayout.LabelField("Does not contain public methods!");
 
            }
 
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}*/