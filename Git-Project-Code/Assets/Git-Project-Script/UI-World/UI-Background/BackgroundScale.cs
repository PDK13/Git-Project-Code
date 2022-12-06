using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScale : MonoBehaviour
{
    [SerializeField] private GitResolution.GitSizeUnitScaleType m_SpriteScale = GitResolution.GitSizeUnitScaleType.Span;

    [SerializeField] private List<SpriteRenderer> m_Primarys;

    [SerializeField] private GameObject m_Camera;

    private Vector2 m_ResolutionPrimary = new Vector2();

    private void Start()
    {
        if (m_Camera == null) m_Camera = Camera.main.gameObject;

        if (m_Camera != null)
        {
            m_ResolutionPrimary = GitResolution.GetCameraSizeUnit(m_Camera.GetComponent<Camera>());
        }
    }

    private void LateUpdate()
    {
        if (m_Camera == null) return;

        if (GitResolution.GetCameraSizeUnit(m_Camera.GetComponent<Camera>()) == m_ResolutionPrimary) return;

        m_ResolutionPrimary = GitResolution.GetCameraSizeUnit(m_Camera.GetComponent<Camera>());

        foreach (SpriteRenderer m_Primary in m_Primarys)
        {
            m_Primary.size = GitResolution.GetSizeUnitScaled(
                GitSprite.GetSpriteSizeUnit(m_Primary.sprite),
                m_ResolutionPrimary,
                m_SpriteScale);
        }
    }
}