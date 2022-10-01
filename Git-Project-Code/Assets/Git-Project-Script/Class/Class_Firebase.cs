//using System.Collections.Generic;
//using System.Collections;
//using UnityEngine;

//using Firebase;
//using Firebase.Auth;
//using Firebase.Database;

///// <summary>
///// Working on Firebasee Primary for Android
///// </summary>
//public class Class_Firebase
////Firebasee Primary for Android
//{
//    public bool m_Debug = false;

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public Class_Firebase()
//    {
//        Set_FirebaseStart();
//        this.m_Debug = false;
//    }

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public Class_Firebase(bool m_Debug)
//    {
//        Set_FirebaseStart();
//        this.m_Debug = m_Debug;
//    }

//    private void Set_FirebaseStart()
//    {
//        fm_Auth = FirebaseAuth.DefaultInstance;
//        //Auth on Firebase
//        //Active Auth on Android

//        da_Reference = FirebaseDatabase.DefaultInstance.RootReference;
//        //Database on Firebase

//        cl_Data = new Class_Data();
//        //Working on Data

//        FirebaseApp.Create(new AppOptions(), GetFirebaseAuth_ID());
//    }

//    public void Set_FirebaseEnd()
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
//    public void Set_FirebaseAuth_SignOut()
//    {
//        if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_SignOut: Sign Out!");
//        fm_Auth.SignOut();
//    }

//    /// <summary>
//    /// Get Auth Login
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAuth_Login()
//    {
//        if(m_Debug) Debug.Log("GetFirebaseAuth_LogIn: " + (fm_Auth.CurrentUser != null).ToString());
//        return fm_Auth.CurrentUser != null;
//    }

//    /// <summary>
//    /// Get Display Name of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAuth_DisplayName()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAuth_DisplayName: " + fm_Auth.CurrentUser.DisplayName);
//            return fm_Auth.CurrentUser.DisplayName;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAuth_DisplayName: Null");
//        return null;
//    }

//    /// <summary>
//    /// Get Email of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAuth_Email()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAuth_Email: " + fm_Auth.CurrentUser.Email);
//            return fm_Auth.CurrentUser.Email;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAuth_Email: Null");
//        return null;
//    }

//    /// <summary>
//    /// Get ID of User after get Auth
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAuth_ID()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            if(m_Debug) Debug.Log("GetFirebaseAuth_ID: " + fm_Auth.CurrentUser.UserId);
//            return fm_Auth.CurrentUser.UserId;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAuth_ID: Null");
//        return null;
//    }

//    //Register

//    /// <summary>
//    /// Check if Register is Done
//    /// </summary>
//    private bool m_FirebaseAuth_Register_Done = false;

//    /// <summary>
//    /// Check if Register is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAuth_Register_Done()
//    {
//        return m_FirebaseAuth_Register_Done;
//    }

//    /// <summary>
//    /// Set Register Done check
//    /// </summary>
//    /// <param name="m_Chance"></param>
//    public void Set_FirebaseAuth_Register_Done(bool m_Chance)
//    {
//        this.m_FirebaseAuth_Register_Done = m_Chance;
//    }

//    /// <summary>
//    /// Create User Auth to Firebase
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void Set_FirebaseAuth_Register_ContinueWith(string s_Email, string s_Password, object o_Class)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        Set_FirebaseAuth_Register_Done(false);

//        Set_Firebase_Message("Creating...");
//        if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_ContinueWith: Creating");

//        fm_Auth.CreateUserWithEmailAndPasswordAsync(s_Email, s_Password).ContinueWith(task =>
//        {
//            if (task.IsCanceled || task.IsFaulted)
//            {
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Create
//            Set_FirebaseAuth_Register_Done(true);

//            FirebaseUser fm_AuthUser = task.Result;

//            Set_Firebase_Message("Register Complete");
//            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_ContinueWith: " + fm_Auth.CurrentUser.Email);

//            if (o_Class != null)
//            {
//                Set_FirebaseDatabase_Object(
//                    "_Player/" + GetFirebaseAuth_ID().ToString(),
//                    o_Class);
//                //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"
//            }
//        });
//    }

