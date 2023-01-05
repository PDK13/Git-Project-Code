#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class UnityAndroidKeystore
{
    //Auto add Password for this Project!!
    static UnityAndroidKeystore()
    {
        PlayerSettings.keystorePass = "1234567890";
        PlayerSettings.keyaliasPass = "1234567890";
        PlayerSettings.Android.keystoreName = Application.dataPath + "/../key-store.keystore";
    }
}

#endif