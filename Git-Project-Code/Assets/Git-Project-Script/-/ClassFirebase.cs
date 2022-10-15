//using System.Collections.Generic;
//using System.Collections;
//using UnityEngine;

//using Firebase;
//using Firebase.Auth;
//using Firebase.Database;

///// <summary>
///// Working on Firebasee Primary for Android
///// </summary>
//public class ClassFirebase
////Firebasee Primary for Android
//{
//    public bool m_Debug = false;

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public ClassFirebase()
//    {
//        SetFirebaseStart();
//        mDebug = false;
//    }

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public ClassFirebase(bool m_Debug)
//    {
//        SetFirebaseStart();
//        mDebug = m_Debug;
//    }

//    private void SetFirebaseStart()
//    {
//        fm_Auth = FirebaseAuth.DefaultInstance;
//        //Auth on Firebase
//        //Active Auth on Android

//        daReference = FirebaseDatabase.DefaultInstance.RootReference;
//        //Database on Firebase

//        cmData = new ClassData();
//        //Working on Data

//        FirebaseApp.Create(new AppOptions(), GetFirebaseAum_ID());
//    }

//    public void SetFirebaseEnd()
//    {
//        //FirebaseAuth.DefaultInstance.Dispose();
//        //FirebaseDatabase.DefaultInstance.RootReference.OnDisconnect();
//    }

//    //=========================================================================================

//    //Auth ContinueWith (Primary Code)
//    //"https://firebase.google.com/docs/auth/unity/start?authuser=0"
//    //"https://forum.unity.com/threads/firebase-database-problem.976599/"

//    //Auth

//    /// <summary>
//    /// Auth on Firebase
//    /// </summary>
//    private FirebaseAuth fm_Auth;

//    /// <summary>
//    /// Auth Sign out
//    /// </summary>
//    public void SetFirebaseAum_SignOut()
//    {
//        if(m_Debug) Debug.LogWarning("SetFirebaseAum_SignOut: Sign Out!");
//        fm_Auth.SignOut();
//    }

//    /// <summary>
//    /// Get Auth Login
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAumLogin()
//    {
//        if(m_Debug) Debug.Log("GetFirebaseAumLogIn: " + (fm_Auth.CurrentUser != null).ToString());
//        return fm_Auth.CurrentUser != null;
//    }

//    /// <summary>
//    /// Get Display Name of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAumDisplayName()
//    {
//        if (GetFirebaseAumLogin())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAumDisplayName: " + fm_Auth.CurrentUser.DisplayName);
//            return fm_Auth.CurrentUser.DisplayName;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAumDisplayName: Null");
//        return null;
//    }

//    /// <summary>
//    /// Get Email of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAum_Email()
//    {
//        if (GetFirebaseAumLogin())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAum_Email: " + fm_Auth.CurrentUser.Email);
//            return fm_Auth.CurrentUser.Email;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAum_Email: Null");
//        return null;
//    }

//    /// <summary>
//    /// Get ID of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAum_ID()
//    {
//        if (GetFirebaseAumLogin())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAum_ID: " + fm_Auth.CurrentUser.UserId);
//            return fm_Auth.CurrentUser.UserId;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAum_ID: Null");
//        return null;
//    }

//    //Register

//    /// <summary>
//    /// Check if Register is Done
//    /// </summary>
//    private bool m_FirebaseAumRegisterDone = false;

//    /// <summary>
//    /// Check if Register is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAumRegisterDone()
//    {
//        return m_FirebaseAumRegisterDone;
//    }

//    /// <summary>
//    /// Set Register Done check
//    /// </summary>
//    /// <param name="mChance"></param>
//    public void SetFirebaseAumRegisterDone(bool m_Chance)
//    {
//        m_FirebaseAumRegisterDone = m_Chance;
//    }

//    /// <summary>
//    /// Create User Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void SetFirebaseAumRegister_ContinueWith(string m_Email, string m_Password, object o_Class)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//            return;
//        }

//        SetFirebaseAumRegisterDone(false);