//    /// <summary>
//    /// Create User Auth to Firebase
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    /// <param name="s_RePassword"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void Set_FirebaseAuth_Register_ContinueWith(string s_Email, string s_Password, string s_RePassword, object o_Class)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        if (s_Password != s_RePassword)
//        {
//            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_ContinueWith: Not same Password");
//            return;
//        }
//        else
//        {
//            Set_FirebaseAuth_Register_Done(false);

//            Set_Firebase_Message("Creating...");
//            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_ContinueWith: Creating");

//            fm_Auth.CreateUserWithEmailAndPasswordAsync(s_Email, s_Password).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                //Create
//                Set_FirebaseAuth_Register_Done(true);

//                FirebaseUser fm_AuthUser = task.Result;

//                Set_Firebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_ContinueWith: " + fm_Auth.CurrentUser.Email);

//                if (o_Class != null)
//                {
//                    Set_FirebaseDatabase_Object(
//                        GetFirebaseDatabase_ID(GetFirebaseAuth_ID().ToString()),
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
//    private bool FirebaseListen_Login_Done = false;

//    /// <summary>
//    /// Check if Login is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAuth_Login_Done()
//    {
//        return FirebaseListen_Login_Done;
//    }

//    /// <summary>
//    /// Set Login Done check
//    /// </summary>
//    /// <param name="m_Chance"></param>
//    public void Set_FirebaseAuth_Login_Done(bool m_Chance)
//    {
//        this.FirebaseListen_Login_Done = m_Chance;
//    }

//    /// <summary>
//    /// Login User Auth to Firebase
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    public void Set_FirebaseListen_Login_ContinueWith(string s_Email, string s_Password)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        Set_Firebase_Message("Log-ing...");
//        if(m_Debug) Debug.LogWarning("Set_FirebaseListen_Login_ContinueWith: Log-ing...");

//        fm_Auth.SignInWithEmailAndPasswordAsync(s_Email, s_Password).ContinueWith(task =>
//        {
//            if (task.IsCanceled || task.IsFaulted)
//            {
//                if(m_Debug) Debug.LogError("Set_FirebaseListen_Login_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Login
//            FirebaseUser fm_AuthUser = task.Result;

//            Set_Firebase_Message("Login Complete");
//            if(m_Debug) Debug.LogWarning("Set_FirebaseListen_Login_ContinueWith: " + fm_Auth.CurrentUser.Email);
//        });
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase
//    /// </summary>
//    /// <param name="s_DisplayName"></param>
//    /// <param name="u_PhotoUrl"></param>
//    public void Set_FirebaseAuth_ChanceProfile_ContinueWith(string s_DisplayName, System.Uri u_PhotoUrl)
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            Firebase.Auth.UserProfile u_Profile = new Firebase.Auth.UserProfile
//            {
//                DisplayName = s_DisplayName,
//                PhotoUrl = u_PhotoUrl,
//                //PhotoUrl = new System.Uri("https://example.com/jane-q-user/profile.jpg")
//            };
//            //Update
//            fm_Auth.CurrentUser.UpdateUserProfileAsync(u_Profile).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceProfile_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChanceProfile_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceProfile_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    public void Set_FirebaseAuth_ChanceEmail_ContinueWith(string s_Email)
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            //Update
//            fm_Auth.CurrentUser.UpdateEmailAsync(s_Email).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceEmail_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChanceEmail_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceEmail_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase
//    /// </summary>
//    /// <param name="s_NewPassword"></param>
//    public void Set_FirebaseAuth_ChancePassword_ContinueWith(string s_NewPassword)
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            fm_Auth.CurrentUser.UpdatePasswordAsync(s_NewPassword).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChancePassword_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChancePassword_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChancePassword_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase
//    /// </summary>
//    public void Set_FirebaseAuth_DeleteAuth_ContinueWith()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            fm_Auth.CurrentUser.DeleteAsync().ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_DeleteAuth_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_DeleteAuth_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_DeleteAuth_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Send Email / Gmail Verification ContinueWith

//    /// <summary>
//    /// Check if Send Email is Done
//    /// </summary>
//    private bool m_FirebaseAuth_SendEmail_Done = false;

