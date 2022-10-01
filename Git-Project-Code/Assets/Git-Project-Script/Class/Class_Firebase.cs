//using System.Collections.Generic;
//using System.Collections;
//using UnityEngine;

//using Firebase;
//using Firebase.Auth;
//using Firebase.Database;

///// <summary>
///// Working on Firebasee Primary for Android
///// </summary>
//public class Clasm_Firebase
////Firebasee Primary for Android
//{
//    public bool m_Debug = false;

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public Clasm_Firebase()
//    {
//        SetFirebaseStart();
//        this.m_Debug = false;
//    }

//    /// <summary>
//    /// Working on Firebasee Primary for Android
//    /// </summary>
//    public Clasm_Firebase(bool m_Debug)
//    {
//        SetFirebaseStart();
//        this.m_Debug = m_Debug;
//    }

//    private void SetFirebaseStart()
//    {
//        fm_Auth = FirebaseAuth.DefaultInstance;
//        //Auth on Firebase
//        //Active Auth on Android

//        da_Reference = FirebaseDatabase.DefaultInstance.RootReference;
//        //Database on Firebase

//        cl_Data = new Clasm_Data();
//        //Working on Data

//        FirebaseApp.Create(new AppOptions(), GetFirebaseAuth_ID());
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
//    public void SetFirebaseAuth_SignOut()
//    {
//        if(m_Debug) Debug.LogWarning("SetFirebaseAuth_SignOut: Sign Out!");
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
//    public void SetFirebaseAuth_Register_Done(bool m_Chance)
//    {
//        this.m_FirebaseAuth_Register_Done = m_Chance;
//    }

//    /// <summary>
//    /// Create User Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void SetFirebaseAuth_Register_ContinueWith(string m_Email, string m_Password, object o_Class)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        SetFirebaseAuth_Register_Done(false);

//        SetFirebase_Message("Creating...");
//        if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_ContinueWith: Creating");

//        fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//        {
//            if (task.IsCanceled || task.IsFaulted)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Create
//            SetFirebaseAuth_Register_Done(true);

//            FirebaseUser fm_AuthUser = task.Result;

//            SetFirebase_Message("Register Complete");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_ContinueWith: " + fm_Auth.CurrentUser.Email);

//            if (o_Class != null)
//            {
//                SetFirebaseDatabase_Object(
//                    "_Player/" + GetFirebaseAuth_ID().ToString(),
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
//    /// <param name="m_RePassword"></param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    public void SetFirebaseAuth_Register_ContinueWith(string m_Email, string m_Password, string m_RePassword, object o_Class)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        if (m_Password != m_RePassword)
//        {
//            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_ContinueWith: Not same Password");
//            return;
//        }
//        else
//        {
//            SetFirebaseAuth_Register_Done(false);

//            SetFirebase_Message("Creating...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_ContinueWith: Creating");

//            fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                //Create
//                SetFirebaseAuth_Register_Done(true);

//                FirebaseUser fm_AuthUser = task.Result;

//                SetFirebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_ContinueWith: " + fm_Auth.CurrentUser.Email);

//                if (o_Class != null)
//                {
//                    SetFirebaseDatabase_Object(
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
//    public void SetFirebaseAuth_Login_Done(bool m_Chance)
//    {
//        this.FirebaseListen_Login_Done = m_Chance;
//    }

//    /// <summary>
//    /// Login User Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    public void SetFirebaseListen_Login_ContinueWith(string m_Email, string m_Password)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//            return;
//        }

//        SetFirebase_Message("Log-ing...");
//        if(m_Debug) Debug.LogWarning("SetFirebaseListen_Login_ContinueWith: Log-ing...");

//        fm_Auth.SignInWithEmailAndPasswordAsync(m_Email, m_Password).ContinueWith(task =>
//        {
//            if (task.IsCanceled || task.IsFaulted)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseListen_Login_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Login
//            FirebaseUser fm_AuthUser = task.Result;