//        SetFirebase_Message("Creating...");
//        if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_ContinueWith: Creating");

//        fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//        {
//            if (task.Canceled || task.Faulted)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Create
//            SetFirebaseAumRegisterDone(true);

//            FirebaseUser fm_AuthUser = task.Result;

//            SetFirebase_Message("Register Complete");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_ContinueWith: " + fm_Auth.CurrentUser.Email);

//            if (o_Class != null)
//            {
//                SetFirebaseDatabase_Object(
//                    "_Player/" + GetFirebaseAum_ID().ToString(),
//                    o_Class);
//                //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"
//            }
//        });
//    }

//    /// <summary>
//    /// Create User Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="mRePassword"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void SetFirebaseAumRegister_ContinueWith(string m_Email, string m_Password, string m_RePassword, object o_Class)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//            return;
//        }

//        if (m_Password != m_RePassword)
//        {
//            if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_ContinueWith: Not same Password");
//            return;
//        }
//        else
//        {
//            SetFirebaseAumRegisterDone(false);

//            SetFirebase_Message("Creating...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_ContinueWith: Creating");

//            fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                //Create
//                SetFirebaseAumRegisterDone(true);

//                FirebaseUser fm_AuthUser = task.Result;

//                SetFirebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_ContinueWith: " + fm_Auth.CurrentUser.Email);

//                if (o_Class != null)
//                {
//                    SetFirebaseDatabase_Object(
//                        GetFirebaseDatabase_ID(GetFirebaseAum_ID().ToString()),
//                        o_Class);
//                    //Create Primary User Auth Profile in Firebase Database at "_ID$UserAuthID/"
//                }
//            });
//        }
//    }

//    //Login

//    /// <summary>
//    /// Check if Login is Done
//    /// </summary>
//    private bool FirebaseListenLoginDone = false;

//    /// <summary>
//    /// Check if Login is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAumLoginDone()
//    {
//        return FirebaseListenLoginDone;
//    }

//    /// <summary>
//    /// Set Login Done check
//    /// </summary>
//    /// <param name="mChance"></param>
//    public void SetFirebaseAumLoginDone(bool m_Chance)
//    {
//        this.FirebaseListenLoginDone = m_Chance;
//    }

//    /// <summary>
//    /// Login User Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    public void SetFirebaseListenLogin_ContinueWith(string m_Email, string m_Password)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//            return;
//        }

//        SetFirebase_Message("Log-ing...");
//        if(m_Debug) Debug.LogWarning("SetFirebaseListenLogin_ContinueWith: Log-ing...");

//        fm_Auth.SignInWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//        {
//            if (task.Canceled || task.Faulted)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseListenLogin_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Login
//            FirebaseUser fm_AuthUser = task.Result;

//            SetFirebase_Message("Login Complete");
//            if(m_Debug) Debug.LogWarning("SetFirebaseListenLogin_ContinueWith: " + fm_Auth.CurrentUser.Email);
//        });
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase
//    /// </summary>
//    /// <param name="mDisplayName"></param>
//    /// <param name="u_PhotoUrl"></param>
//    public void SetFirebaseAumChanceProfile_ContinueWith(string m_DisplayName, System.Uri u_PhotoUrl)
//    {
//        if (GetFirebaseAumLogin())
//        {
//            Firebase.Auth.UserProfile u_Profile = new Firebase.Auth.UserProfile
//            {
//                DisplayName = m_DisplayName,
//                PhotoUrl = u_PhotoUrl,
//                //PhotoUrl = new System.Uri("https://example.com/jane-q-user/profile.jpg")
//            };
//            //Update
//            fm_Auth.CurrentUser.UpdateUserProfileAsync(u_Profile).ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAumChanceProfile_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChanceProfile_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAumChanceProfile_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    public void SetFirebaseAumChanceEmaim_ContinueWith(string m_Email)
//    {
//        if (GetFirebaseAumLogin())
//        {
//            //Update
//            fm_Auth.CurrentUser.UpdateEmailAsync(m_Email).ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAumChanceEmaim_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChanceEmaim_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAumChanceEmaim_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase
//    /// </summary>
//    /// <param name="m_NewPassword"></param>
//    public void SetFirebaseAumChancePassword_ContinueWith(string m_NewPassword)
//    {
//        if (GetFirebaseAumLogin())
//        {
//            fm_Auth.CurrentUser.UpdatePasswordAsync(m_NewPassword).ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAumChancePassword_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChancePassword_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAumChancePassword_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase
//    /// </summary>
//    public void SetFirebaseAumDeleteAum_ContinueWith()
//    {
//        if (GetFirebaseAumLogin())
//        {
//            fm_Auth.CurrentUser.DeleteAsync().ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAumDeleteAum_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumDeleteAum_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAumDeleteAum_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Send Email / Gmail Verification ContinueWith

