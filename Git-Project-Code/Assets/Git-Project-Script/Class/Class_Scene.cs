using UnityEngine;
using UnityEngine.SceneManagement;

public class Class_Scene
{
    public static void Set_ChanceScene(string s_SceneName, LoadSceneMode enum_LoadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(s_SceneName, enum_LoadSceneMode);
    }

    public static int GetSceneBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    #region Player Pref

    #region Player Pref Set

    public static void Set_PlayerPrefs_Clear_All()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static void Set_PlayerPrefs_Clear(string s_ValueName)
    {
        if (GetPlayerPrefsIsExist(s_ValueName))
        {
            PlayerPrefs.DeleteKey(s_ValueName);
            PlayerPrefs.Save();
        }

        Debug.LogError("Set_PlayerPrefs_Clear: Not Exist" + "\"" + s_ValueName + "\"");
    }

    public static void Set_PlayerPrefs(string s_ValueName, string s_Value)
    {
        PlayerPrefs.SetString(s_ValueName, s_Value);
        PlayerPrefs.Save();
    }

    public static void Set_PlayerPrefs(string s_ValueName, int i_Value)
    {
        PlayerPrefs.SetInt(s_ValueName, i_Value);
        PlayerPrefs.Save();
    }

    public static void Set_PlayerPrefs(string s_ValueName, float f_Value)
    {
        PlayerPrefs.SetFloat(s_ValueName, f_Value);
        PlayerPrefs.Save();
    }

    #endregion

    #region Player Pref Get

    public static bool GetPlayerPrefsIsExist(string s_ValueName)
    {
        return PlayerPrefs.HasKey(s_ValueName);
    }

    public static string GetPlayerPrefs_String(string s_ValueName)
    {
        if (GetPlayerPrefsIsExist(s_ValueName))
        {
            return PlayerPrefs.GetString(s_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_String: Not Exist" + "\"" + s_ValueName + "\"");
        return null;
    }

    public static int GetPlayerPrefs_Int(string s_ValueName)
    {
        if (GetPlayerPrefsIsExist(s_ValueName))
        {
            return PlayerPrefs.GetInt(s_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_Int: Not Exist" + "\"" + s_ValueName + "\"");
        return 0;
    }

    public static float GetPlayerPrefs_Float(string s_ValueName)
    {
        if (GetPlayerPrefsIsExist(s_ValueName))
        {
            return PlayerPrefs.GetFloat(s_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_Float: Not Exist" + "\"" + s_ValueName + "\"");
        return 0.0f;
    }

    #endregion

    #endregion
}
