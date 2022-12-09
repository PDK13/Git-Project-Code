using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Simple_Script))]
public class Simple_ScriptCustomEditor : Editor
{
    private SerializedProperty m_AnotherVaribleA; //Stored Varible

    private SerializedProperty m_AnotherVaribleB;
    private SerializedProperty m_AnotherVaribleC;

    private SerializedProperty m_AnotherVariblePath;

    private void OnEnable()
    {
        m_AnotherVaribleA = serializedObject.FindProperty("m_VaribleA"); //Find Varible

        m_AnotherVaribleB = serializedObject.FindProperty("m_VaribleB");
        m_AnotherVaribleC = serializedObject.FindProperty("m_VaribleC");

        m_AnotherVariblePath = serializedObject.FindProperty("m_VariblePath");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); //...???

        //=================================================== Begin Check

        EditorGUI.BeginChangeCheck(); //Begin Check

        EditorGUILayout.PropertyField(m_AnotherVaribleA); //Show varible

        Simple_Script m_Temp = (target as Simple_Script); //"Target" mean GameObject

        if (m_Temp.m_VaribleA == Simple_Script.Option.Option1) 
        {
            m_Temp.SetOptionDebug(); //Call Methode from Sript or Component
        }

        EditorGUI.EndChangeCheck();

        //=================================================== End Check

        //EditorGUILayout.PropertyField(m_AnotherVaribleB);
        EditorGUILayout.PropertyField(m_AnotherVaribleC);

        EditorGUILayout.PropertyField(m_AnotherVariblePath);

        serializedObject.ApplyModifiedProperties(); //Apply All Chance...???
    }

    private void OnSceneGUI()
    {
        Simple_Script m_Temp = (target as Simple_Script);

        if (m_Temp != null)
        {
            //Draw some line from GameObject

            Vector3[] m_Path = m_Temp.GetPath();

            Handles.color = Color.red; //Color line
            Handles.DrawPolyLine(m_Path); //Draw line

            for (int i = 0; i < m_Path.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                Vector3 m_HandlePos = Handles.PositionHandle(m_Path[i], Quaternion.identity); //...???

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(m_Temp, "change path"); //...???

                    m_Temp.SetPath(m_HandlePos, i);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}