//    /// <summary>
//    /// Check if Send Email is Done
//    /// </summary>
//    private bool m_FirebaseAum_SendEmaimDone = false;

//    /// <summary>
//    /// Check if Send Email is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAum_SendEmaimDone()
//    {
//        return m_FirebaseAum_SendEmaimDone;
//    }

//    /// <summary>
//    /// Set Send Email Done check
//    /// </summary>
//    /// <param name="mChance"></param>
//    public void SetFirebaseAum_SendEmaimDone(bool m_Chance)
//    {
//        m_FirebaseAum_SendEmaimDone = m_Chance;
//    }

//    /// <summary>
//    /// Get Email Verified after Send Email
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAum_EmailVerification()
//    {
//        if (GetFirebaseAumLogin())
//        {
//            fm_Auth.CurrentUser.ReloadAsync();
//            //Refreshes the data for this User Auth on Firebase

//            if(m_Debug) Debug.Log("GetFirebaseAum_EmailVerification: " + fm_Auth.CurrentUser.EmailVerified);
//            return fm_Auth.CurrentUser.EmailVerified;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAum_EmailVerification: Not Login yet");
//        return false;
//    }

//    /// <summary>
//    /// Send Email Verification on Firebase
//    /// </summary>
//    public void SetFirebaseAum_EmailVerification_Send_ContinueWith()
//    {
//        SetFirebaseAum_SendEmaimDone(false);

//        FirebaseUser fmUserCurrent = fm_Auth.CurrentUser;
//        if (fmUserCurrent != null)
//        {
//            fmUserCurrent.SendEmailVerificationAsync().ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    //If not Checked m_ail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("SetFirebaseAum_EmailVerification_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                SetFirebaseAum_SendEmaimDone(true);

//                if(m_Debug) Debug.LogWarning("SetFirebaseAum_EmailVerification_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase
//    /// </summary>
//    /// <param name="m_Email"></param>
//    public void SetFirebaseAum_PasswordResetEmaim_Send_ContinueWith(string m_Email)
//    {
//        SetFirebaseAum_SendEmaimDone(false);

//        FirebaseUser fmUserCurrent = fm_Auth.CurrentUser;
//        if (fmUserCurrent != null)
//        {
//            fm_Auth.SendPasswordResetEmailAsync(m_Email).ContinueWith(task =>
//            {
//                if (task.Canceled || task.Faulted)
//                {
//                    //If not Checked m_ail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("SetFirebaseAum_PasswordResetEmaim_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                SetFirebaseAum_SendEmaimDone(true);

//                if(m_Debug) Debug.LogWarning("SetFirebaseAum_PasswordResetEmaim_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    //Auth IEnumerator (In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));")
//    //"https://docs.unity3d.com/Manual/Coroutines.html"