//    /// <summary>
//    /// Check if Send Email is Done
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAuth_SendEmail_Done()
//    {
//        return m_FirebaseAuth_SendEmail_Done;
//    }

//    /// <summary>
//    /// Set Send Email Done check
//    /// </summary>
//    /// <param name="m_Chance"></param>
//    public void Set_FirebaseAuth_SendEmail_Done(bool m_Chance)
//    {
//        this.m_FirebaseAuth_SendEmail_Done = m_Chance;
//    }

//    /// <summary>
//    /// Get Email Verified after Send Email
//    /// </summary>
//    /// <returns></returns>
//    public bool GetFirebaseAuth_EmailVerification_Check()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            fm_Auth.CurrentUser.ReloadAsync();
//            //Refreshes the data for this User Auth on Firebase

//            if(m_Debug) Debug.Log("GetFirebaseAuth_EmailVerification_Check: " + fm_Auth.CurrentUser.IsEmailVerified);
//            return fm_Auth.CurrentUser.IsEmailVerified;
//        }
//        if(m_Debug) Debug.Log("GetFirebaseAuth_EmailVerification_Check: Not Login yet");
//        return false;
//    }

//    /// <summary>
//    /// Send Email Verification on Firebase
//    /// </summary>
//    public void Set_FirebaseAuth_EmailVerification_Send_ContinueWith()
//    {
//        Set_FirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            fm_User_Cur.SendEmailVerificationAsync().ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    //If not Checked Mail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_EmailVerification_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                Set_FirebaseAuth_SendEmail_Done(true);

//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_EmailVerification_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase
//    /// </summary>
//    /// <param name="s_Email"></param>
//    public void Set_FirebaseAuth_PasswordResetEmail_Send_ContinueWith(string s_Email)
//    {
//        Set_FirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            fm_Auth.SendPasswordResetEmailAsync(s_Email).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    //If not Checked Mail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("Set_FirebaseAuth_PasswordResetEmail_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                Set_FirebaseAuth_SendEmail_Done(true);

//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_PasswordResetEmail_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    //Auth IEnumerator (In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));")
//    //"https://docs.unity3d.com/Manual/Coroutines.html"

//    //Register

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    /// <param name="s_DisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_Player/UserAuthID"</param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_Register_IEnumerator(string s_Email, string s_Password, string s_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            Set_FirebaseAuth_Register_Done(false);

//            Set_Firebase_Message("Register-ing...");
//            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: Register-ing");

//            var v_RegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(s_Email, s_Password);
//            //Create User Auth to Firebase

//            yield return new WaitUntil(predicate: () => v_RegisterTask.IsCompleted);
//            //Wait until Create User Auth to Firebase complete

//            Set_FirebaseAuth_Register_Done(true);

//            if (v_RegisterTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_RegisterTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        Set_Firebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Missing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        Set_Firebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Missing Password");
//                        break;
//                    case AuthError.WeakPassword:
//                        Set_Firebase_Message("Weak Password");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Weak Password");
//                        break;
//                    case AuthError.EmailAlreadyInUse:
//                        Set_Firebase_Message("Email Already In Use");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Email Already In Use");
//                        break;
//                }
//            }
//            else
//            //If Create User Auth to Firebase complete
//            {
//                Set_Firebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: " + s_Email);

//                FirebaseUser fm_AuthUser = v_RegisterTask.Result;

//                if (fm_AuthUser != null)
//                //If Login new User Auth to Firebase complete
//                {
//                    //Send Email

//                    if (m_EmailVerification)
//                    {
//                        Set_FirebaseAuth_EmailVerification_Send_ContinueWith();
//                    }

//                    //Set Profile

//                    UserProfile u_Profile = new UserProfile { DisplayName = s_DisplayName };
//                    //Create User Profile (DisplayName) Auth to Firebase

//                    var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//                    //Update User Auth Profile

//                    yield return new WaitUntil(predicate: () => v_ProfileTask.IsCompleted);
//                    //Wait until Update User Profile Auth to Firebase complete

