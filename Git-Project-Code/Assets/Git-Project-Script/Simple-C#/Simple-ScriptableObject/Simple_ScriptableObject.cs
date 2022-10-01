using UnityEngine;

[CreateAssetMenu(fileName = "My-Script-Object", menuName = "Git-Project-Code/My-Script-Object", order = 0)]
public class Simple_ScriptableObject : ScriptableObject
{
    //Data is stored local
    [SerializeField] private string m_MyString = "Hello World!";

    //Data is stored local can be get normaly
    public void SetMyString(string m_MyString)
    {
        this.m_MyString = m_MyString;
    }

    //Data is stored local can be CHANCE to new value and be SAVED (CAUTION!)
    public string GetMyString()
    {
        return m_MyString;
    }
}