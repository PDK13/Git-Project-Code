#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;

public class UnityToolIsoEditor : EditorWindow
{
    private GameObject m_IsoWorldManagerGameObject;

    private IsoWorldManager m_IsoWorldManager;

    [MenuItem("Tool/ISO-MAP")]
    public static void Init()
    {
        GetWindow<UnityToolIsoEditor>("ISO-MAP");
    }

    private void Update()
    {

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

                SetGUIBlocks();

                SetGUIFile();

                SetGUIKeyboard();

                SetGUICurson();

                SetGUICurrent();
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

        if (m_BlockListByType == null)
        {
            return;
        }

        if (m_BlockListByType.Count == 0)
        {
            return;
        }

        if (m_Curson.GetComponent<SpriteRenderer>() == null)
        {
            m_Curson.AddComponent<SpriteRenderer>();
        }

        Sprite m_Sprite = m_BlockListByType[m_BlockChoiceIndex].GetComponent<SpriteRenderer>().sprite;
        Color m_Color = m_BlockListByType[m_BlockChoiceIndex].GetComponent<SpriteRenderer>().color;
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

    //Block Path

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

        if (GUILayout.Button("Refresh"))
            IsoWorldManager.SetBlock(GitResources.GetResourcesPrefab(m_BlocksPath));

        GUILayout.Space(10);
    }

    //Types

    List<GameObject> m_BlockListByType = new List<GameObject>();

    private int m_TypeChoiceIndex = 0;

    private void SetGUITypeChoice()
    {
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUIStyle m_StyleLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        GUILayout.BeginHorizontal();

        GUILayout.Label("Type", m_Style, GUILayout.Width(position.width / 5));

        string[] m_Choice = GitEnum.GetEnumList<IsoType>().ToArray();

        m_TypeChoiceIndex = EditorGUILayout.Popup("", m_TypeChoiceIndex, m_Choice);

        GUILayout.EndHorizontal();
    }

    //Blocks

    private string m_BlocksPath = "Blocks";

    private const int c_BlockHorizontalMax = 4;

    private const int c_BlockVerticallMax = 2;

    private int m_BlocksPage = 0;

    private int m_BlockChoiceIndex = 0;

    private void SetGUIBlocks()
    {
        if (IsoWorldManager.Blocks == null)
        {
            m_BlocksPage = 0;
            m_BlockChoiceIndex = 0;
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            m_BlocksPage = 0;
            m_BlockChoiceIndex = 0;
            return;
        }

        GUIStyle m_StyleLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        int m_Index = m_BlocksPage * c_BlockHorizontalMax * c_BlockVerticallMax;
        float m_Width = position.width / c_BlockHorizontalMax - 4f;
        float m_Height = m_Width;

        SetGUITypeChoice();

        m_BlockListByType = IsoWorldManager.GetBlock((IsoType)m_TypeChoiceIndex);

        if (m_BlockListByType == null)
        {
            m_BlocksPage = 0;
            m_BlockChoiceIndex = 0;
        }

        if (m_BlockListByType.Count == 0)
        {
            m_BlocksPage = 0;
            m_BlockChoiceIndex = 0;
        }

        SetGUIBlocksList();

        GUI.backgroundColor = Color.white;

        GUILayout.BeginHorizontal();
        int m_PageMax = m_BlockListByType.Count / (c_BlockHorizontalMax * c_BlockVerticallMax);
        if (GUILayout.Button("Prev", GUILayout.Width(m_Width)))
        {
            if (m_BlocksPage > 0) m_BlocksPage--;
        }
        GUILayout.Label(string.Format("Page {0}/{1}", m_BlocksPage, m_PageMax), m_StyleLabel);
        if (GUILayout.Button("Next", GUILayout.Width(m_Width)))
        {
            if (m_BlocksPage < m_PageMax) m_BlocksPage++;
        }
        GUILayout.EndHorizontal();
        //Blocks

        //Imformation
        if (m_BlockListByType.Count > 0)
        {
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.clear;
            GUILayout.Button(m_BlockListByType[m_BlockChoiceIndex].name);
            //GUILayout.Button(m_BlockListByType[m_BlockChoiceIndex].GetComponent<IsoBlock>().Type.ToString(), GUILayout.Width(position.width / 3));
            GUILayout.EndHorizontal();
        }
        //Imformation

        GUI.backgroundColor = Color.white;
    }

