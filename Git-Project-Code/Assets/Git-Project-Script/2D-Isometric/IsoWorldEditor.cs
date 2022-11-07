#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;

public class IsoWorldEditor : EditorWindow
{
    private GameObject m_IsoWorldManagerGameObject;

    private IsoWorldManager m_IsoWorldManager;

    [MenuItem("Git-Project-Script Tools/Iso-Map Tool")]
    public static void Init()
    {
        GetWindow<IsoWorldEditor>("[!] Iso-Map Tool");
    }

    private void OnGUI()
    {
        GUIStyle m_StyleLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
        };

        GUIStyle m_StyleMessage = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.Label("WORLD MANAGER", m_StyleLabel);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Object", m_Style, GUILayout.Width(position.width / 5));

        m_IsoWorldManagerGameObject = (GameObject)EditorGUILayout.ObjectField("", m_IsoWorldManagerGameObject, typeof(GameObject), true);

        GUILayout.EndHorizontal();

        if (m_IsoWorldManagerGameObject != null)
        {
            m_IsoWorldManager = m_IsoWorldManagerGameObject.GetComponent<IsoWorldManager>();

            if (m_IsoWorldManager != null)
            {
                GUILayout.Label("WORLD EDITOR", m_StyleLabel);

                //Editor!

                SetGUIBlocksPath();
            }
            else
            {
                GUILayout.Label("Require Component", m_StyleMessage);
            }
        }
        else
        {
            GUILayout.Label("Require GameObject", m_StyleMessage);
        }
    }

    #region Block(s)

    private string m_BlocksPath = "/Blocks";

    private void SetGUIBlocksPath()
    {
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.Space(10);

        GUILayout.Label("BLOCKS", m_Style);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Path", m_Style, GUILayout.Width(position.width / 5));

        m_BlocksPath = EditorGUILayout.TextField("", m_BlocksPath);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Get Block(s)", GUILayout.Width(position.width / 3)))
        {
            Debug.LogFormat("{0}: Button {1} Pressed!", this.name, i);
        }
    }

    #endregion
}

#endif