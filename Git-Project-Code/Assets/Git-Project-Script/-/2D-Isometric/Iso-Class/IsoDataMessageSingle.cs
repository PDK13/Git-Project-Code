using UnityEngine;

public class IsoDataMessageSingle
{
    [Tooltip("Name of Message")]
    private string s_Name = "";

    [Tooltip("Message")]
    private string s_Message = "";

    public IsoDataMessageSingle(string s_Name, string s_Message)
    {
        Set_Message(s_Name, s_Message);
    }

    #region Message Set 

    public void Set_Message(string s_Name, string s_Message)
    {
        this.s_Name = s_Name;
        this.s_Message = s_Message;
    }

    public void Set_Message_Name(string s_Name)
    {
        this.s_Name = s_Name;
    }

    public void Set_Message_Message(string s_Message)
    {
        this.s_Message = s_Message;
    }

    #endregion

    #region Message Get 

    public string Get_Name()
    {
        return s_Name;
    }

    public string Get_Message()
    {
        return s_Message;
    }

    #endregion
}