//            SetFirebase_Message("Login Complete");
//            if(m_Debug) Debug.LogWarning("SetFirebaseListen_Login_ContinueWith: " + fm_Auth.CurrentUser.Email);
//        });
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase
//    /// </summary>
//    /// <param name="m_DisplayName"></param>
//    /// <param name="u_PhotoUrl"></param>
//    public void SetFirebaseAuth_ChanceProfile_ContinueWith(string m_DisplayName, System.Uri u_PhotoUrl)
//    {
//        if (GetFirebaseAuth_Login())
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
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceProfile_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChanceProfile_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceProfile_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    public void SetFirebaseAuth_ChanceEmail_ContinueWith(string m_Email)
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            //Update
//            fm_Auth.CurrentUser.UpdateEmailAsync(m_Email).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceEmail_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChanceEmail_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceEmail_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase
//    /// </summary>
//    /// <param name="m_NewPassword"></param>
//    public void SetFirebaseAuth_ChancePassword_ContinueWith(string m_NewPassword)
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            fm_Auth.CurrentUser.UpdatePasswordAsync(m_NewPassword).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_ChancePassword_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChancePassword_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_ChancePassword_ContinueWith: UnSuccessfully!");
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase
//    /// </summary>
//    public void SetFirebaseAuth_DeleteAuth_ContinueWith()
//    {
//        if (GetFirebaseAuth_Login())
//        {
//            fm_Auth.CurrentUser.DeleteAsync().ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_DeleteAuth_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_DeleteAuth_ContinueWith: Successfully!");
//            });
//        }
//        else
//        {
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_DeleteAuth_ContinueWith: UnSuccessfully!");
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
//    public void SetFirebaseAuth_SendEmail_Done(bool m_Chance)
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
//    public void SetFirebaseAuth_EmailVerification_Send_ContinueWith()
//    {
//        SetFirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            fm_User_Cur.SendEmailVerificationAsync().ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    //If not Checked Mail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_EmailVerification_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                SetFirebaseAuth_SendEmail_Done(true);

//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_EmailVerification_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase
//    /// </summary>
//    /// <param name="m_Email"></param>
//    public void SetFirebaseAuth_PasswordResetEmail_Send_ContinueWith(string m_Email)
//    {
//        SetFirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            fm_Auth.SendPasswordResetEmailAsync(m_Email).ContinueWith(task =>
//            {
//                if (task.IsCanceled || task.IsFaulted)
//                {
//                    //If not Checked Mail, this will throuw Exception
//                    if(m_Debug) Debug.LogError("SetFirebaseAuth_PasswordResetEmail_Send_ContinueWith: UnSuccessfully!");
//                    return;
//                }
//                SetFirebaseAuth_SendEmail_Done(true);

//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_PasswordResetEmail_Send_ContinueWith: Successfully!");
//            });
//        }
//    }

//    //Auth IEnumerator (In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));")
//    //"https://docs.unity3d.com/Manual/Coroutines.html"

