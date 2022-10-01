//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Android_FirebaseStart : MonoBehaviour
//{
//    /// <summary>
//    /// FIREBASE
//    /// </summary>
//    private ClassFirebase cm_Firebase;

//    /// <summary>
//    /// Text EMAIL Auth
//    /// </summary>
//    public Text t_EmailAuth;

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public GameObject m_StartButton;

//    /// <summary>
//    /// Text INFO
//    /// </summary>
//    public Text t_Info;

//    /// <summary>
//    /// Button LOG OUT
//    /// </summary>
//    public GameObject m_ButtonLogOut;

//    private void Start()
//    {
//        cm_Firebase = new ClassFirebase();

//        m_StartButton.SetActive(false);
//        m_ButtonLogOut.SetActive(false);

//        t_Info.text = "";
//    }

//    private void Update()
//    {
//        if (cm_Firebase.GetFirebaseAumLogin())
//        //If Auth LOGIN Success
//        {
//            t_EmailAuth.text = cm_Firebase.GetFirebaseAum_Email().ToUper();

//            m_ButtonLogOut.SetActive(true);
//            //Active Button LOG OUT

//            if (!cm_Firebase.GetFirebaseAum_EmailVerification())
//            //If EMAIL VERIFICATION not check yet
//            {
//                t_Info.text = "Waiting for Email Verification check";

//                m_StartButton.SetActive(false);
//                //De-Active Button START
//            }
//            else
//            //If EMAIL VERIFICATION check Success
//            {
//                t_Info.text = "Click \"START\" to continue";

//                m_StartButton.SetActive(true);
//                //Active Button START
//            }
//        }
//        else
//        //If Auth not LOGIN yet
//        {
//            t_EmailAuth.text = "Unknown".ToUper();

//            t_Info.text = "Click \"LOGIN\" or \"CREATE\" to continue";

//            m_StartButton.SetActive(false);
//            //De-Active Button START
//            m_ButtonLogOut.SetActive(false);
//            //De-Active Button LOG OUT
//        }
//    }

//    private void OnDestroy()
//    {
//        Debug.LogWarning("Android_FirebaseStart: OnDestroy");
//    }

//    //Create

//    /// <summary>
//    /// Scene CREATE AUTH
//    /// </summary>
//    public string m_SceneCreate = "Android_FirebaseCreate";

//    /// <summary>
//    /// Button CREATE
//    /// </summary>
//    public void ButtonCreate()
//    {
//        ClassScene cm_Scene = new ClassScene(m_SceneCreate);
//        //Chance Scene to "Create"
//    }

//    //Login

//    /// <summary>
//    /// Scene LOGIN Auth
//    /// </summary>
//    public string m_SceneLogin = "Android_FirebaseLogin";

//    /// <summary>
//    /// Button LOGIN
//    /// </summary>
//    public void ButtonLogin()
//    {
//        ClassScene cm_Scene = new ClassScene(m_SceneLogin);
//        //Chance Scene to "Login"
//    }

//    //Log out

//    /// <summary>
//    /// Button LOG OUT
//    /// </summary>
//    public void ButtonLogOut()
//    {
//        cm_Firebase.SetFirebaseAum_SignOut();
//        //Sign out User Auth from Firebase
//    }

//    //Info

//    /// <summary>
//    /// Scene INFO
//    /// </summary>
//    public string m_SceneInfo = "Android_FirebaseInfo";

//    /// <summary>
//    /// Button INFO
//    /// </summary>
//    public void Button_Info()
//    {
//        ClassScene cm_Scene = new ClassScene(m_SceneInfo);
//        //Chance Scene to "Login"
//    }

//    //Start

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public string m_SceneStart = "";

//    /// <summary>
//    /// Button START
//    /// </summary>
//    public void ButtonStart()
//    {
//        ClassScene cm_Scene = new ClassScene(m_SceneStart);
//        //Chance Scene to "Login"
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
