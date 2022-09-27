using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager cs_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject g_SoundClone;

    private GameObject g_SoundClone_Music;

    private bool b_Sound_isMute = false;

    [Header("Sound List")]

    [SerializeField] private List<GameObject> lg_SoundClone_Cur = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (cs_This == null)
        {
            cs_This = this;
        }
    }

    public void Set_Music(AudioClip au_Clip)
    {
        if (g_SoundClone_Music != null)
        {
            //If Music not emty, Stop Old and Start New Music!
            Destroy(g_SoundClone_Music);
        }

        //Start new Music!
        g_SoundClone_Music = Set_Sound_2D(au_Clip, true);
    }

    public GameObject Set_Sound_3D(AudioClip au_Clip, bool b_Loop, Vector2 v2_Pos, float f_Distance)
    {
        GameObject g_SoundClone = Class_Object.Set_Prepab_Create(this.g_SoundClone, this.transform);

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_3D(au_Clip, b_Loop, v2_Pos, f_Distance);

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(Get_Sound_isMute());

        if (b_Loop)
        {
            lg_SoundClone_Cur.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public GameObject Set_Sound_2D(AudioClip au_Clip, bool b_Loop)
    {
        GameObject g_SoundClone = Class_Object.Set_Prepab_Create(this.g_SoundClone, this.transform);

        g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(au_Clip, b_Loop);

        g_SoundClone.GetComponent<SoundClone>().Set_Sound_Mute(Get_Sound_isMute());

        if (b_Loop)
        {
            lg_SoundClone_Cur.Add(g_SoundClone);
        }

        return g_SoundClone;
    }

    public void Set_Sound_Mute(bool b_Sound_isMute)
    {
        for (int i = 0; i < lg_SoundClone_Cur.Count; i++)
        {
            lg_SoundClone_Cur[i].GetComponent<SoundClone>().Set_Sound_Mute(b_Sound_isMute);
        }
    }

    public bool Get_Sound_isMute()
    {
        return b_Sound_isMute;
    }

    public void Set_Sound_Stop()
    {
        int i_List_Count = lg_SoundClone_Cur.Count;

        for (int i = 0; i < i_List_Count; i++)
        {
            Destroy(lg_SoundClone_Cur[i]);
        }

        for (int i = 0; i < i_List_Count; i++)
        {
            lg_SoundClone_Cur.RemoveAt(0);
        }
    }

    public bool Get_Sound_isExist()
    {
        return lg_SoundClone_Cur.Count > 0;
    }
}