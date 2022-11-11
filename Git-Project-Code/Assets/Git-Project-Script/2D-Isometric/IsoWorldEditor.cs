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

    #region Block(s)

    private string m_BlocksPath = "Blocks";

    private const int c_BlockHorizontalMax = 4;

    private const int c_BlockVerticallMax = 4;

    private int m_Page = 0;

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
            return;
        }

        if (IsoWorldManager.Blocks.Count == 0)
        {
            m_Page = 0;
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
                    GUILayout.Button("...", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                }
                else
                {
                    Sprite m_Sprite = IsoWorldManager.Blocks[m_Index].GetComponent<SpriteRenderer>().sprite;

                    if (m_Sprite.texture.isReadable == true)
                    {
                        Texture2D m_Texture = new Texture2D((int)m_Sprite.rect.width, (int)m_Sprite.rect.height);

                        var pixels = m_Sprite.texture.GetPixels(
                            (int)m_Sprite.textureRect.x,
                            (int)m_Sprite.textureRect.y,
                            (int)m_Sprite.textureRect.width,
                            (int)m_Sprite.textureRect.height);
                        m_Texture.SetPixels(pixels);
                        m_Texture.Apply();

                        GUIContent m_Content = new GUIContent(string.Format(" -{0}", m_Index), (Texture)m_Texture);

                        GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                    }
                    else
                    {
                        GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                        Debug.LogFormat("{0}: Need enable \"Read/Write Enable\" in Inspector.", m_Sprite.texture.name);
                    }
                }

                m_Index++;
            }

            GUILayout.EndHorizontal();
        }

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