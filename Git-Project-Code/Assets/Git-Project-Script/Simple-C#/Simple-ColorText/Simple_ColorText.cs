using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Simple_ColorText : MonoBehaviour
{
    [SerializeField] private Color m_Color;

    [SerializeField] private TextMeshProUGUI m_Text;

    private void FixedUpdate()
    {
        string m_ColorHexCode = GitColor.GetColorHexCode(m_Color);

        //Option 1:

        //string m_TextString = string.Format("Chance <#{0}>Color</color> in run-time!", m_ColorHexCode);

        //m_Text.text = m_TextString;

        //Option 2:

        string m_TextString = string.Format("Chance {0} in run-time!", GitColor.GetColorHexFormat(m_Color, "Color"));

        m_Text.text = m_TextString;
    }
}
