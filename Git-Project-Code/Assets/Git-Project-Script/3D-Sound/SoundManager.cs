using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager m_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject m_SoundClone;

    private bool m_CheckMute = false;

    [Header("Sound List")]

    //First Index is Background music
    private List<GameObject> m_SoundCloneList = new List<GameObject>() { null };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (m_This == null)
        {
            m_This = this;
        }
    }

    #region m_usic

    public static SoundClone SetBackgroundMusic(AudioClip m_Clip, float m_VolumnPrimary)
    {
        SetBackgroundMusicStop();

        GameObject m_SoundClone = GitGameObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound2D(new SoundCloneData(m_Clip, true, m_VolumnPrimary));

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(m_This.m_CheckMute);

        m_This.m_SoundCloneList[0] = m_SoundClone;

        return m_SoundClone.GetComponent<SoundClone>();
    }

    public static void SetBackgroundMusicStop()
    {
        if (m_This.m_SoundCloneList[0] == null)
        {
            return;
        }

        Destroy(m_This.m_SoundCloneList[0]);
    }

    public static void SetBackgroundMusicMute(bool m_CheckMute)
    {
        if (m_This.m_SoundCloneList[0] == null)
        {
            return;
        }

        m_This.m_SoundCloneList[0].GetComponent<SoundClone>().SetSoundMute(m_CheckMute);
    }

    #endregion

    #region Sound Primary

    public static GameObject SetSound3D(AudioClip m_Clip, bool m_CheckLoop, float m_VolumnPrimary, Vector2 m_Pos, float m_Distance)
    {
        GameObject m_SoundClone = GitGameObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound3D(new SoundCloneData(m_Clip, m_CheckLoop, m_VolumnPrimary), m_Pos, m_Distance);

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(m_This.m_CheckMute);

        if (m_CheckLoop)
        {
            m_This.m_SoundCloneList.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static GameObject SetSound2D(AudioClip m_Clip, bool m_CheckLoop, float m_VolumnPrimary)
    {
        GameObject m_SoundClone = GitGameObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound2D(new SoundCloneData(m_Clip, m_CheckLoop, m_VolumnPrimary));

        m_SoundClone.GetComponent<SoundClone>().SetSoundMute(m_This.m_CheckMute);

        if (m_CheckLoop)
        {
            m_This.m_SoundCloneList.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static bool SetSoundStop(AudioClip m_Clip)
    {
        for (int i = 1; i < m_This.m_SoundCloneList.Count; i++)
        {
            if (m_This.m_SoundCloneList[i].GetComponent<SoundClone>().GetSound().name.Equals(m_Clip.name))
            {
                Destroy(m_This.m_SoundCloneList[i]);

                m_This.m_SoundCloneList.RemoveAt(i);

                return true;
            }
        }

        Debug.LogWarningFormat("{0}: Not found Sound {1} to Stop it!", m_This.name, m_Clip.name);
        return false;
    }

    public static void SetSoundStopAll()
    {
        for (int i = 1; i < m_This.m_SoundCloneList.Count; i++)
        {
            Destroy(m_This.m_SoundCloneList[i]);
        }

        m_This.m_SoundCloneList = new List<GameObject>();
    }

    public static void SetSoundMute(bool m_CheckMute)
    {
        for (int i = 1; i < m_This.m_SoundCloneList.Count; i++)
        {
            m_This.m_SoundCloneList[i].GetComponent<SoundClone>().SetSoundMute(m_CheckMute);
        }
    }

    #endregion

    #region Mute

    public static void SetMute(bool m_CheckMute)
    {
        m_This.m_CheckMute = m_CheckMute;

        SetBackgroundMusicMute(m_CheckMute);

        SetSoundMute(m_CheckMute);
    }

    public static bool GetMute()
    {
        return m_This.m_CheckMute;
    }

    #endregion
}