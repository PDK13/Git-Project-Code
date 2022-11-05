#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnityToolRef : EditorWindow
{
    private string m_Name = "";

    private string m_Value = "";

    private readonly List<string> m_Choice = new List<string>() { "String", "Int", "Float" };

    private string m_ChoiceCurrent = "String";

    private const float m_ButtonHorizontalCount = 2f;

    [MenuItem("Git-Project-Script Tools/Player-Ref Tool")]
    public static void Init()
    {
        GetWindow<UnityToolRef>("[!] Player-Ref Tool");
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
        GUILayout.Label(m_ChoiceCurrent.ToUpper(), m_Style);
        GUILayout.Space(10);

        SetGUIButton_Type();

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

        SetGUIButton_Set();

        SetGUIButton_Get();

        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("CLEAR", m_Style);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        SetGUIButtonClear();

        SetGUIButtonClearAll();

        GUILayout.EndHorizontal();

        GUILayout.Space(5f);
    }

    private void SetGUIButton_Type()
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < m_Choice.Count; i++)
        {
            if (m_Choice[i].Equals(m_ChoiceCurrent))
            {
                string m_Button_Text = "[ " + m_Choice[i] + " ]";

                GUILayout.Button(m_Button_Text, GUILayout.Width(position.width / m_Choice.Count), GUILayout.Height(50f));
            }
            else
            {
                if (GUILayout.Button(m_Choice[i], GUILayout.Width(position.width / m_Choice.Count), GUILayout.Height(50f)))
                {
                    m_ChoiceCurrent = m_Choice[i];
                }
            }
        }

        GUILayout.EndHorizontal();
    }

    private void SetGUIButton_Set()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonHorizontalCount), GUILayout.Height(50f)))
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
            if (m_ChoiceCurrent == m_Choice[0])
            {
                GitPlayerPref.SetPlayerPrefs(m_Name, m_Value);
            }
            else
                if (m_ChoiceCurrent == m_Choice[1])
            {
                GitPlayerPref.SetPlayerPrefs(m_Name, int.Parse(m_Value));
            }
            else
                if (m_ChoiceCurrent == m_Choice[2])
            {
                GitPlayerPref.SetPlayerPrefs(m_Name, float.Parse(m_Value));
            }
        }
    }

    private void SetGUIButton_Get()
    {
        if (GUILayout.Button("Get", GUILayout.Width(position.width / m_ButtonHorizontalCount), GUILayout.Height(50f)))
        {
            if (m_Name.Equals(""))
            {
                Debug.LogError("Tool: Name is Emty!");
            }
            else
            if (m_ChoiceCurrent == m_Choice[0])
            {
                if (GitPlayerPref.GetPlayerPrefsExist(m_Name))
                {
                    m_Value = GitPlayerPref.GetPlayerPrefsString(m_Name);
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
            else
            if (m_ChoiceCurrent == m_Choice[1])
            {
                if (GitPlayerPref.GetPlayerPrefsExist(m_Name))
                {
                    m_Value = GitPlayerPref.GetPlayerPrefsInt(m_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
            else
            if (m_ChoiceCurrent == m_Choice[2])
            {
                if (GitPlayerPref.GetPlayerPrefsExist(m_Name))
                {
                    m_Value = GitPlayerPref.GetPlayerPrefsFloat(m_Name).ToString();
                }
                else
                {
                    Debug.LogWarning("Tool: Not found Value!");
                    m_Value = "";
                }
            }
        }
    }

    public void SetGUIButtonClear()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / m_ButtonHorizontalCount), GUILayout.Height(50f)))
        {
            Debug.LogWarningFormat("Tool: Clear {0} = {1} Value!", m_Name, m_Value);

            GitPlayerPref.SetPlayerPrefsClear(m_Name);

            //m_Name = "";
            //m_Value = "";
        }
    }

    public void SetGUIButtonClearAll()
    {
        if (GUILayout.Button("Clear All", GUILayout.Width(position.width / m_ButtonHorizontalCount), GUILayout.Height(50f)))
        {
            Debug.LogWarning("Tool: Clear all Value!");

            GitPlayerPref.SetPlayerPrefsClearAll();

            //m_Name = "";
            //m_Value = "";
        }
    }
}

#endif