//    //Register

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="m_DisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_Player/UserAuthID"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_Register_IEnumerator(string m_Email, string m_Password, string m_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            SetFirebaseAuth_Register_Done(false);

//            SetFirebase_Message("Register-ing...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: Register-ing");

//            var v_RegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password);
//            //Create User Auth to Firebase

//            yield return new WaitUntil(predicate: () => v_RegisterTask.IsCompleted);
//            //Wait until Create User Auth to Firebase complete

//            SetFirebaseAuth_Register_Done(true);

//            if (v_RegisterTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_RegisterTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        SetFirebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Missing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        SetFirebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Missing Password");
//                        break;
//                    case AuthError.WeakPassword:
//                        SetFirebase_Message("Weak Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Weak Password");
//                        break;
//                    case AuthError.EmailAlreadyInUse:
//                        SetFirebase_Message("Email Already In Use");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Email Already In Use");
//                        break;
//                }
//            }
//            else
//            //If Create User Auth to Firebase complete
//            {
//                SetFirebase_Message("Register Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: " + m_Email);

//                FirebaseUser fm_AuthUser = v_RegisterTask.Result;

//                if (fm_AuthUser != null)
//                //If Login new User Auth to Firebase complete
//                {
//                    //Send Email

//                    if (m_EmailVerification)
//                    {
//                        SetFirebaseAuth_EmailVerification_Send_ContinueWith();
//                    }

//                    //Set Profile

//                    UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
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
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: " + m_DisplayName);
//                    }
//                    else
//                    {
//                        if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: Successfully!");
//                    }

//                    if (o_Class != null)
//                    {
//                        SetFirebaseDatabase_Object(
//                            "_Player/" + GetFirebaseAuth_ID().ToString(),
//                            o_Class);
//                        //Create Primary User Auth Profile in Firebase Database at "_Player/$UserAuthID/"
//                    }
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// Create User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <param name="m_PasswordRe">Check if "Re-Password" same to "Password"</param>
//    /// <param name="m_DisplayName"></param>
//    /// <param name="m_EmailVerification">If "True", Firebase will send an Email Verification</param>
//    /// <param name="o_Class">Add "new m_yClass()" to Firebase Database at "_ID$UserAuthID/"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_Register_IEnumerator(string m_Email, string m_Password, string m_PasswordRe, string m_DisplayName, bool m_EmailVerification, object o_Class)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            if (m_Password != m_PasswordRe)
//            {
//                SetFirebase_Message("Check Password Again");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: Not same Password");
//            }
//            else
//            {
//                SetFirebaseAuth_Register_Done(false);

//                SetFirebase_Message("Register-ing...");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: Register-ing");

//                var v_RegisterTask = fm_Auth.CreateUserWithEmailAndPasswordAsync(m_Email, m_Password);
//                //Create User Auth to Firebase

//                yield return new WaitUntil(predicate: () => v_RegisterTask.IsCompleted);
//                //Wait until Create User Auth to Firebase complete

//                SetFirebaseAuth_Register_Done(true);

//                if (v_RegisterTask.Exception != null)
//                //If Create User Auth to Firebase Error
//                {
//                    FirebaseException m_FirebaseEx = v_RegisterTask.Exception.GetBaseException() as FirebaseException;
//                    AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                    //Get Create User Auth to Firebase Error

//                    switch (a_ErrorCode)
//                    {
//                        case AuthError.MissingEmail:
//                            SetFirebase_Message("Missing Email");
//                            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Missing Email");
//                            break;
//                        case AuthError.MissingPassword:
//                            SetFirebase_Message("Missing Password");
//                            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Missing Password");
//                            break;
//                        case AuthError.WeakPassword:
//                            SetFirebase_Message("Weak Password");
//                            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Weak Password");
//                            break;
//                        case AuthError.EmailAlreadyInUse:
//                            SetFirebase_Message("Email Already In Use");
//                            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: Email Already In Use");
//                            break;
//                    }
//                }
//                else
//                //If Create User Auth to Firebase complete
//                {
//                    SetFirebase_Message("Register Complete");
//                    if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: " + m_Email);

//                    FirebaseUser fm_AuthUser = v_RegisterTask.Result;

//                    if (fm_AuthUser != null)
//                    //If Login new User Auth to Firebase complete
//                    {
//                        //Send Email

//                        if (m_EmailVerification)
//                        {
//                            SetFirebaseAuth_EmailVerification_Send_ContinueWith();
//                        }

//                        //Set Profile

//                        UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
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
//                            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_IEnumerator: " + m_DisplayName);
//                        }
//                        else
//                        {
//                            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Register_IEnumerator: Successfully!");
//                        }

//                        if (o_Class != null)
//                        {
//                            SetFirebaseDatabase_Object(
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
//    /// <param name="m_ID"></param>
//    /// <returns>Get root string "_ID$UserAuthID/"</returns>
//    public string GetFirebaseDatabase_ID(string m_ID)
//    {
//        return "_ID" + m_ID;
//    }

//    //Login

//    /// <summary>
//    /// Login User Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Login(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <param name="m_Password"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_Login_IEnumerator(string m_Email, string m_Password)
//    {
//        Clasm_String cm_String = new Clasm_String();
//        if (!cm_String.GetCheckEmail(m_Email))
//        {
//            SetFirebase_Message("Invalid Email...");
//            if(m_Debug) Debug.LogError("SetFirebaseAuth_Register_ContinueWith: Invalid Email");
//        }
//        else
//        {
//            SetFirebaseAuth_Login_Done(false);

//            SetFirebase_Message("Log-ing...");
//            if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Login_IEnumerator: Log-ing...");

//            var v_LoginTask = fm_Auth.SignInWithEmailAndPasswordAsync(m_Email, m_Password);
//            //Login User Auth to Firebase

//            yield return new WaitUntil(predicate: () => v_LoginTask.IsCompleted);
//            //Wait until Create User Auth to Firebase complete

//            SetFirebaseAuth_Login_Done(true);

//            if (v_LoginTask.Exception != null)
//            //If Create User Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_LoginTask.Exception.GetBaseException() as FirebaseException;
//                AuthError a_ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                //Get Create User Auth to Firebase Error

//                switch (a_ErrorCode)
//                {
//                    case AuthError.MissingEmail:
//                        SetFirebase_Message("Missing Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Login_IEnumerator: Missing Email");
//                        break;
//                    case AuthError.MissingPassword:
//                        SetFirebase_Message("Missing Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Login_IEnumerator: Missing Password");
//                        break;
//                    case AuthError.WrongPassword:
//                        SetFirebase_Message("Wrong Password");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Login_IEnumerator: Wrong Password");
//                        break;
//                    case AuthError.InvalidEmail:
//                        SetFirebase_Message("Invalid Email");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Login_IEnumerator: Invalid Email");
//                        break;
//                    case AuthError.UserNotFound:
//                        SetFirebase_Message("Account does not exist");
//                        if(m_Debug) Debug.LogError("SetFirebaseAuth_Login_IEnumerator: Account does not exist");
//                        break;
//                }
//            }
//            else
//            //If Login User Auth to Firebase complete
//            {
//                SetFirebase_Message("Login Complete");
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_Login_IEnumerator: " + GetFirebaseAuth_Email());

//                FirebaseUser fm_AuthUser = v_LoginTask.Result;
//            }
//        }
//    }

//    //Chance Profile

//    /// <summary>
//    /// Create User Profile Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseAuth_Profile(...));"
//    /// </summary>
//    /// <param name="m_DisplayName"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_ChanceProfile_IEnumerator(string m_DisplayName)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            UserProfile u_Profile = new UserProfile { DisplayName = m_DisplayName };
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
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceProfile_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChanceProfile_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Email

//    /// <summary>
//    /// Chance Email Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_ChanceEmail(...));"
//    /// </summary>
//    /// <param name="m_Email">"myEmail@gmail.com"</param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_ChanceEmail_IEnumerator(string m_Email)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_EmailTask = fm_AuthUser.UpdateEmailAsync(m_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_EmailTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_EmailTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_EmailTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_ChanceEmail_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChanceEmail_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Chance Password

//    /// <summary>
//    /// Chance Password Auth to Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_ChancePassword(...));"
//    /// </summary>
//    /// <param name="m_NewPassword"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_ChancePassword_IEnumerator(string m_NewPassword)
//    {
//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_PasswordTask = fm_AuthUser.UpdatePasswordAsync(m_NewPassword);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_PasswordTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            if (v_PasswordTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_PasswordTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_ChancePassword_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_ChancePassword_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Delete Auth

//    /// <summary>
//    /// Delete Auth on Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_DeleteAuth(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_DeleteAuth_IEnumerator()
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
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_DeleteAuth_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_DeleteAuth_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //Send Email / Gmail Verification ContinueWith

//    private int m_ResetEventTime = 100;

//    public void SetResetEventTime(int m_ResetEventTime)
//    {
//        this.m_ResetEventTime = m_ResetEventTime;
//    }

//    /// <summary>
//    /// Send Email Verification on Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_EmailVerification_Send(...));"
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_EmailVerification_Send_IEnumerator()
//    {
//        SetFirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_AuthUser = fm_Auth.CurrentUser;
//        if (fm_AuthUser != null)
//        {
//            var v_VerificationTask = fm_AuthUser.SendEmailVerificationAsync();
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            SetFirebaseAuth_SendEmail_Done(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_EmailVerification_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_EmailVerification_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    /// <summary>
//    /// Send Email Password Verification on Firebase. In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_PasswordResetEmail_Send(...));"
//    /// </summary>
//    /// <param name="m_Email"></param>
//    /// <returns></returns>
//    public IEnumerator SetFirebaseAuth_PasswordResetEmail_Send_IEnumerator(string m_Email)
//    {
//        SetFirebaseAuth_SendEmail_Done(false);

//        FirebaseUser fm_User_Cur = fm_Auth.CurrentUser;
//        if (fm_User_Cur != null)
//        {
//            var v_VerificationTask = fm_Auth.SendPasswordResetEmailAsync(m_Email);
//            //Update User Auth Profile

//            yield return new WaitUntil(predicate: () => v_VerificationTask.IsCompleted);
//            //Wait until Update User Profile Auth to Firebase complete

//            SetFirebaseAuth_SendEmail_Done(true);

//            if (v_VerificationTask.Exception != null)
//            //If Update User Profile Auth to Firebase Error
//            {
//                FirebaseException m_FirebaseEx = v_VerificationTask.Exception.GetBaseException() as FirebaseException;
//                AuthError ErrorCode = (AuthError)m_FirebaseEx.ErrorCode;
//                if(m_Debug) Debug.LogError("SetFirebaseAuth_PasswordResetEmail_Send_IEnumerator: UnSuccessfully!");
//            }
//            else
//            {
//                if(m_Debug) Debug.LogWarning("SetFirebaseAuth_PasswordResetEmail_Send_IEnumerator: Successfully!");
//            }
//        }
//    }

//    //=========================================================================================

//    //Event FirebaseDatabase Listener (Set at Start or Awake)

//    //Value Changed

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ValueChanged(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged");
//        da_Reference.ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ValueChanged_Reset(System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ValueChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ValueChanged(string m_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged: " + m_DatabaseAcess);
//        da_Reference.Child(m_DatabaseAcess).ValueChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Value Changed Listener by "SetFirebaseEvent_ValueChanged(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ValueChanged_Reset(string m_DatabaseAcess, System.EventHandler<ValueChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++) 
//            da_Reference.Child(m_DatabaseAcess).ValueChanged -= e_EventHandler;
//    }

//    //Child Added

//    /// <summary>
//    /// Add Event Child Added Listener by "SetFirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildAdded(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildAdded");
//        da_Reference.ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "SetFirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildAdded_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildAdded -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Added Listener by "SetFirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildAdded(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildAdded: " + m_DatabaseAcess);
//        da_Reference.Child(m_DatabaseAcess).ChildAdded += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Added Listener by "SetFirebaseEvent_ChildAdded(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildAdded_Reset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(m_DatabaseAcess).ChildAdded -= e_EventHandler;
//    }

//    //Child Changed

//    /// <summary>
//    /// Add Event Child Changed Listener by "SetFirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildChanged(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildChanged");
//        da_Reference.ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "SetFirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildChanged_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildChanged -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Changed Listener by "SetFirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildChanged(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildChanged: " + m_DatabaseAcess);
//        da_Reference.Child(m_DatabaseAcess).ChildChanged += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Changed Listener by "SetFirebaseEvent_ChildChanged(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildChanged_Reset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(m_DatabaseAcess).ChildChanged -= e_EventHandler;
//    }

//    //Child Moved

//    /// <summary>
//    /// Add Event Child Moved Listener by "SetFirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildMoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildMoved");
//        da_Reference.ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Moved Listener by "SetFirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildMoved_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildMoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Moved Listener by "SetFirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildMoved(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildMoved: " + m_DatabaseAcess);
//        da_Reference.Child(m_DatabaseAcess).ChildMoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Moved Listener by "SetFirebaseEvent_ChildMoved(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildMoved_Reset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(m_DatabaseAcess).ChildMoved -= e_EventHandler;
//    }

//    //Child Removed

//    /// <summary>
//    /// Add Event Child Removed Listener by "SetFirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildRemoved(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildRemoved");
//        da_Reference.ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "SetFirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildRemoved_Reset(System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset");

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.ChildRemoved -= e_EventHandler;
//    }

//    /// <summary>
//    /// Add Event Child Removed Listener by "SetFirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildRemoved(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ChildRemoved: " + m_DatabaseAcess);
//        da_Reference.Child(m_DatabaseAcess).ChildRemoved += e_EventHandler;
//    }

//    /// <summary>
//    /// Remove Event Child Removed Listener by "SetFirebaseEvent_ChildRemoved(myEvent);"
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyEvent" or "KeyEvent"</param>
//    /// <param name="e_EventHandler">Add  "void m_yEvent(object sender, ValueChangedEventArgs e)"</param>
//    public void SetFirebaseEvent_ChildRemoved_Reset(string m_DatabaseAcess, System.EventHandler<ChildChangedEventArgs> e_EventHandler)
//    {
//        if(m_Debug) Debug.Log("SetFirebaseEvent_ValueChanged_Reset: " + m_DatabaseAcess);

//        for (int i = 0; i < m_ResetEventTime; i++)
//            da_Reference.Child(m_DatabaseAcess).ChildRemoved -= e_EventHandler;
//    }

//    //=========================================================================================

//    //Script Message

//    private string m_FirebaseAuth_Message = "";

//    /// <summary>
//    /// Get Current Message
//    /// </summary>
//    /// <returns></returns>
//    public string GetFirebaseAuth_Message()
//    {
//        return m_FirebaseAuth_Message;
//    }

//    /// <summary>
//    /// Clear Current Message
//    /// </summary>
//    public void SetFirebaseAuth_Message_Clear()
//    {
//        m_FirebaseAuth_Message = "";
//    }

//    /// <summary>
//    /// Set Message
//    /// </summary>
//    /// <param name="m_Message"></param>
//    private void SetFirebase_Message(string m_Message)
//    {
//        m_FirebaseAuth_Message = m_Message;
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
//    /// <param name="m_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, int m_Value)
//    {
//        da_Reference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with FLOAT
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, float m_Value)
//    {
//        da_Reference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with STRING
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, string m_Value)
//    {
//        da_Reference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with BOOL
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="m_Value"></param>
//    public void SetFirebaseDatabase_Value(string m_DatabaseAcess, bool m_Value)
//    {
//        da_Reference.Child(m_DatabaseAcess).SetValueAsync(m_Value);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Value: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Value + "\"");
//    }

//    /// <summary>
//    /// Update Child in Database with OBJECT
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyUpdate" or "KeyUpdate"</param>
//    /// <param name="o_Class">"MyClass" see example in "Android_Firebase_UserClass.cs"</param>
//    public void SetFirebaseDatabase_Object(string m_DatabaseAcess, object o_Class)
//    {
//        string m_Json = JsonUtility.ToJson(o_Class);
//        da_Reference.Child(m_DatabaseAcess).SetRawJsonValueAsync(m_Json);
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Object: " + "\"" + m_DatabaseAcess + "\" : \"" + m_Json + "\"");
//    }

