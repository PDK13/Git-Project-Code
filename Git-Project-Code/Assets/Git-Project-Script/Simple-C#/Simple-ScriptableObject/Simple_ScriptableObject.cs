using UnityEngine;

[CreateAssetMenu(fileName = "My-Script-Object", menuName = "Git-Project-Code/My-Script-Object", order = 0)]
public class Simple_ScriptableObject : ScriptableObject
{
    //Data is stored local
    [SerializeField] private string s_MyString = "Hello World!";

    //Data is stored local can be get normaly
    public void Set_MyString(string s_MyString)
    {
        this.s_MyString = s_MyString;
    }

    //Data is stored local can be CHANCE to new value and be SAVED (CAUTION!)
    public string GetMyString()
    {
        return s_MyString;
    }
}