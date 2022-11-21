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
        GUILayout.Label("Manager", m_Style, GUILayout.Width(position.width / 5));
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

                SetGUIFile();

                SetGUIKeyboard();

                SetGUICurson();
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

    #region Curson

    private const string CURSON_NAME = "Iso-Map-Curson";

    private GameObject m_Curson;

    private void SetGUICurson()
    {
        if (m_Curson == null)
        {
            if (m_IsoWorldManagerGameObject.transform.GetChild(0).name == CURSON_NAME)
            {
                m_Curson = m_IsoWorldManagerGameObject.transform.GetChild(0).gameObject;
            }
            else
            {
                m_Curson = GitGameObject.SetGameObjectCreate(CURSON_NAME, m_IsoWorldManagerGameObject.transform);
            }
        }

        if (IsoWorldManager.Blocks == null)
        {
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            return;
        }

        if (m_Curson.GetComponent<SpriteRenderer>() == null)
        {
            m_Curson.AddComponent<SpriteRenderer>();
        }

        Sprite m_Sprite = IsoWorldManager.Blocks[m_ChoiceIndex].GetComponent<SpriteRenderer>().sprite;
        Color m_Color = IsoWorldManager.Blocks[m_ChoiceIndex].GetComponent<SpriteRenderer>().color;
        m_Curson.GetComponent<SpriteRenderer>().sprite = m_Sprite;
        m_Curson.GetComponent<SpriteRenderer>().color = m_Color;

        if (m_Curson.GetComponent<IsoBlock>() == null)
        {
            m_Curson.AddComponent<IsoBlock>();
        }

        if (m_Curson.GetComponent<IsoBlock>().WorldManager == null)
        {
            m_Curson.GetComponent<IsoBlock>().WorldManager = m_IsoWorldManager;
        }

        if (m_Curson.GetComponent<IsoBlock>().Scale != IsoWorldManager.Scale)
        {
            m_Curson.GetComponent<IsoBlock>().Scale = IsoWorldManager.Scale;
        }
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

        GUILayout.BeginHorizontal();
        GUILayout.Label("", m_Style, GUILayout.Width(position.width / 5));
        if (GUILayout.Button("Get"))
        {
            IsoWorldManager.SetBlock(GitResources.GetResourcesPrefab(m_BlocksPath));
        }
        GUILayout.EndHorizontal();

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

                    Texture2D m_Texture = GitSprite.GetTextureConvert(m_Sprite);

                    if (m_Texture != null)
                    {
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
        GUILayout.EndVertical();

        GUI.backgroundColor = Color.white;

        GUILayout.BeginHorizontal();
        GUILayout.Button("Prev", GUILayout.Width(m_Width));
        GUILayout.Label(string.Format("Page {0}", m_Page), m_StyleLabel);
        GUILayout.Button("Next", GUILayout.Width(m_Width));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.clear;
        GUILayout.Button(IsoWorldManager.Blocks[m_ChoiceIndex].name);
        GUILayout.Button(IsoWorldManager.Blocks[m_ChoiceIndex].GetComponent<IsoBlock>().Type.ToString(),  GUILayout.Width(position.width / 3));
        GUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
    }

    #endregion

    #region File

    private string m_Path = "";

    private string m_File = "";

    private void SetGUIFile()
    {
        if (IsoWorldManager.Blocks == null)
        {
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            return;
        }

        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.Space(10);

        GUILayout.Label("FILE", m_Style);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Path", m_Style, GUILayout.Width(position.width / 5));
        m_Path = EditorGUILayout.TextField("", m_Path);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("File", m_Style, GUILayout.Width(position.width / 5));
        m_File = EditorGUILayout.TextField("", m_File);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("", m_Style, GUILayout.Width(position.width / 5));
        if (GUILayout.Button("New"))
        {
            //...
        }
        if (GUILayout.Button("Open"))
        {
            //...
        }
        if (GUILayout.Button("Save"))
        {
            //...
        }
        GUILayout.EndHorizontal();
    }

    #endregion

    #region Input Keyboard

    private string m_KeyboardCode = "";
    private string m_KeyboardCodeLast;

    private const string KEY_U = "w";
    private const string KEY_D = "s";
    private const string KEY_L = "a";
    private const string KEY_R = "d";
    private const string KEY_T = "r";
    private const string KEY_B = "f";

    private const string KEY_ADD = "j";
    private const string KEY_DEL = "k";

    private const string KEY_LOCK = "l";
    private bool m_Lock = false;

    private void SetGUIKeyboard()
    {
        if (IsoWorldManager.Blocks == null)
        {
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            return;
        }

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

        GUILayout.Space(10);

        GUILayout.Label("KEYBOARD", m_Style);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Input", m_Style, GUILayout.Width(position.width / 5));
        m_KeyboardCode = GUILayout.TextField("");
        GUILayout.Label(m_Lock ? "LOCK" : "", m_Style, GUILayout.Width(position.width / 5));
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
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Up;
                    Debug.Log(KEY_U);
                    break;
                case KEY_D:
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Down;
                    Debug.Log(KEY_D);
                    break;
                case KEY_L:
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Left;
                    Debug.Log(KEY_L);
                    break;
                case KEY_R:
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Right;
                    Debug.Log(KEY_R);
                    break;
                case KEY_T:
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Top;
                    Debug.Log(KEY_T);
                    break;
                case KEY_B:
                    m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Bot;
                    Debug.Log(KEY_B);
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
        GUILayout.Label(string.Format("{0} {1} {2} {3} - {4} {5}",
            KEY_U.ToUpper(), 
            KEY_D.ToUpper(), 
            KEY_L.ToUpper(), 
            KEY_R.ToUpper(), 
            KEY_T.ToUpper(), 
            KEY_B.ToUpper()), 
            m_StyleInstrctionR, 
            GUILayout.Width(position.width / 2));
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
}

#endif