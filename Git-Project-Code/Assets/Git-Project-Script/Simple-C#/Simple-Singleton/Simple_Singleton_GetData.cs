using UnityEngine;

public class Simple_Singleton_GetData : MonoBehaviour
{
    private void Start()
    {
        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.GetInstance().m_Number_Public);

        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.GetNumber());

        Simple_Singleton.SetNumber(3);

        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.GetNumber());
    }
}