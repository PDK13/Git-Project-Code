#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnityToolRef : EditorWindow
{
    private const int m_ButtonMainHorizontalCount = 2;

    [MenuItem("Git-Project-Script Tools/Player-Ref Tool")]
    public static void Init()
    {
        GetWindow<UnityToolRef>("[!] Player-Ref Tool");
    }

    private void OnGUI()
    {
        SetGUIType();

        SetGUIRef();

        SetGUIOption();

        SetGUIList();
    }

    #region Type

    private readonly List<string> m_Choice = new List<string>() { "String", "Int", "Float" };

    private string m_ChoiceCurrent = "String";

    private void SetGUIType()
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

        GUILayout.Space(10f);
    }

    private void SetGUIButtonType()
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < m_Choice.Count; i++)
        {
            if (i < m_Choice.Count - 1)
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
            else
            {
                if (m_Choice[i].Equals(m_ChoiceCurrent))
                {
                    string m_ButtonText = "[ " + m_Choice[i] + " ]";

                    GUILayout.Button(m_ButtonText);
                }
                else
                {
                    if (GUILayout.Button(m_Choice[i]))
                    {
                        m_ChoiceCurrent = m_Choice[i];
                    }
                }
            }
        }

        GUILayout.EndHorizontal();
    }

    #endregion

    #region Ref

    private string m_Name = "";

    private string m_Value = "";

    private void SetGUIRef()
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

        GUILayout.Label("REF", m_StyleLabel);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Name", m_Style, GUILayout.Width(position.width / 5));

        m_Name = EditorGUILayout.TextField("", m_Name);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Value", m_Style, GUILayout.Width(position.width / 5));

        m_Value = EditorGUILayout.TextField("", m_Value);

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
    }

    #endregion

    #region Option

    private void SetGUIOption()
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

        GUILayout.Label("OPPTION", m_StyleLabel);

        GUILayout.BeginHorizontal();

        SetGUIButtonSet();

        SetGUIButtonGet();

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        SetGUIButtonClear();

        SetGUIButtonClearAll();

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
    }

    private void SetGUIButtonSet()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonMainHorizontalCount)))
        {
            if (m_Name.Equals(""))
            {
                Debug.LogWarning("Tool: Name Ref is Emty!");
            }
            else
            if (m_Value.Equals(""))
            {
                Debug.LogWarning("Tool: Value Ref is Emty!");
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
        if (GUILayout.Button("Get"))
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

    private void SetGUIButtonClear()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / m_ButtonMainHorizontalCount)))
        {
            Debug.LogWarningFormat("Tool: Clear {0} = {1} Value!", m_Name, m_Value);

            GitPlayerPref.SetPlayerPrefsClear(m_Name);

            //m_Name = "";
            //m_Value = "";
        }
    }

    private void SetGUIButtonClearAll()
    {
        if (GUILayout.Button("Clear All"))
        {
            Debug.LogWarning("Tool: Clear all Value!");

            GitPlayerPref.SetPlayerPrefsClearAll();

            //m_Name = "";
            //m_Value = "";
        }
    }

    #endregion

    #region List

    private const int m_ButtonListHorizontalCount = 6;

    private List<string> m_Refs = new List<string>();

    private string GetGUIListPath()
    {
        return GitFile.GetPath(GitFile.Path.Assets, "Ref-Data.txt", @"/..");
    }

    private void SetGUIListDataRead()
    {
        m_Refs = new List<string>();

        string m_FileDataPath = GetGUIListPath();

        if (GitFile.GetPathFileExist(m_FileDataPath))
        {
            string m_RefNameGet = "";

            GitFileIO m_FileIO = new GitFileIO();
            m_FileIO.SetReadDataStart(m_FileDataPath);
            do
            {
                m_RefNameGet = m_FileIO.GetReadDataAutoString();
                //Debug.LogFormat("Tool: Found Ref Name: {0}", m_RefNameGet);
                if (m_RefNameGet != "") m_Refs.Add(m_RefNameGet);
            }
            while (m_RefNameGet != "" );
        }
        else
        {
            GitFileIO m_FileIO = new GitFileIO();
            m_FileIO.SetWriteDataStart(m_FileDataPath);
        }
    }

    private void SetGUIListDataSave()
    {
        string m_FileDataPath = GetGUIListPath();

        GitFileIO m_FileIO = new GitFileIO();
        for (int i = 0; i < m_Refs.Count; i++) m_FileIO.SetWriteDataAdd(m_Refs[i]);
        m_FileIO.SetWriteDataStart(m_FileDataPath);
    }

    private void SetGUIList()
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

        GUILayout.Label("LIST", m_StyleLabel);

        if (GUILayout.Button("Refresh")) SetGUIListDataRead();

        SetGUIListMemory();

        GUILayout.Space(10f);
    }

    private void SetGUIListMemory()
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

        for (int i = 0; i < m_Refs.Count + 1; i++)
        {
            GUILayout.BeginHorizontal();

            if (i < m_Refs.Count)
            {
                GUILayout.Label(m_Refs[i], m_StyleLabel);

                if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonListHorizontalCount)))
                {
                    if (m_Name != "")
                    {
                        m_Refs[i] = m_Name;
                        SetGUIListDataSave();
                    }
                    else
                        Debug.LogWarning("Tool: Name Ref is Emty!");
                }

                if (GUILayout.Button("Get", GUILayout.Width(position.width / m_ButtonListHorizontalCount)))
                {
                    m_Name = m_Refs[i];
                }

                if (GUILayout.Button("Del", GUILayout.Width(position.width / m_ButtonListHorizontalCount)))
                {
                    m_Refs[i] = "";
                    m_Refs.RemoveAt(i);
                    SetGUIListDataSave();
                    break;
                }
            }
            else
            {
                GUILayout.Label("", m_StyleLabel);

                if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonListHorizontalCount)))
                {
                    if (m_Name != "")
                    {
                        m_Refs.Add(m_Name);
                        SetGUIListDataSave();
                    }
                    else
                        Debug.LogWarning("Tool: Name Ref is Emty!");
                }

                GUILayout.Button("", GUILayout.Width(position.width / m_ButtonListHorizontalCount));
                GUILayout.Button("", GUILayout.Width(position.width / m_ButtonListHorizontalCount));
            }

            GUILayout.EndHorizontal();
        }
    }

    #endregion
}

#endif