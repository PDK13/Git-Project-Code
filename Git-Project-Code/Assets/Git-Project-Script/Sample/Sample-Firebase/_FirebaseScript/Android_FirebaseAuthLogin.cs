//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthLogin : MonoBehaviour
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
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cl_Firebase.GetFirebaseAuth_Login())
//        //If Auth is LOGIN Sucess
//        {
//            t_EmailAuth.text = cl_Firebase.GetFirebaseAuth_Email().ToUpper();
//        }
//        else
//        //If Auth not LOGIN yet
//        {
//            t_EmailAuth.text = "Unknown".ToUpper();
//        }

//        if (cl_Firebase.GetFirebaseAuth_Login_Done())
//        {
//            t_Info.text = cl_Firebase.GetFirebaseAuth_Message();
//            cl_Firebase.SetFirebaseAuth_Register_Done(false);
//        }
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseLogin: OnDestroy");
//    }

//    //Login

//    /// <summary>
//    /// Button LOGIN
//    /// </summary>
//    public void Button_Login()
//    {
//        //cl_Firebase.SetFirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        cl_Firebase.SetFirebaseAuth_Message_Clear();

//        if (m_Email.text == "")
//        {
//            t_Info.text = "Email not allow emty";
//            return;
//        }

//        if (m_Password.text == "")
//        {
//            t_Info.text = "Password not allow emty";
//            return;
//        }

//        StartCoroutine(cl_Firebase.SetFirebaseAuth_Login_IEnumerator(m_Email.text, m_Password.text));

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
//        cl_Firebase.SetFirebaseAuth_SignOut();
//        //Sign out User Auth from Firebase

//        Application.Quit();
//    }
//}
