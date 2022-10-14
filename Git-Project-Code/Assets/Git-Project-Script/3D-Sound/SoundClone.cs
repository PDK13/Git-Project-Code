using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class SoundClone : MonoBehaviour
{
    private AudioSource com_AudioSource;

    private float m_VolumnPrimary = 1f;

    private void SetComponentAdd()
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }

        com_AudioSource = GetComponent<AudioSource>();
    }

    #region Play

    public void SetPlaySound3D(SoundCloneData m_SoundCloneData, Vector2 v2_Pos, float m_Distance)
    {
        SetComponentAdd();

        com_AudioSource.clip = m_SoundCloneData.GetClip();

        com_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_VolumnPrimary = m_SoundCloneData.GetVolumnPrimary();

        com_AudioSource.spatialBlend = 1;

        transform.position = v2_Pos;

        com_AudioSource.maxDistance = m_Distance;

        com_AudioSource.Play();

        if (!m_SoundCloneData.GetCheckLoop())
        {
            StartCoroutine(SetSoundWhenStop());
        }
    }

    public void SetPlaySound2D(SoundCloneData m_SoundCloneData)
    {
        SetComponentAdd();

        com_AudioSource.clip = m_SoundCloneData.GetClip();

        com_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_VolumnPrimary = m_SoundCloneData.GetVolumnPrimary();

        com_AudioSource.spatialBlend = 0;

        com_AudioSource.Play();

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
            com_AudioSource.volume = 1f;
        }
        else
        if (m_VolumnPrimary * m_Volumn < 0f)
        {
            com_AudioSource.volume = 0f;
        }
        else
        {
            com_AudioSource.volume = m_VolumnPrimary * m_Volumn;
        }
    }

    public void SetSoundMute(bool b_CheckMute)
    {
        com_AudioSource.mute = b_CheckMute;
    }

    public void SetSoundStop()
    {
        com_AudioSource.Stop();
    }

    #endregion

    #region Get

    public AudioClip GetSound()
    {
        return com_AudioSource.clip;
    }

    public bool GetCheckSoundMute()
    {
        return com_AudioSource.mute;
    }

    public bool GetCheckSoundStop()
    {
        return com_AudioSource.isPlaying == false;
    }

    public bool GetCheckSoundPlay()
    {
        return com_AudioSource.isPlaying == true;
    }

    #endregion
}

[System.Serializable]
public class SoundCloneData
{
    private readonly AudioClip au_Clip;
    private readonly bool mAllowLoop;
    private readonly float m_VolumnPrimary;

    public SoundCloneData(AudioClip au_Clip, bool mAllowLoop, float m_VolumnPrimary)
    {
        this.au_Clip = au_Clip;
        this.mAllowLoop = mAllowLoop;
        this.m_VolumnPrimary = m_VolumnPrimary;
    }

    public AudioClip GetClip()
    {
        return au_Clip;
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