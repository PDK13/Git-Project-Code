using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIMessage : MonoBehaviour
{
    [Header("Input Field for Message Pos")]

    [Tooltip("Input Message's Name")]
    [SerializeField]
    private InputField inp_Name;

    [Tooltip("Input Message")]
    [SerializeField]
    private InputField inp_Message;

    [Header("Button Message for Message List")]

    [Tooltip("Button Message List Add")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Add;

    [Tooltip("Button Message List Del Lastest")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Del_Lastest;

    #region Message Pos

    public void SetClone(IsoDataMessageSingle v3_Message_Single)
    {
        inp_Name.text = v3_Message_Single.GetName();
        inp_Message.text = v3_Message_Single.GetMessage();
    }

    public IsoDataMessageSingle GetData()
    {
        return new IsoDataMessageSingle(
            inp_Name.text,
            inp_Message.text);
    }

    public UIButtonOnClick GetButton_Add()
    {
        return cl_Button_Add;
    }

    public UIButtonOnClick GetButton_Del_Lastest()
    {
        return cl_Button_Del_Lastest;
    }

    #endregion
}