//    //Register

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="mDisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_Player/UserAuthID"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumRegister_IEnumerator(string m_Email, string m_Password, string m_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            SetFirebaseAumRegisterDone(false);

//            SetFirebase_Message("Register-ing...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: Register-ing");

//            var vRegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password);
//            //Create User Auth to Firebase

//            yield return new WaitUntil(predicate: () => vRegisterTask.Completed);
//            //Wait until Create User Auth to Firebase complete

//            SetFirebaseAumRegisterDone(true);

//            if (vRegisterTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = vRegisterTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        SetFirebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: m_issing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        SetFirebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: m_issing Password");
//                        break;
//                    case AuthError.WeakPassword:
//                        SetFirebase_Message("Weak Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: Weak Password");
//                        break;
//                    case AuthError.EmailAlreadyInUse:
//                        SetFirebase_Message("Email Already In Use");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: Email Already In Use");
//                        break;
//                }
//            }
//            else
//            //If Create User Auth to Firebase complete
//            {
//                SetFirebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: " + m_Email);

//                FirebaseUser fm_AuthUser = vRegisterTask.Result;

//                if (fm_AuthUser != null)
//                //If Login new User Auth to Firebase complete
//                {
//                    //Send Email

//                    if (m_EmailVerification)
//                    {
//                        SetFirebaseAum_EmailVerification_Send_ContinueWith();
//                    }

//                    //Set Profile

//                    UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
//                    //Create User Profile (DisplayName) Auth to Firebase

//                    var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//                    //Update User Auth Profile

//                    yield return new WaitUntil(predicate: () => v_ProfileTask.Completed);
//                    //Wait until Update User Profile Auth to Firebase complete

//                    if (v_ProfileTask.Exception != null)
//                    //If Update User Profile Auth to Firebase Error
//                    {
//                        FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                        AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                        if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: " + m_DisplayName);
//                    }
//                    else
//                    {
//                        if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: Successfully!");
//                    }

//                    if (o_Class != null)
//                    {
//                        SetFirebaseDatabase_Object(
//                            "_Player/" + GetFirebaseAum_ID().ToString(),
//                            o_Class);
//                        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"
//                    }
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="m_PasswordRe">Check if "Re-Password" same to "Password"</param>
//    /// <param name="mDisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumRegister_IEnumerator(string m_Email, string m_Password, string m_PasswordRe, string m_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            if (m_Password != m_PasswordRe)
//            {
//                SetFirebase_Message("Check Password Again");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: Not same Password");
//            }
//            else
//            {
//                SetFirebaseAumRegisterDone(false);

//                SetFirebase_Message("Register-ing...");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: Register-ing");

//                var vRegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password);
//                //Create User Auth to Firebase

//                yield return new WaitUntil(predicate: () => vRegisterTask.Completed);
//                //Wait until Create User Auth to Firebase complete

//                SetFirebaseAumRegisterDone(true);

//                if (vRegisterTask.Exception != null)
//                //If Create User Auth to Firebase Error
//                {
//                    FirebaseException m_FirebaseEx = vRegisterTask.Exception.GetBaseException() as FirebaseException;
//                    AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                    //Get Create User Auth to Firebase Error

//                    switch (a_ErrorCode)
//                    {
//                        case AuthError.MissingEmail:
//                            SetFirebase_Message("Missing Email");
//                            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: m_issing Email");
//                            break;
//                        case AuthError.MissingPassword:
//                            SetFirebase_Message("Missing Password");
//                            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: m_issing Password");
//                            break;
//                        case AuthError.WeakPassword:
//                            SetFirebase_Message("Weak Password");
//                            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: Weak Password");
//                            break;
//                        case AuthError.EmailAlreadyInUse:
//                            SetFirebase_Message("Email Already In Use");
//                            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: Email Already In Use");
//                            break;
//                    }
//                }
//                else
//                //If Create User Auth to Firebase complete
//                {
//                    SetFirebase_Message("Register Complete");
//                    if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: " + m_Email);

//                    FirebaseUser fm_AuthUser = vRegisterTask.Result;

//                    if (fm_AuthUser != null)
//                    //If Login new User Auth to Firebase complete
//                    {
//                        //Send Email

//                        if (m_EmailVerification)
//                        {
//                            SetFirebaseAum_EmailVerification_Send_ContinueWith();
//                        }

//                        //Set Profile

//                        UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
//                        //Create User Profile (DisplayName) Auth to Firebase

//                        var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//                        //Update User Auth Profile

//                        yield return new WaitUntil(predicate: () => v_ProfileTask.Completed);
//                        //Wait until Update User Profile Auth to Firebase complete

//                        if (v_ProfileTask.Exception != null)
//                        //If Update User Profile Auth to Firebase Error
//                        {
//                            FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                            AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_IEnumerator: " + m_DisplayName);
//                        }
//                        else
//                        {
//                            if(m_Debug) Debug.LogWarning("SetFirebaseAumRegister_IEnumerator: Successfully!");
//                        }

//                        if (o_Class != null)
//                        {
//                            SetFirebaseDatabase_Object(
//                                GetFirebaseDatabase_ID(GetFirebaseAum_ID().ToString()),
//                                o_Class);
//                            //Create Primary User Auth Profile in Firebase Database at "_ID$UserAuthID/"
//                        }
//                    }
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// Get root of a ID in Firebase Database
//    /// </summary>
//    /// <param name="m_ID"></param>
//    /// <returns>Get root string "_ID$UserAuthID/"</returns>
//    public string GetFirebaseDatabase_ID(string m_ID)
//    {
//        return "_ID" + m_ID;
//    }

//    //Login

//    /// <summary>
//    /// Login User Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorLogin(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumLogin_IEnumerator(string m_Email, string m_Password)
//    {
//        ClassString m_String = new ClassString();
//        if (!m_String.GetEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAumRegister_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            SetFirebaseAumLoginDone(false);

//            SetFirebase_Message("Log-ing...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAumLogin_IEnumerator: Log-ing...");

//            var vLoginTask = fm_Auth.SignInWithEmailAndPasswordAsync(m_Email, m_Password);
//            //Login User Auth to Firebase

//            yield return new WaitUntil(predicate: () => vLoginTask.Completed);
//            //Wait until Create User Auth to Firebase complete

//            SetFirebaseAumLoginDone(true);

//            if (vLoginTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = vLoginTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        SetFirebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumLogin_IEnumerator: m_issing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        SetFirebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumLogin_IEnumerator: m_issing Password");
//                        break;
//                    case AuthError.WrongPassword:
//                        SetFirebase_Message("Wrong Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumLogin_IEnumerator: Wrong Password");
//                        break;
//                    case AuthError.InvalidEmail:
//                        SetFirebase_Message("Invalid Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumLogin_IEnumerator: Invalid Email");
//                        break;
//                    case AuthError.UserNotFound:
//                        SetFirebase_Message("Account does not exist");
//                        if(m_Debug) Debug.LogError("SetFirebaseAumLogin_IEnumerator: Account does not exist");
//                        break;
//                }
//            }
//            else
//            //If Login User Auth to Firebase complete
//            {
//                SetFirebase_Message("Login Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumLogin_IEnumerator: " + GetFirebaseAum_Email());

//                FirebaseUser fm_AuthUser = vLoginTask.Result;
//            }
//        }
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseAum_Profile(...));"
//    /// </summary>
//    /// <param name="mDisplayName"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumChanceProfile_IEnumerator(string m_DisplayName)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
//            //Create User Profile (DisplayName) Auth to Firebase

