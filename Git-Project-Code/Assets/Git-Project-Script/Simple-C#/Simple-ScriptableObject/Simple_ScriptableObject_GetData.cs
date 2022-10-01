using UnityEngine;

public class Simple_ScriptableObject_GetData : MonoBehaviour
{
    [SerializeField] private Simple_ScriptableObject cm_Simple_ScriptableObject;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.LogFormat("{0}: Data: {1}", name, cm_Simple_ScriptableObject.GetMyString());

        cm_Simple_ScriptableObject.SetMyString("Good bye!");

        Debug.LogFormat("{0}: Data after chance: {1}", name, cm_Simple_ScriptableObject.GetMyString());
    }
}