//                    if (v_ProfileTask.Exception != null)
//                    //If Update User Profile Auth to Firebase Error
//                    {
//                        FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                        AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: " + s_DisplayName);
//                    }
//                    else
//                    {
//                        if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: Successfully!");
//                    }

//                    if (o_Class != null)
//                    {
//                        Set_FirebaseDatabase_Object(
//                            "_Player/" + GetFirebaseAuth_ID().ToString(),
//                            o_Class);
//                        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"
//                    }
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    /// <param name="s_PasswordRe">Check if "Re-Password" same to "Password"</param>
//    /// <param name="s_DisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_Register_IEnumerator(string s_Email, string s_Password, string s_PasswordRe, string s_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            if (s_Password != s_PasswordRe)
//            {
//                Set_Firebase_Message("Check Password Again");
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: Not same Password");
//            }
//            else
//            {
//                Set_FirebaseAuth_Register_Done(false);

//                Set_Firebase_Message("Register-ing...");
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: Register-ing");

//                var v_RegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(s_Email, s_Password);
//                //Create User Auth to Firebase

//                yield return new WaitUntil(predicate: () => v_RegisterTask.IsCompleted);
//                //Wait until Create User Auth to Firebase complete

//                Set_FirebaseAuth_Register_Done(true);

//                if (v_RegisterTask.Exception != null)
//                //If Create User Auth to Firebase Error
//                {
//                    FirebaseException m_FirebaseEx = v_RegisterTask.Exception.GetBaseException() as FirebaseException;
//                    AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                    //Get Create User Auth to Firebase Error

//                    switch (a_ErrorCode)
//                    {
//                        case AuthError.MissingEmail:
//                            Set_Firebase_Message("Missing Email");
//                            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Missing Email");
//                            break;
//                        case AuthError.MissingPassword:
//                            Set_Firebase_Message("Missing Password");
//                            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Missing Password");
//                            break;
//                        case AuthError.WeakPassword:
//                            Set_Firebase_Message("Weak Password");
//                            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Weak Password");
//                            break;
//                        case AuthError.EmailAlreadyInUse:
//                            Set_Firebase_Message("Email Already In Use");
//                            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: Email Already In Use");
//                            break;
//                    }
//                }
//                else
//                //If Create User Auth to Firebase complete
//                {
//                    Set_Firebase_Message("Register Complete");
//                    if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: " + s_Email);

//                    FirebaseUser fm_AuthUser = v_RegisterTask.Result;

//                    if (fm_AuthUser != null)
//                    //If Login new User Auth to Firebase complete
//                    {
//                        //Send Email

//                        if (m_EmailVerification)
//                        {
//                            Set_FirebaseAuth_EmailVerification_Send_ContinueWith();
//                        }

//                        //Set Profile

//                        UserProfile u_Profile = new UserProfile { DisplayName = s_DisplayName };
//                        //Create User Profile (DisplayName) Auth to Firebase

//                        var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//                        //Update User Auth Profile

//                        yield return new WaitUntil(predicate: () => v_ProfileTask.IsCompleted);
//                        //Wait until Update User Profile Auth to Firebase complete

//                        if (v_ProfileTask.Exception != null)
//                        //If Update User Profile Auth to Firebase Error
//                        {
//                            FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                            AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_IEnumerator: " + s_DisplayName);
//                        }
//                        else
//                        {
//                            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Register_IEnumerator: Successfully!");
//                        }

//                        if (o_Class != null)
//                        {
//                            Set_FirebaseDatabase_Object(
//                                GetFirebaseDatabase_ID(GetFirebaseAuth_ID().ToString()),
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
//    /// <param name="s_ID"></param>
//    /// <returns>Get root string "_ID$UserAuthID/"</returns>
//    public string GetFirebaseDatabase_ID(string s_ID)
//    {
//        return "_ID" + s_ID;
//    }

//    //Login

//    /// <summary>
//    /// Login User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Login(...));"
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <param name="s_Password"></param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_Login_IEnumerator(string s_Email, string s_Password)
//    {
//        Class_String cs_String = new Class_String();
//        if (!cs_String.GetCheckEmail(s_Email))
//        {
//            Set_Firebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("Set_FirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            Set_FirebaseAuth_Login_Done(false);

