using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager m_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject m_SoundClone;

    private readonly bool mAllowSoundMute = false;

    [Header("Sound List")]

    //First Index is Background m_usic
    private List<GameObject> lm_SoundClone = new List<GameObject>() { null };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (m_This == null)
        {
            m_This = this;
        }
    }

    #region m_usic

    public static SoundClone SetBackgroundMusic(AudioClip au_Clip, float m_VolumnPrimary)
    {
        SetBackgroundMusicStop();

        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound2D(new SoundCloneData(au_Clip, true, m_VolumnPrimary));

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(GetCheckSoundMute());

        m_This.lm_SoundClone[0] = m_SoundClone;

        return m_SoundClone.GetComponent<SoundClone>();
    }

    public static void SetBackgroundMusicStop()
    {
        if (m_This.lm_SoundClone[0] == null)
        {
            return;
        }

        Destroy(m_This.lm_SoundClone[0]);
    }

    #endregion

    #region Sound Primary

    public static GameObject SetSound3D(AudioClip au_Clip, bool mAllowLoop, float m_VolumnPrimary, Vector2 v2_Pos, float m_Distance)
    {
        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound3D(new SoundCloneData(au_Clip, mAllowLoop, m_VolumnPrimary), v2_Pos, m_Distance);

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(GetCheckSoundMute());

        if (mAllowLoop)
        {
            m_This.lm_SoundClone.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static GameObject SetSound2D(AudioClip au_Clip, bool mAllowLoop, float m_VolumnPrimary)
    {
        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound2D(new SoundCloneData(au_Clip, mAllowLoop, m_VolumnPrimary));

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(GetCheckSoundMute());

        if (mAllowLoop)
        {
            m_This.lm_SoundClone.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static bool SetSoundStop(AudioClip au_Clip)
    {
        for (int i = 1; i < m_This.lm_SoundClone.Count; i++)
        {
            if (m_This.lm_SoundClone[i].GetComponent<SoundClone>().GetSound().name.Equals(au_Clip.name))
            {
                Destroy(m_This.lm_SoundClone[i]);

                m_This.lm_SoundClone.RemoveAt(i);

                return true;
            }
        }

        Debug.LogWarningFormat("{0}: Not found Sound {1} to Stop it!", m_This.name, au_Clip.name);
        return false;
    }

    public static void SetSoundStopAll()
    {
        for (int i = 1; i < m_This.lm_SoundClone.Count; i++)
        {
            Destroy(m_This.lm_SoundClone[i]);
        }

        m_This.lm_SoundClone = new List<GameObject>();
    }

    #endregion

    #region Sound Primary Mute

    public static void SetSoundMute(bool mAllowSoundMute)
    {
        for (int i = 0; i < m_This.lm_SoundClone.Count; i++)
        {
            m_This.lm_SoundClone[i].GetComponent<SoundClone>().SetSoundMute(mAllowSoundMute);
        }
    }

    public static bool GetCheckSoundMute()
    {
        return m_This.mAllowSoundMute;
    }

    #endregion
}