using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScale : MonoBehaviour
{
    [SerializeField] private GitSizeUnitScaleType m_SpriteScale = GitSizeUnitScaleType.Span;

    [SerializeField] private List<SpriteRenderer> m_Primarys;

    [SerializeField] private GameObject m_Tarket;

    private void LateUpdate()
    {
        if (m_Tarket.GetComponent<Camera>())
        {
            foreach(SpriteRenderer m_Primary in m_Primarys)
            {
                m_Primary.size = GitResolution.GetSizeUnitScaled(
                    GitResolution.GetSpriteSizeUnit(m_Primary.sprite),
                    GitResolution.GetCameraSizeUnit(),
                    m_SpriteScale);
            }
        }
        else
        if (m_Tarket.GetComponent<SpriteRenderer>())
        {
            foreach (SpriteRenderer m_Primary in m_Primarys)
            {
                m_Primary.size = GitResolution.GetSizeUnitScaled(
                    GitResolution.GetSpriteSizeUnit(m_Primary.sprite),
                    GitResolution.GetSpriteSizeUnit(m_Tarket.GetComponent<SpriteRenderer>().sprite),
                    m_SpriteScale);
            }
        }
    }
}