//            Set_Firebase_Message("Log-ing...");
//            if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Login_IEnumerator: Log-ing...");

//            var v_LoginTask = fm_Auth.SignInWithEmailAndPasswordAsync(s_Email, s_Password);
//            //Login User Auth to Firebase

//            yield return new WaitUntil(predicate: () => v_LoginTask.IsCompleted);
//            //Wait until Create User Auth to Firebase complete

//            Set_FirebaseAuth_Login_Done(true);

//            if (v_LoginTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_LoginTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        Set_Firebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Login_IEnumerator: Missing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        Set_Firebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Login_IEnumerator: Missing Password");
//                        break;
//                    case AuthError.WrongPassword:
//                        Set_Firebase_Message("Wrong Password");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Login_IEnumerator: Wrong Password");
//                        break;
//                    case AuthError.InvalidEmail:
//                        Set_Firebase_Message("Invalid Email");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Login_IEnumerator: Invalid Email");
//                        break;
//                    case AuthError.UserNotFound:
//                        Set_Firebase_Message("Account does not exist");
//                        if(m_Debug) Debug.LogError("Set_FirebaseAuth_Login_IEnumerator: Account does not exist");
//                        break;
//                }
//            }
//            else
//            //If Login User Auth to Firebase complete
//            {
//                Set_Firebase_Message("Login Complete");
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_Login_IEnumerator: " + GetFirebaseAuth_Email());

//                FirebaseUser fm_AuthUser = v_LoginTask.Result;
//            }
//        }
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseAuth_Profile(...));"
//    /// </summary>
//    /// <param name="s_DisplayName"></param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_ChanceProfile_IEnumerator(string s_DisplayName)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            UserProfile u_Profile = new UserProfile { DisplayName = s_DisplayName };
//            //Create User Profile (DisplayName) Auth to Firebase

//            var v_ProfileTask = fm_AuthUser.UpdateUserProfileAsync(u_Profile);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_ProfileTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_ProfileTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_ProfileTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceProfile_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChanceProfile_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_ChanceEmail(...));"
//    /// </summary>
//    /// <param name="s_Email">"myEmail@gmail.com"</param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_ChanceEmail_IEnumerator(string s_Email)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_EmailTask = fm_AuthUser.UpdateEmailAsync(s_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_EmailTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_EmailTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_EmailTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChanceEmail_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChanceEmail_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_ChancePassword(...));"
//    /// </summary>
//    /// <param name="s_NewPassword"></param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_ChancePassword_IEnumerator(string s_NewPassword)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_PasswordTask = fm_AuthUser.UpdatePasswordAsync(s_NewPassword);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_PasswordTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_PasswordTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_PasswordTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_ChancePassword_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_ChancePassword_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_DeleteAuth(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_DeleteAuth_IEnumerator()
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_DeleteTask = fm_AuthUser.DeleteAsync();
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_DeleteTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_DeleteTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_DeleteTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_DeleteAuth_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_DeleteAuth_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Send Email / Gmail Verification ContinueWith

//    private int m_ResetEventTime = 100;

//    public void Set_ResetEventTime(int m_ResetEventTime)
//    {
//        this.m_ResetEventTime = m_ResetEventTime;
//    }

//    /// <summary>
//    /// Send Email Verification on Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_EmailVerification_Send(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_EmailVerification_Send_IEnumerator()
//    {
//        Set_FirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_VerificationTask = fm_AuthUser.SendEmailVerificationAsync();
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            Set_FirebaseAuth_SendEmail_Done(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_EmailVerification_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_EmailVerification_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase. In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_PasswordResetEmail_Send(...));"
//    /// </summary>
//    /// <param name="s_Email"></param>
//    /// <returns></returns>
//    public IEnumerator Set_FirebaseAuth_PasswordResetEmail_Send_IEnumerator(string s_Email)
//    {
//        Set_FirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            var v_VerificationTask = fm_Auth.SendPasswordResetEmailAsync(s_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            Set_FirebaseAuth_SendEmail_Done(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("Set_FirebaseAuth_PasswordResetEmail_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("Set_FirebaseAuth_PasswordResetEmail_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //=========================================================================================

