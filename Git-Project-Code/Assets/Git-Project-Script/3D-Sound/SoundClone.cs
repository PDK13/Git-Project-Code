using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class SoundClone : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private float m_VolumnPrimary = 1f;

    private void SetComponentAdd()
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }

        m_AudioSource = GetComponent<AudioSource>();
    }

    #region Play

    public void SetPlaySound3D(SoundCloneData m_SoundCloneData, Vector2 m_Pos, float m_Distance)
    {
        SetComponentAdd();

        m_AudioSource.clip = m_SoundCloneData.GetClip();

        m_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_VolumnPrimary = m_SoundCloneData.GetVolumnPrimary();

        m_AudioSource.spatialBlend = 1;

        transform.position = m_Pos;

        m_AudioSource.maxDistance = m_Distance;

        m_AudioSource.Play();

        if (!m_SoundCloneData.GetCheckLoop())
        {
            StartCoroutine(SetSoundWhenStop());
        }
    }

    public void SetPlaySound2D(SoundCloneData m_SoundCloneData)
    {
        SetComponentAdd();

        m_AudioSource.clip = m_SoundCloneData.GetClip();

        m_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_VolumnPrimary = m_SoundCloneData.GetVolumnPrimary();

        m_AudioSource.spatialBlend = 0;

        m_AudioSource.Play();

        if (!m_SoundCloneData.GetCheckLoop())
        {
            StartCoroutine(SetSoundWhenStop());
        }
    }

    private IEnumerator SetSoundWhenStop()
    {
        yield return new WaitUntil(() => GetCheckSoundStop() == true);

        Destroy(gameObject);
    }

    #endregion

    #region Set

    public void SetSoundVolumn(float m_Volumn)
    {
        if (m_VolumnPrimary * m_Volumn > 1f)
        {
            m_AudioSource.volume = 1f;
        }
        else
        if (m_VolumnPrimary * m_Volumn < 0f)
        {
            m_AudioSource.volume = 0f;
        }
        else
        {
            m_AudioSource.volume = m_VolumnPrimary * m_Volumn;
        }
    }

    public void SetSoundMute(bool b_CheckMute)
    {
        m_AudioSource.mute = b_CheckMute;
    }

    public void SetSoundStop()
    {
        m_AudioSource.Stop();
    }

    #endregion

    #region Get

    public AudioClip GetSound()
    {
        return m_AudioSource.clip;
    }

    public bool GetCheckSoundMute()
    {
        return m_AudioSource.mute;
    }

    public bool GetCheckSoundStop()
    {
        return m_AudioSource.isPlaying == false;
    }

    public bool GetCheckSoundPlay()
    {
        return m_AudioSource.isPlaying == true;
    }

    #endregion
}

[System.Serializable]
public class SoundCloneData
{
    private readonly AudioClip m_Clip;
    private readonly bool mAllowLoop;
    private readonly float m_VolumnPrimary;

    public SoundCloneData(AudioClip m_Clip, bool mAllowLoop, float m_VolumnPrimary)
    {
        this.m_Clip = m_Clip;
        this.mAllowLoop = mAllowLoop;
        this.m_VolumnPrimary = m_VolumnPrimary;
    }

    public AudioClip GetClip()
    {
        return m_Clip;
    }

    public bool GetCheckLoop()
    {
        return mAllowLoop;
    }

    public float GetVolumnPrimary()
    {
        return m_VolumnPrimary;
    }
}