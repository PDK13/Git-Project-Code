using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager cs_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject g_SoundClone;

    private readonly bool m_SoundIsMute = false;

    [Header("Sound List")]

    //First Index is Background Music
    private List<GameObject> lg_SoundClone = new List<GameObject>() { null };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (cs_This == null)
        {
            cs_This = this;
        }
    }

    #region Music

    public static SoundClone Set_Background_Music(AudioClip au_Clip, float f_Volumn_Primary)
    {
        Set_Background_Music_Stop();

        GameObject g_SoundClone = Class_Object.Set_GameObject_Create(cs_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(new SoundCloneData(au_Clip, true, f_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(GetSoundIsMute());

        cs_This.lg_SoundClone[0] = g_SoundClone;

        return g_SoundClone.GetComponent<SoundClone>();
    }

    public static void Set_Background_Music_Stop()
    {
        if (cs_This.lg_SoundClone[0] == null)
        {
            return;
        }

        Destroy(cs_This.lg_SoundClone[0]);
    }

    #endregion

    #region Sound Primary

    public static GameObject Set_Sound_3D(AudioClip au_Clip, bool m_Loop, float f_Volumn_Primary, Vector2 v2_Pos, float f_Distance)
    {
        GameObject g_SoundClone = Class_Object.Set_GameObject_Create(cs_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_3D(new SoundCloneData(au_Clip, m_Loop, f_Volumn_Primary), v2_Pos, f_Distance);

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(GetSoundIsMute());

        if (m_Loop)
        {
            cs_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static GameObject Set_Sound_2D(AudioClip au_Clip, bool m_Loop, float f_Volumn_Primary)
    {
        GameObject g_SoundClone = Class_Object.Set_GameObject_Create(cs_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null)
        {
            g_SoundClone.AddComponent<SoundClone>();
        }

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(new SoundCloneData(au_Clip, m_Loop, f_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(GetSoundIsMute());

        if (m_Loop)
        {
            cs_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static bool Set_Sound_Stop(AudioClip au_Clip)
    {
        for (int i = 1; i < cs_This.lg_SoundClone.Count; i++)
        {
            if (cs_This.lg_SoundClone[i].GetComponent<SoundClone>().GetSound().name.Equals(au_Clip.name))
            {
                Destroy(cs_This.lg_SoundClone[i]);

                cs_This.lg_SoundClone.RemoveAt(i);

                return true;
            }
        }

        Debug.LogWarningFormat("{0}: Not found Sound {1} to Stop it!", cs_This.name, au_Clip.name);
        return false;
    }

    public static void Set_Sound_Stop_All()
    {
        for (int i = 1; i < cs_This.lg_SoundClone.Count; i++)
        {
            Destroy(cs_This.lg_SoundClone[i]);
        }

        cs_This.lg_SoundClone = new List<GameObject>();
    }

    #endregion

    #region Sound Primary Mute

    public static void Set_Sound_Mute(bool m_SoundIsMute)
    {
        for (int i = 0; i < cs_This.lg_SoundClone.Count; i++)
        {
            cs_This.lg_SoundClone[i].GetComponent<SoundClone>().Set_Sound_Mute(m_SoundIsMute);
        }
    }

    public static bool GetSoundIsMute()
    {
        return cs_This.m_SoundIsMute;
    }

    #endregion
}