//    //Event FirebaseDatabase Listener (Set at Start or Awake)

//    //Value Changed

//    /// <summary>
//    /// Add Event Value Changed Listener by "Set_FirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ValueChanged(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged");
//        da_Reference.ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "Set_FirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ValueChanged_Reset(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ValueChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "Set_FirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ValueChanged(string s_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged: " + s_DatabaseAcess);
//        da_Reference.Child(s_DatabaseAcess).ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "Set_FirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ValueChanged_Reset(string s_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset: " + s_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++) 
//            da_Reference.Child(s_DatabaseAcess).ValueChanged -= e_EventHandler;
//    }

//    //Child Added

//    /// <summary>
//    /// Add Event Child Added Listener by "Set_FirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildAdded(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildAdded");
//        da_Reference.ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "Set_FirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildAdded_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildAdded -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Added Listener by "Set_FirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildAdded(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildAdded: " + s_DatabaseAcess);
//        da_Reference.Child(s_DatabaseAcess).ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "Set_FirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildAdded_Reset(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset: " + s_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(s_DatabaseAcess).ChildAdded -= e_EventHandler;
//    }

//    //Child Changed

//    /// <summary>
//    /// Add Event Child Changed Listener by "Set_FirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildChanged(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildChanged");
//        da_Reference.ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "Set_FirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildChanged_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Changed Listener by "Set_FirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildChanged(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildChanged: " + s_DatabaseAcess);
//        da_Reference.Child(s_DatabaseAcess).ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "Set_FirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildChanged_Reset(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset: " + s_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(s_DatabaseAcess).ChildChanged -= e_EventHandler;
//    }

//    //Child Moved

//    /// <summary>
//    /// Add Event Child Moved Listener by "Set_FirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildMoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildMoved");
//        da_Reference.ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Moved Listener by "Set_FirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildMoved_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildMoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Moved Listener by "Set_FirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildMoved(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildMoved: " + s_DatabaseAcess);
//        da_Reference.Child(s_DatabaseAcess).ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Moved Listener by "Set_FirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildMoved_Reset(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset: " + s_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(s_DatabaseAcess).ChildMoved -= e_EventHandler;
//    }

//    //Child Removed

//    /// <summary>
//    /// Add Event Child Removed Listener by "Set_FirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildRemoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildRemoved");
//        da_Reference.ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "Set_FirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildRemoved_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildRemoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Removed Listener by "Set_FirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildRemoved(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ChildRemoved: " + s_DatabaseAcess);
//        da_Reference.Child(s_DatabaseAcess).ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "Set_FirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void Set_FirebaseEvent_ChildRemoved_Reset(string s_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("Set_FirebaseEvent_ValueChanged_Reset: " + s_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(s_DatabaseAcess).ChildRemoved -= e_EventHandler;
//    }

//    //=========================================================================================

//    //Script Message

//    private string s_FirebaseAuth_Message = "";

//    /// <summary>
//    /// Get Current Message
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAuth_Message()
//    {
//        return s_FirebaseAuth_Message;
//    }

//    /// <summary>
//    /// Clear Current Message
//    /// </summary>
//    public void Set_FirebaseAuth_Message_Clear()
//    {
//        s_FirebaseAuth_Message = "";
//    }

//    /// <summary>
//    /// Set Message
//    /// </summary>
//    /// <param name="s_Message"></param>
//    private void Set_Firebase_Message(string s_Message)
//    {
//        s_FirebaseAuth_Message = s_Message;
//    }

//    //=========================================================================================

//    //Firebase Database (Set)

//    /// <summary>
//    /// Primary Root Database on Firebase
//    /// </summary>
//    private DatabaseReference da_Reference;