//            var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_ProfileTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_ProfileTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAumChanceProfile_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChanceProfile_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorChanceEmail(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumChanceEmaim_IEnumerator(string m_Email)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_EmailTask = fm_AuthUser.UpdateEmailAsync(m_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_EmailTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_EmailTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_EmailTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAumChanceEmaim_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChanceEmaim_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorChancePassword(...));"
//    /// </summary>
//    /// <param name="m_NewPassword"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumChancePassword_IEnumerator(string m_NewPassword)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_PasswordTask = fm_AuthUser.UpdatePasswordAsync(m_NewPassword);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_PasswordTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_PasswordTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_PasswordTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAumChancePassword_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumChancePassword_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorDeleteAuth(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAumDeleteAum_IEnumerator()
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var vDeleteTask = fm_AuthUser.DeleteAsync();
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => vDeleteTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (vDeleteTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = vDeleteTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAumDeleteAum_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAumDeleteAum_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Send Email / Gmail Verification ContinueWith

//    private int m_ResetEventTime = 100;

//    public void SetResetEventTime(int m_ResetEventTime)
//    {
//        mResetEventTime = m_ResetEventTime;
//    }

//    /// <summary>
//    /// Send Email Verification on Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumerator_EmailVerification_Send(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAum_EmailVerification_Send_IEnumerator()
//    {
//        SetFirebaseAum_SendEmaimDone(false);

//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_VerificationTask = fm_AuthUser.SendEmailVerificationAsync();
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            SetFirebaseAum_SendEmaimDone(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAum_EmailVerification_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAum_EmailVerification_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase. In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumerator_PasswordResetEmaim_Send(...));"
//    /// </summary>
//    /// <param name="m_Email"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAum_PasswordResetEmaim_Send_IEnumerator(string m_Email)
//    {
//        SetFirebaseAum_SendEmaimDone(false);

//        FirebaseUser fmUserCurrent = fm_Auth.CurrentUser;
//        if (fmUserCurrent != null)
//        {
//            var v_VerificationTask = fm_Auth.SendPasswordResetEmailAsync(m_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.Completed);
//            //Wait until Update User Profile Auth to Firebase complete

//            SetFirebaseAum_SendEmaimDone(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAum_PasswordResetEmaim_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAum_PasswordResetEmaim_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //=========================================================================================

//    //Event FirebaseDatabase Listener (Set at Start or Awake)

//    //Value Changed

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEventValueChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventValueChanged(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChanged");
//        daReference.ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEventValueChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventValueChangedReset(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.ValueChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEventValueChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventValueChanged(string m_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChanged: " + m_DatabaseAcess);
//        daReference.Child(m_DatabaseAcess).ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEventValueChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventValueChangedReset(string m_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++) 
//            daReference.Child(m_DatabaseAcess).ValueChanged -= e_EventHandler;
//    }

//    //Child Added

//    /// <summary>
//    /// Add Event Child Added Listener by "SetFirebaseEventChildAdded(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildAdded(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildAdded");
//        daReference.ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "SetFirebaseEventChildAdded(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildAddedReset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.ChildAdded -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Added Listener by "SetFirebaseEventChildAdded(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildAdded(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildAdded: " + m_DatabaseAcess);
//        daReference.Child(m_DatabaseAcess).ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "SetFirebaseEventChildAdded(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildAddedReset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.Child(m_DatabaseAcess).ChildAdded -= e_EventHandler;
//    }

//    //Child Changed

//    /// <summary>
//    /// Add Event Child Changed Listener by "SetFirebaseEventChildChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildChanged(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildChanged");
//        daReference.ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "SetFirebaseEventChildChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildChangedReset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.ChildChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Changed Listener by "SetFirebaseEventChildChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildChanged(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildChanged: " + m_DatabaseAcess);
//        daReference.Child(m_DatabaseAcess).ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "SetFirebaseEventChildChanged(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildChangedReset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.Child(m_DatabaseAcess).ChildChanged -= e_EventHandler;
//    }

//    //Child m_oved

//    /// <summary>
//    /// Add Event Child m_oved Listener by "SetFirebaseEventChildMoved(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildMoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildMoved");
//        daReference.ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child m_oved Listener by "SetFirebaseEventChildMoved(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildMovedReset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.ChildMoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child m_oved Listener by "SetFirebaseEventChildMoved(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildMoved(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildMoved: " + m_DatabaseAcess);
//        daReference.Child(m_DatabaseAcess).ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child m_oved Listener by "SetFirebaseEventChildMoved(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildMovedReset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.Child(m_DatabaseAcess).ChildMoved -= e_EventHandler;
//    }

//    //Child Removed

//    /// <summary>
//    /// Add Event Child Removed Listener by "SetFirebaseEventChildRemoved(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildRemoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildRemoved");
//        daReference.ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "SetFirebaseEventChildRemoved(m_yEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildRemovedReset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.ChildRemoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Removed Listener by "SetFirebaseEventChildRemoved(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildRemoved(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventChildRemoved: " + m_DatabaseAcess);
//        daReference.Child(m_DatabaseAcess).ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "SetFirebaseEventChildRemoved(m_yEvent);"
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEventChildRemovedReset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEventValueChangedReset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            daReference.Child(m_DatabaseAcess).ChildRemoved -= e_EventHandler;
//    }

//    //=========================================================================================

//    //Script m_essage

//    private string m_FirebaseAum_Message = "";

//    /// <summary>
//    /// Get Current m_essage
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAum_Message()
//    {
//        return m_FirebaseAum_Message;
//    }

//    /// <summary>
//    /// Clear Current m_essage
//    /// </summary>
//    public void SetFirebaseAum_MessageClear()
//    {
//        m_FirebaseAum_Message = "";
//    }

//    /// <summary>
//    /// Set m_essage
//    /// </summary>
//    /// <param name="m_Message"></param>
//    private void SetFirebase_Message(string m_Message)
//    {
//        m_FirebaseAum_Message = m_Message;
//    }

//    //=========================================================================================

//    //Firebase Database (Set)

//    /// <summary>
//    /// Primary Root Database on Firebase
//    /// </summary>
//    private DatabaseReference daReference;

//    /// <summary>
//    /// Update Child in Database with INT
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, int m_Value)
//    {
//        daReference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with FLOAT
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, float m_Value)
//    {
//        daReference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with STRING
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, string m_Value)
//    {
//        daReference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with BOOL
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, bool m_Value)
//    {
//        daReference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with OBJECT
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="o_Class">"MyClass" see example in "Android_FirebaseUserClass.cs"</param>
//    public void SetFirebaseDatabase_Object(string m_DatabaseAcess, object o_Class)
//    {
//        string m_Json = JsonUtility.ToJson(o_Class);
//        daReference.Child(m_DatabaseAcess).SetRawJsonValueAsync(m_Json);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Object: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Json + "\"");
//    }

