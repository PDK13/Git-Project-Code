#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnityToolPlayerRef : EditorWindow
{
    private string m_Name = "";

    private string m_Value = "";

    private readonly List<string> m_Choice = new List<string>() { "String", "Int", "Float" };

    private string m_Choice_Cur = "String";

    private const float m_Button_Horizontal_Count = 2f;

    [MenuItem("Git-Project-Script Tools/Player-Ref Tool")]
    public static void Init()
    {
        GetWindow<UnityToolPlayerRef>("[!] Player-Ref Tool");
    }

    private void OnGUI()
    {
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        GUILayout.Space(10);
        GUILayout.Label("------ TYPE ------", m_Style);
        GUILayout.Space(10);

        GUILayout.Space(10);
        GUILayout.Label(m_Choice_Cur.ToUpper(), m_Style);
        GUILayout.Space(10);

        Set_GUI_Button_Type();

        GUILayout.Space(10);
        GUILayout.Label("------ REF ------", m_Style);
        GUILayout.Space(10);

        m_Name = EditorGUILayout.TextField("Name", m_Name);

        GUILayout.Space(5f);

        m_Value = EditorGUILayout.TextField("Value", m_Value);

        GUILayout.Space(10);
        GUILayout.Label("------ OPPTION ------", m_Style);
        GUILayout.Space(10);

        GUILayout.Space(10);
        GUILayout.Label("SET & GET", m_Style);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        Set_GUI_Button_Set();

        Set_GUI_Button_Get();

        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("CLEAR", m_Style);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        Set_GUI_Button_Clear();

        Set_GUI_Button_Clear_All();

        GUILayout.EndHorizontal();

        GUILayout.Space(5f);
    }

    private void Set_GUI_Button_Type()
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < m_Choice.Count; i++)
        {
            if (m_Choice[i].Equals(m_Choice_Cur))
            {
                string s_Button_Text = "[ " + m_Choice[i] + " ]";

                GUILayout.Button(s_Button_Text, GUILayout.Width(position.width / m_Choice.Count), GUILayout.Height(50f));
            }
            else
            {
                if (GUILayout.Button(m_Choice[i], GUILayout.Width(position.width / m_Choice.Count), GUILayout.Height(50f)))
                {
                    m_Choice_Cur = m_Choice[i];
                }
            }
        }

        GUILayout.EndHorizontal();
    }

    private void Set_GUI_Button_Set()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / m_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            if (m_Name.Equals(""))
            {
                Debug.LogError("Tool: Name is Emty!");
            }
            else
            if (m_Value.Equals(""))
            {
                Debug.LogError("Tool: Value is Emty!");
            }
            else
            if (m_Choice_Cur == m_Choice[0])
            {
                Class_Scene.Set_PlayerPrefs(m_Name, m_Value);
            }
            else
                if (m_Choice_Cur == m_Choice[1])
            {
                Class_Scene.Set_PlayerPrefs(m_Name, int.Parse(m_Value));
            }
            else
                if (m_Choice_Cur == m_Choice[2])
            {
                Class_Scene.Set_PlayerPrefs(m_Name, float.Parse(m_Value));
            }
        }
    }

    private void Set_GUI_Button_Get()
    {
        if (GUILayout.Button("Get", GUILayout.Width(position.width / m_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            if (m_Name.Equals(""))
            {
                Debug.LogError("Tool: Name is Emty!");
            }
            else
            if (m_Choice_Cur == m_Choice[0])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(m_Name))
                {
                    m_Value = Class_Scene.Get_PlayerPrefs_String(m_Name);
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
            else
            if (m_Choice_Cur == m_Choice[1])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(m_Name))
                {
                    m_Value = Class_Scene.Get_PlayerPrefs_Int(m_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
            else
            if (m_Choice_Cur == m_Choice[2])
            {
                if (Class_Scene.Get_PlayerPrefs_isExist(m_Name))
                {
                    m_Value = Class_Scene.Get_PlayerPrefs_Float(m_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
        }
    }

    public void Set_GUI_Button_Clear()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / m_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            Debug.LogWarningFormat("Tool: Clear {0} = {1} Value!", m_Name, m_Value);

            Class_Scene.Set_PlayerPrefs_Clear(m_Name);

            m_Name = "";
            m_Value = "";
        }
    }

    public void Set_GUI_Button_Clear_All()
    {
        if (GUILayout.Button("Clear All", GUILayout.Width(position.width / m_Button_Horizontal_Count), GUILayout.Height(50f)))
        {
            Debug.LogWarning("Tool: Clear all Value!");

            Class_Scene.Set_PlayerPrefs_Clear_All();

            m_Name = "";
            m_Value = "";
        }
    }
}

#endif