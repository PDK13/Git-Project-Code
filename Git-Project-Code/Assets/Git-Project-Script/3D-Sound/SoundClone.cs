using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class SoundClone : MonoBehaviour
{
    private AudioSource com_AudioSource;

    private float m_Volumn_Primary = 1f;

    private void SetComponent_Add()
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }

        com_AudioSource = GetComponent<AudioSource>();
    }

    #region Play

    public void SetPlaySound_3D(SoundCloneData m_SoundCloneData, Vector2 v2_Pos, float m_Distance)
    {
        SetComponent_Add();

        com_AudioSource.clip = m_SoundCloneData.GetClip();

        com_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_Volumn_Primary = m_SoundCloneData.GetVolumn_Primary();

        com_AudioSource.spatialBlend = 1;

        transform.position = v2_Pos;

        com_AudioSource.maxDistance = m_Distance;

        com_AudioSource.Play();

        if (!m_SoundCloneData.GetCheckLoop())
        {
            StartCoroutine(SetSound_WhenStop());
        }
    }

    public void SetPlaySound_2D(SoundCloneData m_SoundCloneData)
    {
        SetComponent_Add();

        com_AudioSource.clip = m_SoundCloneData.GetClip();

        com_AudioSource.loop = m_SoundCloneData.GetCheckLoop();

        m_Volumn_Primary = m_SoundCloneData.GetVolumn_Primary();

        com_AudioSource.spatialBlend = 0;

        com_AudioSource.Play();

        if (!m_SoundCloneData.GetCheckLoop())
        {
            StartCoroutine(SetSound_WhenStop());
        }
    }

    private IEnumerator SetSound_WhenStop()
    {
        yield return new WaitUntil(() => GetCheckSoundStop() == true);

        Destroy(gameObject);
    }

    #endregion

    #region Set

    public void SetSound_Volumn(float m_Volumn)
    {
        if (m_Volumn_Primary * m_Volumn > 1f)
        {
            com_AudioSource.volume = 1f;
        }
        else
        if (m_Volumn_Primary * m_Volumn < 0f)
        {
            com_AudioSource.volume = 0f;
        }
        else
        {
            com_AudioSource.volume = m_Volumn_Primary * m_Volumn;
        }
    }

    public void SetSound_Mute(bool bMute)
    {
        com_AudioSource.mute = bMute;
    }

    public void SetSound_Stop()
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
    private readonly bool m_AllowLoop;
    private readonly float m_Volumn_Primary;

    public SoundCloneData(AudioClip au_Clip, bool m_AllowLoop, float m_Volumn_Primary)
    {
        this.au_Clip = au_Clip;
        this.m_AllowLoop = m_AllowLoop;
        this.m_Volumn_Primary = m_Volumn_Primary;
    }

    public AudioClip GetClip()
    {
        return au_Clip;
    }

    public bool GetCheckLoop()
    {
        return m_AllowLoop;
    }

    public float GetVolumn_Primary()
    {
        return m_Volumn_Primary;
    }
}