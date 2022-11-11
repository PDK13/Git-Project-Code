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

        if (IsoWorldManager.Blocks.Count > 0)
        {
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

                        //var croppedTexture = new Texture2D((int)m_Sprite.rect.width, (int)m_Sprite.rect.height);
                        //var pixels = m_Sprite.texture.GetPixels((int)m_Sprite.textureRect.x,
                        //                                         (int)m_Sprite.textureRect.y,
                        //                                         (int)m_Sprite.textureRect.width,
                        //                                         (int)m_Sprite.textureRect.height);
                        //croppedTexture.SetPixels(pixels);
                        //croppedTexture.Apply();

                        //GUIContent m_Content = new GUIContent((Texture)croppedTexture);

                        GUIContent m_Content = new GUIContent(m_Sprite.associatedAlphaSplitTexture);

                        //GUILayout.Button("", GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                        GUILayout.Button(m_Content, GUILayout.Width(m_Width), GUILayout.Height(m_Height));
                    }

                    m_Index++;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }
        else 
        {
            m_Page = 0;
        }
    }

    #endregion
}

#endif