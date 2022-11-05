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
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        GUIStyle m_StyleEWarning = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 8
        };

        GUILayout.Space(10);
        GUILayout.Label("------ ISOMETRIC WORLD MANAGER ------", m_Style);
        GUILayout.Space(10);

        m_IsoWorldManagerGameObject = (GameObject)EditorGUILayout.ObjectField("", m_IsoWorldManagerGameObject, typeof(GameObject), true);

        if (m_IsoWorldManagerGameObject != null)
        {
            m_IsoWorldManager = m_IsoWorldManagerGameObject.GetComponent<IsoWorldManager>();

            if (m_IsoWorldManager != null)
            {
                GUILayout.Space(10);
                GUILayout.Label("------ ISOMETRIC WORLD EDITOR ------", m_Style);
                GUILayout.Space(10);

                //Editor!
            }
            else
            {
                GUILayout.Space(10);
                GUILayout.Label("Require Component", m_StyleEWarning);
                GUILayout.Space(10);
            }
        }
        else
        {
            GUILayout.Space(10);
            GUILayout.Label("Require GameObject", m_StyleEWarning);
            GUILayout.Space(10);
        }
    }


}

#endif