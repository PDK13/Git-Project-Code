using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Can call this Script instancely thought this Name of Script!
    private static SoundManager m_This;

    [Header("Sound Manager")]

    [SerializeField] private GameObject m_SoundClone;

    private readonly bool m_AllowSoundMute = false;

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

    public static SoundClone SetBackgroundMusic(AudioClip au_Clip, float m_Volumn_Primary)
    {
        SetBackgroundMusic_Stop();

        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound_2D(new SoundCloneData(au_Clip, true, m_Volumn_Primary));

        m_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetCheckSoundMute());

        m_This.lm_SoundClone[0] = m_SoundClone;

        return m_SoundClone.GetComponent<SoundClone>();
    }

    public static void SetBackgroundMusic_Stop()
    {
        if (m_This.lm_SoundClone[0] == null)
        {
            return;
        }

        Destroy(m_This.lm_SoundClone[0]);
    }

    #endregion

    #region Sound Primary

    public static GameObject SetSound_3D(AudioClip au_Clip, bool m_AllowLoop, float m_Volumn_Primary, Vector2 v2_Pos, float m_Distance)
    {
        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound_3D(new SoundCloneData(au_Clip, m_AllowLoop, m_Volumn_Primary), v2_Pos, m_Distance);

        m_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetCheckSoundMute());

        if (m_AllowLoop)
        {
            m_This.lm_SoundClone.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static GameObject SetSound_2D(AudioClip au_Clip, bool m_AllowLoop, float m_Volumn_Primary)
    {
        GameObject m_SoundClone = ClassObject.SetGameObjectCreate(m_This.m_SoundClone);

        if (m_SoundClone.GetComponent<SoundClone>() == null)
        {
            m_SoundClone.AddComponent<SoundClone>();
        }

        m_SoundClone.GetComponent<SoundClone>().SetPlaySound_2D(new SoundCloneData(au_Clip, m_AllowLoop, m_Volumn_Primary));

        m_SoundClone.GetComponent<SoundClone>().SetSound_Mute(GetCheckSoundMute());

        if (m_AllowLoop)
        {
            m_This.lm_SoundClone.Add(m_SoundClone);
        }

        return m_SoundClone;
    }

    public static bool SetSound_Stop(AudioClip au_Clip)
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

    public static void SetSound_Stop_All()
    {
        for (int i = 1; i < m_This.lm_SoundClone.Count; i++)
        {
            Destroy(m_This.lm_SoundClone[i]);
        }

        m_This.lm_SoundClone = new List<GameObject>();
    }

    #endregion

    #region Sound Primary mute

    public static void SetSound_Mute(bool m_AllowSoundMute)
    {
        for (int i = 0; i < m_This.lm_SoundClone.Count; i++)
        {
            m_This.lm_SoundClone[i].GetComponent<SoundClone>().SetSound_Mute(m_AllowSoundMute);
        }
    }

    public static bool GetCheckSoundMute()
    {
        return m_This.m_AllowSoundMute;
    }

    #endregion
}