//    /// <summary>
//    /// Delete Child in Database
//    /// </summary>
//    /// <param name="mDatabaseAcess">"Parent/KeyDelete" or "KeyDelete"</param>
//    public void SetFirebaseDatabaseDelete(string m_DatabaseAcess)
//    {
//        daReference.Child(m_DatabaseAcess).RemoveValueAsync();
//        if(m_Debug) Debug.Log("SetFirebaseDatabaseDelete: " + "\"" + m_DatabaseAcess + "\"");
//    }

//    //Data

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    private ClassData cmData;

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    /// <returns></returns>
//    public ClassData GetData()
//    {
//        return cmData;
//    }

//    //Firebase Database (Get)

//    private void GetDataSnapshot_ValueDebug(DataSnapshot d_Snapshot, string m_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + d_Snapshot.Value.ToString() + "\"");
//    }

//    private void GetDataSnapshot_ValueDebug(DataSnapshot d_Snapshot, string m_Json, string m_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + m_Json + "\"");
//    }

//    private void GetDataSnapshot_NotExistDebug(DataSnapshot d_Snapshot, string m_Debug)
//    {
//        if(m_Debug) Debug.LogError(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + cmData.GetDataNotFound() + "\"");
//    }

//    private bool GetDataSnapshotExist(DataSnapshot d_Snapshot)
//    {
//        return d_Snapshot.Exists;
//    }

