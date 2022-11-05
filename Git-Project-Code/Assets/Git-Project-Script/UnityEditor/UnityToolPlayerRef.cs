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
        GUIStyle m_StyleLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
        };

        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.Label("TYPE", m_StyleLabel);

        GUILayout.Label(m_ChoiceCurrent.ToUpper(), m_Style);

        SetGUIButtonType();

        GUILayout.Label("REF", m_StyleLabel);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Name", m_Style, GUILayout.Width(position.width / 5));

        //m_Name = EditorGUILayout.TextField("", m_Name, GUILayout.Height(m_ButtonHeight));
        m_Name = EditorGUILayout.TextField("", m_Name);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Value", m_Style, GUILayout.Width(position.width / 5));

        m_Value = EditorGUILayout.TextField("", m_Value);

        GUILayout.EndHorizontal();

        GUILayout.Label("OPPTION", m_StyleLabel);

        //GUILayout.Space(m_SpaceHeight);
        //GUILayout.Label("SET & GET", m_Style);
        //GUILayout.Space(m_SpaceHeight);

        GUILayout.BeginHorizontal();

        SetGUIButtonSet();

        SetGUIButtonGet();

        GUILayout.EndHorizontal();

        //GUILayout.Space(m_SpaceHeight);
        //GUILayout.Label("CLEAR", m_Style);
        //GUILayout.Space(m_SpaceHeight);

        GUILayout.BeginHorizontal();

        SetGUIButtonClear();

        SetGUIButtonClearAll();

        GUILayout.EndHorizontal();
    }

    private void SetGUIButtonType()
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < m_Choice.Count; i++)
        {
            if (m_Choice[i].Equals(m_ChoiceCurrent))
            {
                string m_ButtonText = "[ " + m_Choice[i] + " ]";

                GUILayout.Button(m_ButtonText, GUILayout.Width(position.width / m_Choice.Count));
            }
            else
            {
                if (GUILayout.Button(m_Choice[i], GUILayout.Width(position.width / m_Choice.Count)))
                {
                    m_ChoiceCurrent = m_Choice[i];
                }
            }
        }

        GUILayout.EndHorizontal();
    }

    private void SetGUIButtonSet()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonHorizontalCount)))
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

    private void SetGUIButtonGet()
    {
        if (GUILayout.Button("Get", GUILayout.Width(position.width / m_ButtonHorizontalCount)))
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
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / m_ButtonHorizontalCount)))
        {
            Debug.LogWarningFormat("Tool: Clear {0} = {1} Value!", m_Name, m_Value);

            GitPlayerPref.SetPlayerPrefsClear(m_Name);

            //m_Name = "";
            //m_Value = "";
        }
    }

    public void SetGUIButtonClearAll()
    {
        if (GUILayout.Button("Clear All", GUILayout.Width(position.width / m_ButtonHorizontalCount)))
        {
            Debug.LogWarning("Tool: Clear all Value!");

            GitPlayerPref.SetPlayerPrefsClearAll();

            //m_Name = "";
            //m_Value = "";
        }
    }
}

#endif