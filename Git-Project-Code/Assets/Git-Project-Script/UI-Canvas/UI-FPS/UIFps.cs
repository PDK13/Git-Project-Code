using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFps : MonoBehaviour
{
    [SerializeField] private bool m_Time_Delay = false;

    [SerializeField] private float f_Time_Delay = 0.1f;

    private float f_DeltaTime = 0.0f;
    private float f_Fps = 0.0f;

    private void Start()
    {
        StartCoroutine(Set_Test());
    }

    private IEnumerator Set_Test()
    {
        yield return null;

        do
        {
            f_DeltaTime += Time.deltaTime;
            f_DeltaTime /= 2.0f;
            f_Fps = 1.0f / f_DeltaTime;

            if (GetComponent<Text>())
            {
                GetComponent<Text>().text = "FPS: " + ((int)f_Fps).ToString();
            }
            else
            if (GetComponent<TextMeshProUGUI>())
            {
                GetComponent<TextMeshProUGUI>().text = "FPS: " + ((int)f_Fps).ToString();
            }

            if (m_Time_Delay)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(f_Time_Delay);
            }
        }
        while (1 == 1);
    }
}