//    /// <summary>
//    /// Update Child in Database with INT
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void Set_FirebaseDatabase_Value(string s_DatabaseAcess, int m_Value)
//    {
//        da_Reference.Child(s_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Value: " + "\"" + s_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with FLOAT
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void Set_FirebaseDatabase_Value(string s_DatabaseAcess, float m_Value)
//    {
//        da_Reference.Child(s_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Value: " + "\"" + s_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with STRING
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="s_Value"></param>
//    public void Set_FirebaseDatabase_Value(string s_DatabaseAcess, string s_Value)
//    {
//        da_Reference.Child(s_DatabaseAcess).SetValueAsync(s_Value);
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Value: " + "\"" + s_DatabaseAcess + "\" : \"" + s_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with BOOL
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void Set_FirebaseDatabase_Value(string s_DatabaseAcess, bool m_Value)
//    {
//        da_Reference.Child(s_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Value: " + "\"" + s_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with OBJECT
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="o_Class">"MyClass" see example in "Android_Firebase_UserClass.cs"</param>
//    public void Set_FirebaseDatabase_Object(string s_DatabaseAcess, object o_Class)
//    {
//        string s_Json = JsonUtility.ToJson(o_Class);
//        da_Reference.Child(s_DatabaseAcess).SetRawJsonValueAsync(s_Json);
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Object: " + "\"" + s_DatabaseAcess + "\" : \"" + s_Json + "\"");
//    }

//    /// <summary>
//    /// Delete Child in Database
//    /// </summary>
//    /// <param name="s_DatabaseAcess">"Parent/KeyDelete" or "KeyDelete"</param>
//    public void Set_FirebaseDatabase_Delete(string s_DatabaseAcess)
//    {
//        da_Reference.Child(s_DatabaseAcess).RemoveValueAsync();
//        if(m_Debug) Debug.Log("Set_FirebaseDatabase_Delete: " + "\"" + s_DatabaseAcess + "\"");
//    }

//    //Data

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    private Class_Data cl_Data;

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    /// <returns></returns>
//    public Class_Data GetData()
//    {
//        return cl_Data;
//    }

//    //Firebase Database (Get)

//    private void GetDataSnapshot_Value_Debug(DataSnapshot d_Snapshot, string s_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(s_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + d_Snapshot.Value.ToString() + "\"");
//    }

//    private void GetDataSnapshot_Value_Debug(DataSnapshot d_Snapshot, string s_Json, string s_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(s_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + s_Json + "\"");
//    }

//    private void GetDataSnapshot_NotExist_Debug(DataSnapshot d_Snapshot, string s_Debug)
//    {
//        if(m_Debug) Debug.LogError(s_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + cl_Data.GetData_NotFound() + "\"");
//    }

//    private bool GetDataSnapshotIsExist(DataSnapshot d_Snapshot)
//    {
//        return d_Snapshot.Exists;
//    }

//    //Firebase Database ContinueWith (Primary)

//    /// <summary>
//    /// Check if Get from Firebase Database is Done
//    /// </summary>
//    /// <returns>If Convert fail, return FALSE</returns>
//    public bool GetFirebaseDatabase_GetDone(string s_ProgessSaveName)
//    {
//        return GetData().GetConvert_Bool(GetData().GetData(s_ProgessSaveName));
//    }

//    /// <summary>
//    /// Set Get from Firebase Database Done check
//    /// </summary>
//    /// <param name="m_ProgessChance"></param>
//    public void Set_FirebaseDatabase_GetDone(string s_ProgessSaveName, bool m_ProgessChance)
//    {
//        GetData().Set_Data(s_ProgessSaveName, m_ProgessChance);
//    }

//    /// <summary>
//    /// Get Signle Data inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public void Set_FirebaseDatabase_ValueSingle_ContinueWith(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        da_Reference.Child(s_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("Set_FirebaseDatabase_ValueSingle_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string s_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if(s_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "Set_FirebaseDatabase_ValueSingle_ContinueWith: ");

//                //    cl_Data.Set_Data(s_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, s_Json, "Set_FirebaseDatabase_ValueSingle_ContinueWith: ");
//                //}

//                GetDataSnapshot_Value_Debug(
//                        d_Snapshot, "Set_FirebaseDatabase_ValueSingle_ContinueWith: ");

