//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthCreate : MonoBehaviour
//{
//    /// <summary>
//    /// FIREBASE
//    /// </summary>
//    private Clasm_Firebase cl_Firebase;

//    /// <summary>
//    /// Input Field EMAIL
//    /// </summary>
//    public InputField m_Email;
//    /// <summary>
//    /// Input Field PASSWORD
//    /// </summary>
//    public InputField m_Password;
//    /// <summary>
//    /// Input Field RE-PASSWORD
//    /// </summary>
//    public InputField m_Password_Re;
//    /// <summary>
//    /// Input Field DISPLAY-NAME
//    /// </summary>
//    public InputField m_DisplayName;

//    /// <summary>
//    /// Text EMAIL Auth
//    /// </summary>
//    public Text t_EmailAuth;

//    /// <summary>
//    /// Text INFO
//    /// </summary>
//    public Text t_Info;

//    /// <summary>
//    /// Scene START
//    /// </summary>
//    public string m_SceneBack = "Android_FirebaseStart";

//    private void Start()
//    {
//        cl_Firebase = new Clasm_Firebase();

//        m_Password.inputType = InputField.InputType.Password;
//        m_Password_Re.inputType = InputField.InputType.Password;
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cl_Firebase.GetFirebaseAuth_Login())
//        //If Auth LOGIN or CREATE Success
//        {
//            t_EmailAuth.text = cl_Firebase.GetFirebaseAuth_Email().ToUpper();
//        }
//        else
//        //If Auth not LOGIN or CREATE yet
//        {
//            t_EmailAuth.text = "Unknown".ToUpper();
//        }

//        if (cl_Firebase.GetFirebaseAuth_Register_Done())
//        {
//            t_Info.text = cl_Firebase.GetFirebaseAuth_Message();
//            cl_Firebase.SetFirebaseAuth_Register_Done(false);
//        } 
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseCreate: OnDestroy");
//    }

//    //Create

//    /// <summary>
//    /// Button CREATE
//    /// </summary>
//    public void Button_Create()
//    {
//        //cl_Firebase.SetFirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        cl_Firebase.SetFirebaseAuth_Message_Clear();

//        if(m_Email.text == "")
//        {
//            t_Info.text = "Email not allow emty";
//            return;
//        }

//        if (m_Password.text == "")
//        {
//            t_Info.text = "Password not allow emty";
//            return;
//        }

//        if (m_Password_Re.text == "")
//        {
//            t_Info.text = "Re-Password not same to Password";
//            return;
//        }

//        if(m_Password.text.Length < 6 || m_Password.text.Length > 12)
//        {
//            t_Info.text = "Password allow 6-12 Characters";
//            return;
//        }

//        if (m_DisplayName.text == "")
//        //If DisplayName emty >> DisplayName will "Newbie"
//        {
//            m_DisplayName.text = "Newbie";
//        }

//        m_DisplayName.text = m_DisplayName.text.ToUpper();

//        StartCoroutine(
//            cl_Firebase.SetFirebaseAuth_Register_IEnumerator(
//                m_Email.text,
//                m_Password.text,
//                m_Password_Re.text,
//                m_DisplayName.text,
//                true,
//                new Android_FirebasePlayer_Data(m_DisplayName.text)));
//        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"

//        t_Info.text = cl_Firebase.GetFirebaseAuth_Message();
//    }

//    //Back

//    /// <summary>
//    /// Button BACK
//    /// </summary>
//    public void Button_Cancel()
//    {
//        Clasm_Scene cl_Scene = new Clasm_Scene(m_SceneBack);
//        //Chance Scene to "Back"
//    }

//    //Exit

//    /// <summary>
//    /// Button EXIT
//    /// </summary>
//    public void Button_Exit()
//    {
//        Application.Quit();
//    }
//}
