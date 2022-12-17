#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnityToolRef : EditorWindow
{
    [MenuItem("Tool/PLAYER-REF")]
    public static void Init()
    {
        GetWindow<UnityToolRef>("PLAYER-REF");
    }

    private void OnGUI()
    {
        SetGUIRef();

        SetGUIOption();

        SetGUIList();
    }

    #region Ref

    private readonly string[] m_Choice = { "String", "Int", "Float" };
    private int m_TypeChoiceIndex = 0;

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

        //Type

        GUILayout.BeginHorizontal();

        GUILayout.Label("Type", m_Style, GUILayout.Width(position.width / 5));

        m_TypeChoiceIndex = EditorGUILayout.Popup("", m_TypeChoiceIndex, m_Choice);

        GUILayout.EndHorizontal();

        //Name

        GUILayout.BeginHorizontal();

        GUILayout.Label("Name", m_Style, GUILayout.Width(position.width / 5));

        m_Name = EditorGUILayout.TextField("", m_Name);

        GUILayout.EndHorizontal();

        //Value

        GUILayout.BeginHorizontal();

        GUILayout.Label("Value", m_Style, GUILayout.Width(position.width / 5));

        m_Value = EditorGUILayout.TextField("", m_Value);

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
    }

    #endregion

    #region Option

    private const int m_ButtonMainHorizontalCount = 4;

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

        SetGUIButtonClear();

        SetGUIButtonClearAll();

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
    }

    private void SetGUIButtonSet()
    {
        if (GUILayout.Button("Set", GUILayout.Width(position.width / m_ButtonMainHorizontalCount)))
        {
            if (!m_Name.Equals("") && !m_Value.Equals(""))
            {
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[0])
                    //String
                    GitPlayerPref.SetPlayerPrefs(m_Name, m_Value);
                else
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[1])
                    //Int
                    GitPlayerPref.SetPlayerPrefs(m_Name, int.Parse(m_Value));
                else
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[2])
                    //Float
                    GitPlayerPref.SetPlayerPrefs(m_Name, float.Parse(m_Value));
            }
        }
    }

    private void SetGUIButtonGet()
    {
        if (GUILayout.Button("Get"))
        {
            if (!m_Name.Equals("") && !m_Value.Equals(""))
            {
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[0])
                    //String
                    m_Value = GitPlayerPref.GetPlayerPrefsString(m_Name);
                else
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[1])
                    //Int
                    m_Value = GitPlayerPref.GetPlayerPrefsInt(m_Name).ToString();
                else
                if (m_Choice[m_TypeChoiceIndex] == m_Choice[2])
                    //Float
                    m_Value = GitPlayerPref.GetPlayerPrefsFloat(m_Name).ToString();
            }
        }
    }

    private void SetGUIButtonClear()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(position.width / m_ButtonMainHorizontalCount)))
            GitPlayerPref.SetPlayerPrefsClear(m_Name);
    }

    private void SetGUIButtonClearAll()
    {
        if (GUILayout.Button("Clear All"))
            GitPlayerPref.SetPlayerPrefsClearAll();
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

        GUILayout.Space(10f);

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
                if (GUILayout.Button(m_Refs[i]))
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
                if (GUILayout.Button("[New]"))
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
            }

            GUILayout.EndHorizontal();
        }
    }

    #endregion
}

#endif