#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityToolPlayerRef : EditorWindow
{
    private string s_Name = "";

    private string s_Value = "";

    private List<string> l_Choice = new List<string>() { "String", "Int", "Float" };

    private string s_Choice = "String";

    private const int i_Button_Horizontal_Count = 4;

    [MenuItem("Git-Project-Script Tools/Player-Ref Tool")]
    public static void Init()
    {
        GetWindow<UnityToolPlayerRef>("[!] Player-Ref Tool");
    }

    private void OnGUI()
    {
        GUILayout.Space(5f);

        Set_GUI_Button_Type();

        GUILayout.Space(5f);

        s_Name = EditorGUILayout.TextField("Name", s_Name);

        GUILayout.Space(5f);

        s_Value = EditorGUILayout.TextField("Value", s_Value);

        GUILayout.Space(5f);

        GUILayout.BeginHorizontal();

        Set_GUI_Button_Set();

        Set_GUI_Button_Get();

        Set_GUI_Button_Clear();

        Set_GUI_Button_Clear_All();

        GUILayout.EndHorizontal();

        GUILayout.Space(5f);
    }

    private void Set_GUI_Button_Type()
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < l_Choice.Count; i++)
        {
            if (l_Choice[i].Equals(s_Choice))
            {
                string s_Button_Text = "[ " + l_Choice[i] + " ]";

                GUILayout.Button(s_Button_Text, GUILayout.Width(position.width / l_Choice.Count), GUILayout.Height(50f));
            }
            else
            {
                if (GUILayout.Button(l_Choice[i], GUILayout.Width(position.width / l_Choice.Count), GUILayout.Height(50f)))
                {
                    s_Choice = l_Choice[i];
                }
            }
        }

        GUILayout.EndHorizontal();
    }

    private void Set_GUI_Button_Set()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / i_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            if (s_Name.Equals(""))
            {
                Debug.LogError("Tool: Name is Emty!");
            }
            else
            if (s_Value.Equals(""))
            {
                Debug.LogError("Tool: Value is Emty!");
            }
            else
            if (s_Choice == l_Choice[0])
            {
                Class_Scene.Set_PlayerPrefs(s_Name, s_Value);
            }
            else
                if (s_Choice == l_Choice[1])
            {
                Class_Scene.Set_PlayerPrefs(s_Name, int.Parse(s_Value));
            }
            else
                if (s_Choice == l_Choice[2])
            {
                Class_Scene.Set_PlayerPrefs(s_Name, float.Parse(s_Value));
            }
        }
    }

    private void Set_GUI_Button_Get()
    {
        if (GUILayout.Button("Get", GUILayout.Width(position.width / i_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            if (s_Name.Equals(""))
            {
                Debug.LogError("Tool: Name is Emty!");
            }
            else
            if (s_Choice == l_Choice[0])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(s_Name))
                {
                    s_Value = Class_Scene.Get_PlayerPrefs_String(s_Name);
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    s_Value = "";
                }
            }
            else
            if (s_Choice == l_Choice[1])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(s_Name))
                {
                    s_Value = Class_Scene.Get_PlayerPrefs_Int(s_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    s_Value = "";
                }
            }
            else
            if (s_Choice == l_Choice[2])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(s_Name))
                {
                    s_Value = Class_Scene.Get_PlayerPrefs_Float(s_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    s_Value = "";
                }
            }
        }
    }

    public void Set_GUI_Button_Clear()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / i_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            Debug.LogWarningFormat("Tool: Clear {0} = {1} Value!", s_Name, s_Value);

            Class_Scene.Set_PlayerPrefs_Clear(s_Name);

            s_Name = "";
            s_Value = "";
        }
    }

    public void Set_GUI_Button_Clear_All()
    {
        if (GUILayout.Button("Clear All", GUILayout.Width(position.width / i_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            Debug.LogWarning("Tool: Clear all Value!");

            Class_Scene.Set_PlayerPrefs_Clear_All();

            s_Name = "";
            s_Value = "";
        }
    }
}

#endif