//    /// <summary>
//    /// Delete Child in Database
//    /// </summary>
//    /// <param name="m_DatabaseAcess">"Parent/KeyDelete" or "KeyDelete"</param>
//    public void SetFirebaseDatabase_Delete(string m_DatabaseAcess)
//    {
//        da_Reference.Child(m_DatabaseAcess).RemoveValueAsync();
//        if(m_Debug) Debug.Log("SetFirebaseDatabase_Delete: " + "\"" + m_DatabaseAcess + "\"");
//    }

//    //Data

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    private Clasm_Data cl_Data;

//    /// <summary>
//    /// Working on Data
//    /// </summary>
//    /// <returns></returns>
//    public Clasm_Data GetData()
//    {
//        return cl_Data;
//    }

//    //Firebase Database (Get)

//    private void GetDataSnapshot_Value_Debug(DataSnapshot d_Snapshot, string m_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + d_Snapshot.Value.ToString() + "\"");
//    }

//    private void GetDataSnapshot_Value_Debug(DataSnapshot d_Snapshot, string m_Json, string m_Debug)
//    {
//        if(m_Debug) Debug.LogWarning(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + m_Json + "\"");
//    }

//    private void GetDataSnapshot_NotExist_Debug(DataSnapshot d_Snapshot, string m_Debug)
//    {
//        if(m_Debug) Debug.LogError(m_Debug + "\"" + d_Snapshot.Key.ToString() + "\"" + ":" + "\"" + cl_Data.GetData_NotFound() + "\"");
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
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public void SetFirebaseDatabase_ValueSingle_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        da_Reference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_ValueSingle_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if(m_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");

//                //    cl_Data.SetData(m_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");
//                //}

//                GetDataSnapshot_Value_Debug(
//                        d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");

//                cl_Data.SetData(m_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_ValueSingle_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get List of Key inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public void SetFirebaseDatabase_KeyList_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        da_Reference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyList_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (m_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "SetFirebaseDatabase_KeyList_ContinueWith: ");
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_ContinueWith: ");

//                //    cl_Data.SetIndex_Restart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cl_Data.SetIndex_Plus();
//                //        //Plus Index Data by 1

//                //        cl_Data.SetData(m_DataNameSave, cl_Data.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cl_Data.SetDataCount(m_DataNameSave, cl_Data.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_ContinueWith: ");

//                cl_Data.SetIndex_Restart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cl_Data.SetIndex_Plus();
//                    //Plus Index Data by 1

//                    cl_Data.SetData(m_DataNameSave, cl_Data.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cl_Data.SetDataCount(m_DataNameSave, cl_Data.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyList_ContinueWith: ");
//            }
//        });
//    }

//    /// <summary>
//    /// Get Key Exist inside Key Get from Firebase Database, then Save Bool Data (Get Data from "GetData()")
//    /// </summary>
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public void SetFirebaseDatabase_KeyExist_ContinueWith(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        da_Reference.Child(m_DatabaseAccess).GetValueAsync().ContinueWith(task => {
//            if (task.IsFaulted || task.IsCanceled)
//            {
//                if(m_Debug) Debug.LogError("SetFirebaseDatabase_KeyExist_ContinueWith: UnSuccessfully!");
//                return;
//            }
//            //Get Done
//            SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//            DataSnapshot d_Snapshot = task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_ContinueWith: ");

//                cl_Data.SetData(m_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_ContinueWith: ");

//                cl_Data.SetData(m_DataNameSave, false);
//                //Save Data (Value)
//            }
//        });
//    }

//    //Firebase Database IEnumerator

//    /// <summary>
//    /// Get Signle Data inside Key Get from Firebase Database, then Save Data (Get Data from "GetData()"). 
//    /// In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_ValueSingle_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (m_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");

//                //    cl_Data.SetData(m_DataNameSave, d_Snapshot.Value);
//                //    //Save Data (Value)
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_ValueSingle_IEnumerator: ");

//                cl_Data.SetData(m_DataNameSave, d_Snapshot.Value);
//                //Save Data (Value)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
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
//    /// In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_KeyList_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                string m_Json = d_Snapshot.GetRawJsonValue();
//                //Read & Try Chance Single Data to JSON Data

//                //if (m_Json == "")
//                ////If there is not JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, "SetFirebaseDatabase_KeyList_IEnumerator: ");
//                //}
//                //else
//                ////If there is JSON Data
//                //{
//                //    GetDataSnapshot_Value_Debug(
//                //        d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_IEnumerator: ");

//                //    cl_Data.SetIndex_Restart();
//                //    //Set Index Data to -1

//                //    foreach (DataSnapshot child in d_Snapshot.Children)
//                //    {
//                //        cl_Data.SetIndex_Plus();
//                //        //Plus Index Data by 1

//                //        cl_Data.SetData(m_DataNameSave, cl_Data.GetIndex(), child.Key);
//                //        //Save Data (Key)
//                //    }

//                //    cl_Data.SetDataCount(m_DataNameSave, cl_Data.GetIndex() + 1);
//                //    //Save Data (Count)
//                //}

//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, m_Json, "SetFirebaseDatabase_KeyList_IEnumerator: ");

//                cl_Data.SetIndex_Restart();
//                //Set Index Data to -1

//                foreach (DataSnapshot child in d_Snapshot.Children)
//                {
//                    cl_Data.SetIndex_Plus();
//                    //Plus Index Data by 1

//                    cl_Data.SetData(m_DataNameSave, cl_Data.GetIndex(), child.Key);
//                    //Save Data (Key)
//                }

//                cl_Data.SetDataCount(m_DataNameSave, cl_Data.GetIndex() + 1);
//                //Save Data (Count)

//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
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
//    /// In "MonoBehavior", use "StartCoroutine(SetFirebaseIEnumerator_Register(...));"
//    /// </summary>
//    /// <param name="m_DatabaseAccess">Parent/KeyGet" or "KeyGet"</param>
//    /// <param name="m_DataNameSave"></param>
//    public IEnumerator SetFirebaseDatabase_KeyExist_IEnumerator(string m_DatabaseAccess, string m_DataNameSave, string m_ProgessSaveName)
//    {
//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, false);

//        var v_Task = da_Reference.Child(m_DatabaseAccess).GetValueAsync();

//        yield return new WaitUntil(() => v_Task.IsCompleted || v_Task.IsFaulted);

//        SetFirebaseDatabase_GetDone(m_ProgessSaveName, true);

//        if (v_Task.IsCompleted)
//        {
//            DataSnapshot d_Snapshot = v_Task.Result;

//            if (GetDataSnapshotIsExist(d_Snapshot))
//            //If Exist Data to Read
//            {
//                GetDataSnapshot_Value_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_IEnumerator: ");

//                cl_Data.SetData(m_DataNameSave, true);
//                //Save Data (Value)
//            }
//            else
//            //If not Exist Data to Read
//            {
//                GetDataSnapshot_NotExist_Debug(
//                    d_Snapshot, "SetFirebaseDatabase_KeyExist_IEnumerator: ");

//                cl_Data.SetData(m_DataNameSave, false);
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