//    //Firebase Database ContinueWith (Primary)

//    /// <summary>
//    /// Check if Get from Firebase Database is Done
//    /// </summary>
//    /// <returns>If Convert fail, return FALSE</returns>
//    public bool GetFirebaseDatabase_GetDone(string m_ProgessSaveName)
//    {
//        return GetData().GetConvert_Bool(GetData().GetData(m_ProgessSaveName));
//    }

//    /// <summary>
//    /// Set Get from Firebase Database Done check
//    /// </summary>
//    /// <param name="m_ProgessChance"></param>
//    public void SetFirebaseDatabase_GetDone(string m_ProgessSaveName, bool m_ProgessChance)
//    {
//        GetData().SetData(m_ProgessSaveName, m_ProgessChance);
//    }

//    /// <summary>
//    /// Get Signle Data inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public void SetFirebaseDatabase_ValueSingle_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        daReference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.Faulted || task.Canceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_ValueSingle_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to Json Data

//                //if(m_Json == "")
//                ////If there is not Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");

//                //    cmData.SetData(m_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");
//                //}

//                GetDataSnapshot_ValueDebug(
//                        d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");

//                cmData.SetData(m_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get List of Key inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public void SetFirebaseDatabase_KeyList_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        daReference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.Faulted || task.Canceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyList_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to Json Data

