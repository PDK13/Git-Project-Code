﻿//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseAuthCreate : MonoBehaviour
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
//    /// Input Field RE-PASSWORD
//    /// </summary>
//    public InputField m_PasswordRe;
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
//        cm_Firebase = new ClassFirebase();

//        m_Password.inputType = InputField.InputType.Password;
//        m_PasswordRe.inputType = InputField.InputType.Password;
//        //Set Input Field to "Password"

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cm_Firebase.GetFirebaseAumLogin())
//        //If Auth LOGIN or CREATE Success
//        {
//            t_EmailAuth.text = cm_Firebase.GetFirebaseAum_Email().ToUper();
//        }
//        else
//        //If Auth not LOGIN or CREATE yet
//        {
//            t_EmailAuth.text = "Unknown".ToUper();
//        }

//        if (cm_Firebase.GetFirebaseAumRegisterDone())
//        {
//            t_Info.text = cm_Firebase.GetFirebaseAum_Message();
//            cm_Firebase.SetFirebaseAumRegisterDone(false);
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
//    public void ButtonCreate()
//    {
//        //cm_Firebase.SetFirebaseAum_SignOut();
//        //Sign out User Auth from Firebase

//        cm_Firebase.SetFirebaseAum_MessageClear();

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

//        if (m_PasswordRe.text == "")
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

//        m_DisplayName.text = m_DisplayName.text.ToUper();

//        StartCoroutine(
//            cm_Firebase.SetFirebaseAumRegister_IEnumerator(
//                m_Email.text,
//                m_Password.text,
//                m_PasswordRe.text,
//                m_DisplayName.text,
//                true,
//                new Android_FirebasePlayerData(m_DisplayName.text)));
//        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"

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
//        Application.Quit();
//    }
//}
