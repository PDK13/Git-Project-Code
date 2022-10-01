using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFps : MonoBehaviour
{
    [SerializeField] private bool m_TimeDelayIsAllow = false;

    [SerializeField] private float m_Time_Delay = 0.1f;

    private float m_DeltaTime = 0.0f;
    private float m_Fps = 0.0f;

    private void Start()
    {
        StartCoroutine(SetTest());
    }

    private IEnumerator SetTest()
    {
        yield return null;

        do
        {
            m_DeltaTime += Time.deltaTime;
            m_DeltaTime /= 2.0f;
            m_Fps = 1.0f / m_DeltaTime;

            if (GetComponent<Text>())
            {
                GetComponent<Text>().text = "FPS: " + ((int)m_Fps).ToString();
            }
            else
            if (GetComponent<TextMeshProUGUI>())
            {
                GetComponent<TextMeshProUGUI>().text = "FPS: " + ((int)m_Fps).ToString();
            }

            if (m_TimeDelayIsAllow)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(m_Time_Delay);
            }
        }
        while (1 == 1);
    }
}