//                //if (m_Json == "")
//                ////If there is not Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, "SetFirebaseDatabase_KeyList_ContinueWith: ");
//                //}
//                //else
//                ////If there is Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_ContinueWith: ");

//                //    cmData.SetIndexRestart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cmData.SetIndex_Plus();
//                //        //Plus Index Data by 1

//                //        cmData.SetData(m_DataNameSave, cmData.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cmData.SetDataCount(m_DataNameSave, cmData.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_ValueDebug(
//                    d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_ContinueWith: ");

//                cmData.SetIndexRestart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cmData.SetIndex_Plus();
//                    //Plus Index Data by 1

//                    cmData.SetData(m_DataNameSave, cmData.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cmData.SetDataCount(m_DataNameSave, cmData.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyList_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get Key Exist inside Key Get from Firebase Database, then Save Bool Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public void SetFirebaseDatabase_KeyExist_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        daReference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.Faulted || task.Canceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyExist_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_ValueDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_ContinueWith: ");

//                cmData.SetData(m_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_ContinueWith: ");

//                cmData.SetData(m_DataNameSave, false);
//                //Save Data (Value)
//            }
//        });
//    }

//    //Firebase Database IEnumerator

//    /// <summary>
//    /// Get Signle Data inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()"). 
//    /// In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));"
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_ValueSingle_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = daReference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.Completed || v_Task.Faulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.Completed)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to Json Data

//                //if (m_Json == "")
//                ////If there is not Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");

//                //    cmData.SetData(m_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");
//                //}

//                GetDataSnapshot_ValueDebug(
//                    d_Snapshot, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");

//                cmData.SetData(m_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyList_IEnumerator: UnSuccessfully!");
//        }
//    }

//    /// <summary>
//    /// Get List of Key inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()").
//    /// In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));"
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_KeyList_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = daReference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.Completed || v_Task.Faulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.Completed)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to Json Data

//                //if (m_Json == "")
//                ////If there is not Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, "SetFirebaseDatabase_KeyList_IEnumerator: ");
//                //}
//                //else
//                ////If there is Json Data
//                //{
//                //    GetDataSnapshot_ValueDebug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_IEnumerator: ");

//                //    cmData.SetIndexRestart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cmData.SetIndex_Plus();
//                //        //Plus Index Data by 1

//                //        cmData.SetData(m_DataNameSave, cmData.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cmData.SetDataCount(m_DataNameSave, cmData.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_ValueDebug(
//                    d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_IEnumerator: ");

//                cmData.SetIndexRestart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cmData.SetIndex_Plus();
//                    //Plus Index Data by 1

//                    cmData.SetData(m_DataNameSave, cmData.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cmData.SetDataCount(m_DataNameSave, cmData.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyList_IEnumerator: ");
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyList_IEnumerator: UnSuccessfully!");
//        }
//    }

//    /// <summary>
//    /// Get Key Exist inside Key Get from Firebase Database, then Save Bool Data (Get Data from "GetData()")
//    /// In "MonoBehaviour", use "StartCoroutine(SetFirebaseIEnumeratorRegister(...));"
//    /// </summary>
//    /// <param name="mDatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="mDataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_KeyExist_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = daReference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.Completed || v_Task.Faulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.Completed)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_ValueDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_IEnumerator: ");

//                cmData.SetData(m_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExistDebug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_IEnumerator: ");

//                cmData.SetData(m_DataNameSave, false);
//                //Save Data (Value)
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyExist_IEnumerator: UnSuccessfully!");
//        }
//    }

//    //=========================================================================================
//}