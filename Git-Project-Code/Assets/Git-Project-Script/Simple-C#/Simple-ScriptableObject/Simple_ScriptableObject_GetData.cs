using UnityEngine;

public class Simple_ScriptableObject_GetData : MonoBehaviour
{
    [SerializeField] private Simple_ScriptableObject m_Simple_ScriptableObject;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.LogFormat("{0}: Data: {1}", name, m_Simple_ScriptableObject.GetMyString());

        m_Simple_ScriptableObject.SetMyString("Good bye!");

        Debug.LogFormat("{0}: Data after chance: {1}", name, m_Simple_ScriptableObject.GetMyString());
    }
}