//                cl_Data.Set_Data(s_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_ValueSingle_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get List of Key inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public void Set_FirebaseDatabase_KeyList_ContinueWith(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        da_Reference.Child(s_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("Set_FirebaseDatabase_KeyList_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string s_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (s_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "Set_FirebaseDatabase_KeyList_ContinueWith: ");
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, s_Json, "Set_FirebaseDatabase_KeyList_ContinueWith: ");

//                //    cl_Data.Set_Index_Restart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cl_Data.Set_Index_Plus();
//                //        //Plus Index Data by 1

//                //        cl_Data.Set_Data(s_DataNameSave, cl_Data.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cl_Data.Set_DataCount(s_DataNameSave, cl_Data.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, s_Json, "Set_FirebaseDatabase_KeyList_ContinueWith: ");

//                cl_Data.Set_Index_Restart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cl_Data.Set_Index_Plus();
//                    //Plus Index Data by 1

//                    cl_Data.Set_Data(s_DataNameSave, cl_Data.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cl_Data.Set_DataCount(s_DataNameSave, cl_Data.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyList_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get Key Exist inside Key Get from Firebase Database, then Save Bool Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public void Set_FirebaseDatabase_KeyExist_ContinueWith(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        da_Reference.Child(s_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("Set_FirebaseDatabase_KeyExist_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyExist_ContinueWith: ");

//                cl_Data.Set_Data(s_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyExist_ContinueWith: ");

//                cl_Data.Set_Data(s_DataNameSave, false);
//                //Save Data (Value)
//            }
//        });
//    }

//    //Firebase Database IEnumerator

//    /// <summary>
//    /// Get Signle Data inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()"). 
//    /// In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public IEnumerator Set_FirebaseDatabase_ValueSingle_IEnumerator(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(s_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string s_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (s_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "Set_FirebaseDatabase_ValueSingle_IEnumerator: ");

//                //    cl_Data.Set_Data(s_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, s_Json, "Set_FirebaseDatabase_ValueSingle_IEnumerator: ");
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_ValueSingle_IEnumerator: ");

//                cl_Data.Set_Data(s_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_ValueSingle_IEnumerator: ");
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseDatabase_KeyList_IEnumerator: UnSuccessfully!");
//        }
//    }

//    /// <summary>
//    /// Get List of Key inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()").
//    /// In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public IEnumerator Set_FirebaseDatabase_KeyList_IEnumerator(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(s_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string s_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (s_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "Set_FirebaseDatabase_KeyList_IEnumerator: ");
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, s_Json, "Set_FirebaseDatabase_KeyList_IEnumerator: ");

//                //    cl_Data.Set_Index_Restart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cl_Data.Set_Index_Plus();
//                //        //Plus Index Data by 1

//                //        cl_Data.Set_Data(s_DataNameSave, cl_Data.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cl_Data.Set_DataCount(s_DataNameSave, cl_Data.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, s_Json, "Set_FirebaseDatabase_KeyList_IEnumerator: ");

//                cl_Data.Set_Index_Restart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cl_Data.Set_Index_Plus();
//                    //Plus Index Data by 1

//                    cl_Data.Set_Data(s_DataNameSave, cl_Data.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cl_Data.Set_DataCount(s_DataNameSave, cl_Data.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyList_IEnumerator: ");
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseDatabase_KeyList_IEnumerator: UnSuccessfully!");
//        }
//    }

//    /// <summary>
//    /// Get Key Exist inside Key Get from Firebase Database, then Save Bool Data (Get Data from "GetData()")
//    /// In "MonoBehavior", use "StartCoroutine(Set_FirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="s_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="s_DataNameSave"></param>
//    public IEnumerator Set_FirebaseDatabase_KeyExist_IEnumerator(string s_DatabaseAccess, string s_DataNameSave, string s_ProgessSaveName)
//    {
//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(s_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        Set_FirebaseDatabase_GetDone(s_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyExist_IEnumerator: ");

//                cl_Data.Set_Data(s_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "Set_FirebaseDatabase_KeyExist_IEnumerator: ");

//                cl_Data.Set_Data(s_DataNameSave, false);
//                //Save Data (Value)
//            }
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("Set_FirebaseDatabase_KeyExist_IEnumerator: UnSuccessfully!");
//        }
//    }

//    //=========================================================================================
//}