//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthLogin : MonoBehaviour
//{
//    /// <summary>
//    /// FIREBASE
//    /// </summary>
//    private ClassFirebase cm_Firebase;

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
//        cm_Firebase = new ClassFirebase();

//        m_Password.inputType = InputField.InputType.Password;
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cm_Firebase.GetFirebaseAumLogin())
//        //If Auth is LOGIN Sucess
//        {
//            t_EmailAuth.text = cm_Firebase.GetFirebaseAum_Email().ToUper();
//        }
//        else
//        //If Auth not LOGIN yet
//        {
//            t_EmailAuth.text = "Unknown".ToUper();
//        }

//        if (cm_Firebase.GetFirebaseAumLoginDone())
//        {
//            t_Info.text = cm_Firebase.GetFirebaseAum_Message();
//            cm_Firebase.SetFirebaseAumRegisterDone(false);
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
//    public void ButtonLogin()
//    {
//        //cm_Firebase.SetFirebaseAum_SignOut();
//        //Sign out User Auth from Firebase

//        cm_Firebase.SetFirebaseAum_MessageClear();

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

//        StartCoroutine(cm_Firebase.SetFirebaseAumLogin_IEnumerator(m_Email.text, m_Password.text));

//        t_Info.text = cm_Firebase.GetFirebaseAum_Message();
//    }

//    //Back

//    /// <summary>
//    /// Button BACK
//    /// </summary>
//    public void Button_Cancel()
//    {
//        ClassScene cm_Scene = new ClassScene(m_SceneBack);
//        //Chance Scene to "Back"
//    }

//    //Exit

//    /// <summary>
//    /// Button EXIT
//    /// </summary>
//    public void Button_Exit()
//    {
//        cm_Firebase.SetFirebaseAum_SignOut();
//        //Sign out User Auth from Firebase

//        Application.Quit();
//    }
//}