    private void SetGUIBlocksList()
    {
        int m_Index = m_BlocksPage * c_BlockHorizontalMax * c_BlockVerticallMax;
        float m_Width = position.width / c_BlockHorizontalMax - 4f;
        float m_Height = m_Width;

        GUILayout.BeginVertical();
        for (int i = 0; i < c_BlockVerticallMax; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < c_BlockHorizontalMax; j++)
            {
                if (m_Index > m_BlockListByType.Count - 1)
                {
                    GUI.backgroundColor = Color.clear;
                    GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                }
                else
                {
                    Sprite m_Sprite = m_BlockListByType[m_Index].GetComponent<SpriteRenderer>().sprite;

                    Texture2D m_Texture = GitSprite.GetTextureConvert(m_Sprite);

                    if (m_Texture != null)
                    {
                        GUIContent m_Content = new GUIContent("", (Texture)m_Texture);

                        if (m_Index == m_BlockChoiceIndex)
                        {
                            GUI.backgroundColor = Color.white;
                            GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                        }
                        else
                        {
                            GUI.backgroundColor = Color.clear;
                            if (GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height)))
                            {
                                m_BlockChoiceIndex = m_Index;
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
    }

    #endregion

    #region File

    private string m_Path = "";

    private string m_File = "";

    private void SetGUIFile()
    {
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
        if (GUILayout.Button("New"))
        {
            //...
            IsoWorldManager.SetWorldDestroy();
            IsoWorldManager.SetWorldGenerate();
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

        GUILayout.Space(10);
    }

    #endregion

    #region Input Keyboard

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

        //Event Keyboard when Tool on focus!!
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    switch (Event.current.keyCode)
                    {
                        //Move Curson!!
                        case KeyCode.UpArrow:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Up;
                            e.Use();
                            break;
                        case KeyCode.DownArrow:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Down;
                            e.Use();
                            break;
                        case KeyCode.LeftArrow:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Left;
                            e.Use();
                            break;
                        case KeyCode.RightArrow:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Right;
                            e.Use();
                            break;
                        case KeyCode.PageUp:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Top;
                            e.Use();
                            break;
                        case KeyCode.PageDown:
                            m_Curson.GetComponent<IsoBlock>().Pos += IsoVector.Bot;
                            e.Use();
                            break;
                        //Block Curson!!
                        case KeyCode.Insert:
                            IsoWorldManager.SetWorldCreate(m_Curson.GetComponent<IsoBlock>().Pos, m_BlockListByType[m_BlockChoiceIndex], null);
                            break;
                        case KeyCode.Delete:
                            IsoWorldManager.SetWorldRemove(m_Curson.GetComponent<IsoBlock>().Pos, m_BlockListByType[m_BlockChoiceIndex].GetComponent<IsoBlock>().Type);
                            break;
                    }
                }
                break;
        }
        //Event Keyboard when Tool on focus!!
    }

    #endregion

    #region Current Pos

    private void SetGUICurrent()
    {
        GUIStyle m_Style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
        };

        float m_Width = position.width / c_BlockHorizontalMax - 4f;
        float m_Height = m_Width;

        GUILayout.Label("CURRENT", m_Style);

        GUILayout.BeginHorizontal();
        //Block Image!!
        GameObject m_BlockCurrent = IsoWorldManager.GetWorldBlock(m_Curson.GetComponent<IsoBlock>().PosMatrix);
        if (m_BlockCurrent != null)
        {
            Sprite m_Sprite = m_BlockCurrent.GetComponent<SpriteRenderer>().sprite;

            Texture2D m_Texture = GitSprite.GetTextureConvert(m_Sprite);

            GUIContent m_Content = new GUIContent("", (Texture)m_Texture);

            GUI.backgroundColor = Color.white;
            GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height));
        }
        else
        {
            GUI.backgroundColor = Color.clear;
            GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
        }
        //Block Image!!
        //Block Imformation!!
        GUI.backgroundColor = Color.clear;
        GUILayout.BeginVertical();
        if (m_BlockCurrent != null)
        {
            //Pos
            GUILayout.BeginHorizontal();
            IsoVector m_Pos = m_BlockCurrent.GetComponent<IsoBlock>().Pos;
            GUILayout.Button("Pos: ", GUILayout.Width(position.width / 5));
            GUILayout.Button(string.Format("[{0};{1};{2}]", m_Pos.X_UD, m_Pos.Y_LR, m_Pos.H_TB));
            GUILayout.EndHorizontal();
            //Name
            GUILayout.BeginHorizontal();
            GUILayout.Button("Name: ", GUILayout.Width(position.width / 5));
            GUILayout.Button(m_BlockCurrent.name);
            GUILayout.EndHorizontal();
            //Type
            GUILayout.BeginHorizontal();
            GUILayout.Button("Type: ", GUILayout.Width(position.width / 5));
            GUILayout.Button(m_BlockCurrent.GetComponent<IsoBlock>().Type.ToString());
            GUILayout.EndHorizontal();
            //...
            GUILayout.BeginHorizontal();
            GUILayout.Button("...", GUILayout.Width(position.width / 5));
            GUILayout.Button("...");
            GUILayout.EndHorizontal();
        }
        else
        {
            //Pos
            GUILayout.BeginHorizontal();
            GUILayout.Button("Pos: ", GUILayout.Width(position.width / 5));
            GUILayout.Button("...");
            GUILayout.EndHorizontal();
            //Name
            GUILayout.BeginHorizontal();
            GUILayout.Button("Name: ", GUILayout.Width(position.width / 5));
            GUILayout.Button("...");
            GUILayout.EndHorizontal();
            //Type
            GUILayout.BeginHorizontal();
            GUILayout.Button("Type: ", GUILayout.Width(position.width / 5));
            GUILayout.Button("...");
            GUILayout.EndHorizontal();
            //...
            GUILayout.BeginHorizontal();
            GUILayout.Button("...", GUILayout.Width(position.width / 5));
            GUILayout.Button("...");
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        //Block Imformatiohn
        GUILayout.EndHorizontal();
    }

    #endregion
}

#endif