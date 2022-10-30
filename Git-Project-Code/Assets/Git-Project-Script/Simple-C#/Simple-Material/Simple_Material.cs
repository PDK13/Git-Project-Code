using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Material : MonoBehaviour
{
    private const string m_PropertyName = "_Color"; //Open a Material with NotePad to check its Property(s)

    private MeshRenderer m_MeshRenderer;

    private List<Color> m_Color = new List<Color>() { Color.red, Color.yellow, Color.green };

    private int m_ColorIndex = -1;

    private void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_ColorIndex++;

            if (m_ColorIndex > m_Color.Count - 1)
            {
                m_ColorIndex = 0;
            }

            SetMaterialColor(m_Color[m_ColorIndex]);
        }
    }

    private void SetMaterialColor(Color m_Color)
    {
        MaterialPropertyBlock m_MaterialBlock = new MaterialPropertyBlock();

        this.m_MeshRenderer.SetPropertyBlock(m_MaterialBlock);

        int id = Shader.PropertyToID(m_PropertyName);

        m_MaterialBlock.SetColor(id, m_Color);

        this.m_MeshRenderer.SetPropertyBlock(m_MaterialBlock);
    }
}
