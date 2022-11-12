#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;

[ExecuteAlways]
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
        SetGUIKeyboard();

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

        GUILayout.Space(10);

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
                m_IsoWorldManager.SetWorldManagerStatic();

                GUILayout.Space(10);

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

    #region Input Keyboard

    private string m_KeyboardCode = "";

    private const string KEY_U = "w";
    private const string KEY_D = "s";
    private const string KEY_L = "a";
    private const string KEY_R = "d";

    private const string KEY_ADD = "j";
    private const string KEY_DEL = "k";

    private const string KEY_LOCK = "l";
    private bool m_Lock = false;

    private void SetGUIKeyboard()
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

        GUIStyle m_StyleInstrctionL = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleRight,
        };

        GUIStyle m_StyleInstrctionR = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleLeft,
        };

        GUILayout.Label("KEYBOARD", m_StyleLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Input", m_Style, GUILayout.Width(position.width / 5));
        m_KeyboardCode = GUILayout.TextField("");
        GUILayout.Label(m_Lock ? "LOCK" : "UN-LOCK", m_Style, GUILayout.Width(position.width / 5));
        GUILayout.EndHorizontal();

        if (m_KeyboardCode == KEY_LOCK)
        {
            m_Lock = !m_Lock;
        }

        if (m_Lock == false)
        {
            switch (m_KeyboardCode)
            {
                case KEY_U:
                    //...
                    Debug.Log(KEY_U);
                    break;
                case KEY_D:
                    //...
                    Debug.Log(KEY_D);
                    break;
                case KEY_L:
                    //...
                    Debug.Log(KEY_L);
                    break;
                case KEY_R:
                    //...
                    Debug.Log(KEY_R);
                    break;

                case KEY_ADD:
                    //...
                    Debug.Log(KEY_ADD);
                    break;
                case KEY_DEL:
                    //...
                    Debug.Log(KEY_DEL);
                    break;
            }
        }

        GUILayout.Label("Instruction", m_Style);

        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Move", GUILayout.Width(position.width / 2)), m_StyleInstrctionL);
        GUILayout.Label(string.Format("{0} {1} {2} {3}", KEY_U.ToUpper(), KEY_D.ToUpper(), KEY_L.ToUpper(), KEY_R.ToUpper()), m_StyleInstrctionR, GUILayout.Width(position.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Add", GUILayout.Width(position.width / 2)), m_StyleInstrctionL);
        GUILayout.Label(string.Format("{0}", KEY_ADD.ToUpper()), m_StyleInstrctionR, GUILayout.Width(position.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Del", GUILayout.Width(position.width / 2)), m_StyleInstrctionL);
        GUILayout.Label(string.Format("{0}", KEY_DEL.ToUpper()), m_StyleInstrctionR, GUILayout.Width(position.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Lock", GUILayout.Width(position.width / 2)), m_StyleInstrctionL);
        GUILayout.Label(string.Format("{0}", KEY_LOCK.ToUpper()), m_StyleInstrctionR, GUILayout.Width(position.width / 2));
        GUILayout.EndHorizontal();
    }

    #endregion

    #region Block(s)

    private string m_BlocksPath = "Blocks";

    private const int c_BlockHorizontalMax = 4;

    private const int c_BlockVerticallMax = 4;

    private int m_Page = 0;

    private int m_ChoiceIndex = 0;

    private void SetGUIBlocksPath()
    {
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.Label("BLOCKS", m_Style);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Path", m_Style, GUILayout.Width(position.width / 5));

        m_BlocksPath = EditorGUILayout.TextField("", m_BlocksPath);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Get Block(s)"))
        {
            IsoWorldManager.SetBlock(GitResources.GetResourcesPrefab(m_BlocksPath));
        }

        SetGUIBlocksMatrix();
    }

    private void SetGUIBlocksMatrix()
    {
        if (IsoWorldManager.Blocks == null)
        {
            m_Page = 0;
            m_ChoiceIndex = 0;
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            m_Page = 0;
            m_ChoiceIndex = 0;
            return;
        }

        GUIStyle m_StyleLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.BeginVertical();

        int m_Index = m_Page * c_BlockHorizontalMax * c_BlockVerticallMax;

        float m_Width = position.width / c_BlockHorizontalMax - 4f;
        float m_Height = m_Width;

        for (int i = 0; i < c_BlockVerticallMax; i++)
        {
            GUILayout.BeginHorizontal();

            for (int j = 0; j < c_BlockHorizontalMax; j++)
            {
                if (m_Index > IsoWorldManager.Blocks.Count - 1)
                {
                    GUI.backgroundColor = Color.clear;
                    GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                }
                else
                {
                    Sprite m_Sprite = IsoWorldManager.Blocks[m_Index].GetComponent<SpriteRenderer>().sprite;

                    if (m_Sprite.texture.isReadable == true)
                    {
                        Texture2D m_Texture = GitSprite.GetTextureConvert(m_Sprite);

                        GUIContent m_Content = new GUIContent("", (Texture)m_Texture);

                        if (m_Index == m_ChoiceIndex)
                        {
                            GUI.backgroundColor = Color.white;

                            GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                        }
                        else
                        {
                            GUI.backgroundColor = Color.clear;

                            if (GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height)))
                            {
                                m_ChoiceIndex = m_Index;
                            }
                        }
                    }
                    else
                    {
                        GUI.backgroundColor = Color.clear;
                        GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                        Debug.LogFormat("{0}: Need enable \"Read/Write Enable\" in Inspector.", m_Sprite.texture.name);
                    }
                }

                m_Index++;
            }

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.white;

        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Button("Prev", GUILayout.Width(m_Width));
        GUILayout.Label(string.Format("Page {0}", m_Page), m_StyleLabel);
        GUILayout.Button("Next", GUILayout.Width(m_Width));
        GUILayout.EndHorizontal();
    }

    #endregion
}

#endif