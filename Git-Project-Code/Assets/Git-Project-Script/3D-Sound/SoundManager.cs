using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager cm_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject g_SoundClone;

    private readonly bool m_SoundIsMute = false;

    [Header("Sound List")]

    //First Index is Background Music
    private List<GameObject> lg_SoundClone = new List<GameObject>() { null };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (cm_This == null)
        {
            cm_This = this;
        }
    }

    #region Music

    public static SoundClone SetBackground_Music(AudioClip au_Clip, float m_Volumn_Primary)
    {
        SetBackground_Music_Stop();

        GameObject g_SoundClone = Clasm_Object.SetGameObject_Create(cm_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().SetPlaySound_2D(new SoundCloneData(au_Clip, true, m_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetSoundIsMute());

        cm_This.lg_SoundClone[0] = g_SoundClone;

        return g_SoundClone.GetComponent<SoundClone>();
    }

    public static void SetBackground_Music_Stop()
    {
        if (cm_This.lg_SoundClone[0] == null)
        {
            return;
        }

        Destroy(cm_This.lg_SoundClone[0]);
    }

    #endregion

    #region Sound Primary

    public static GameObject SetSound_3D(AudioClip au_Clip, bool m_Loop, float m_Volumn_Primary, Vector2 v2_Pos, float m_Distance)
    {
        GameObject g_SoundClone = Clasm_Object.SetGameObject_Create(cm_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().SetPlaySound_3D(new SoundCloneData(au_Clip, m_Loop, m_Volumn_Primary), v2_Pos, m_Distance);

        g_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetSoundIsMute());

        if (m_Loop)
        {
            cm_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static GameObject SetSound_2D(AudioClip au_Clip, bool m_Loop, float m_Volumn_Primary)
    {
        GameObject g_SoundClone = Clasm_Object.SetGameObject_Create(cm_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().SetPlaySound_2D(new SoundCloneData(au_Clip, m_Loop, m_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetSoundIsMute());

        if (m_Loop)
        {
            cm_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static bool SetSound_Stop(AudioClip au_Clip)
    {
        for (int i = 1; i < cm_This.lg_SoundClone.Count; i++)
        {
            if (cm_This.lg_SoundClone[i].GetComponent<SoundClone>().GetSound().name.Equals(au_Clip.name))
            {
                Destroy(cm_This.lg_SoundClone[i]);

                cm_This.lg_SoundClone.RemoveAt(i);

                return true;
            }
        }

        Debug.LogWarningFormat("{0}: Not found Sound {1} to Stop it!", cm_This.name, au_Clip.name);
        return false;
    }

    public static void SetSound_Stop_All()
    {
        for (int i = 1; i < cm_This.lg_SoundClone.Count; i++)
        {
            Destroy(cm_This.lg_SoundClone[i]);
        }

        cm_This.lg_SoundClone = new List<GameObject>();
    }

    #endregion

    #region Sound Primary Mute

    public static void SetSound_Mute(bool m_SoundIsMute)
    {
        for (int i = 0; i < cm_This.lg_SoundClone.Count; i++)
        {
            cm_This.lg_SoundClone[i].GetComponent<SoundClone>().SetSound_Mute(m_SoundIsMute);
        }
    }

    public static bool GetSoundIsMute()
    {
        return cm_This.m_SoundIsMute;
    }

    #endregion
}