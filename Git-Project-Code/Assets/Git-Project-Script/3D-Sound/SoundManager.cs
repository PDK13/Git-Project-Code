using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager cs_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject g_SoundClone;

    private bool b_Sound_isMute = false;

    [Header("Sound List")]

    //First Index is Background Music
    private List<GameObject> lg_SoundClone = new List<GameObject>() { null };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

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

        if (g_SoundClone.GetComponent<SoundClone>() == null) g_SoundClone.AddComponent<SoundClone>();

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(new SoundCloneData(au_Clip, true, f_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(Get_Sound_isMute());

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

    public static GameObject Set_Sound_3D(AudioClip au_Clip, bool b_Loop, float f_Volumn_Primary, Vector2 v2_Pos, float f_Distance)
    {
        GameObject g_SoundClone = Class_Object.Set_GameObject_Create(cs_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null) g_SoundClone.AddComponent<SoundClone>();

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_3D(new SoundCloneData(au_Clip, b_Loop, f_Volumn_Primary), v2_Pos, f_Distance);

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(Get_Sound_isMute());

        if (b_Loop)
        {
            cs_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static GameObject Set_Sound_2D(AudioClip au_Clip, bool b_Loop, float f_Volumn_Primary)
    {
        GameObject g_SoundClone = Class_Object.Set_GameObject_Create(cs_This.g_SoundClone);

        if (g_SoundClone.GetComponent<SoundClone>() == null) g_SoundClone.AddComponent<SoundClone>();

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(new SoundCloneData(au_Clip, b_Loop, f_Volumn_Primary));

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(Get_Sound_isMute());

        if (b_Loop)
        {
            cs_This.lg_SoundClone.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public static bool Set_Sound_Stop(AudioClip au_Clip)
    {
        for (int i = 1; i < cs_This.lg_SoundClone.Count; i++)
        {
            if (cs_This.lg_SoundClone[i].GetComponent<SoundClone>().Get_Sound().name.Equals(au_Clip.name))
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

    public static void Set_Sound_Mute(bool b_Sound_isMute)
    {
        for (int i = 0; i < cs_This.lg_SoundClone.Count; i++)
        {
            cs_This.lg_SoundClone[i].GetComponent<SoundClone>().Set_Sound_Mute(b_Sound_isMute);
        }
    }

    public static bool Get_Sound_isMute()
    {
        return cs_This.b_Sound_isMute;
    }

    #endregion
}