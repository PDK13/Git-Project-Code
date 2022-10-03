﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassScene
{
    public static void SetChanceScene(string m_SceneName, LoadSceneMode enumLoadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(m_SceneName, enumLoadSceneMode);
    }

    public static int GetSceneBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    #region Player Prefs

    #region Player Prefs Set

    public static void SetPlayerPrefsClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefsClear(string m_ValueName)
    {
        if (GetCheckPlayerPrefsExist(m_ValueName))
        {
            PlayerPrefs.DeleteKey(m_ValueName);
            PlayerPrefs.Save();
        }

        Debug.LogError("SetPlayerPrefsClear: Not Exist" + "\"" + m_ValueName + "\"");
    }

    public static void SetPlayerPrefs(string m_ValueName, string m_Value)
    {
        PlayerPrefs.SetString(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, int m_Value)
    {
        PlayerPrefs.SetInt(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, float m_Value)
    {
        PlayerPrefs.SetFloat(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    #endregion

    #region Player Prefs Get

    public static bool GetCheckPlayerPrefsExist(string m_ValueName)
    {
        return PlayerPrefs.HasKey(m_ValueName);
    }

    public static string GetPlayerPrefsString(string m_ValueName)
    {
        if (GetCheckPlayerPrefsExist(m_ValueName))
        {
            return PlayerPrefs.GetString(m_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_String: Not Exist" + "\"" + m_ValueName + "\"");
        return null;
    }

    public static int GetPlayerPrefsInt(string m_ValueName)
    {
        if (GetCheckPlayerPrefsExist(m_ValueName))
        {
            return PlayerPrefs.GetInt(m_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_Int: Not Exist" + "\"" + m_ValueName + "\"");
        return 0;
    }

    public static float GetPlayerPrefsFloat(string m_ValueName)
    {
        if (GetCheckPlayerPrefsExist(m_ValueName))
        {
            return PlayerPrefs.GetFloat(m_ValueName);
        }

        Debug.LogError("GetPlayerPrefs_Float: Not Exist" + "\"" + m_ValueName + "\"");
        return 0.0f;
    }

    #endregion

    #endregion
}
