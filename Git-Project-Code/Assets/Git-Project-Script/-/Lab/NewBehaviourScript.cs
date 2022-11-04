using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GitSizeUnitScaleType m_SpriteScale;

    private SpriteRenderer m_SpriteRendererPrimary;

    [SerializeField] SpriteRenderer m_SpriteRendererTarket;

    private void Start()
    {
        m_SpriteRendererPrimary = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        m_SpriteRendererPrimary.size = GitResolution.GetSizeUnitScaled( m_SpriteRendererPrimary.sprite, m_SpriteRendererTarket.sprite, m_SpriteScale);
    }
}