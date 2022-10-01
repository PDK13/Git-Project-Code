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

    public void SetPlaySound_3D(SoundCloneData cm_SoundCloneData, Vector2 v2_Pos, float m_Distance)
    {
        SetComponent_Add();

        com_AudioSource.clip = cm_SoundCloneData.GetClip();

        com_AudioSource.loop = cm_SoundCloneData.GetLoop();

        m_Volumn_Primary = cm_SoundCloneData.GetVolumn_Primary();

        com_AudioSource.spatialBlend = 1;

        transform.position = v2_Pos;

        com_AudioSource.maxDistance = m_Distance;

        com_AudioSource.Play();

        if (!cm_SoundCloneData.GetLoop())
        {
            StartCoroutine(SetSound_WhenStop());
        }
    }

    public void SetPlaySound_2D(SoundCloneData cm_SoundCloneData)
    {
        SetComponent_Add();

        com_AudioSource.clip = cm_SoundCloneData.GetClip();

        com_AudioSource.loop = cm_SoundCloneData.GetLoop();

        m_Volumn_Primary = cm_SoundCloneData.GetVolumn_Primary();

        com_AudioSource.spatialBlend = 0;

        com_AudioSource.Play();

        if (!cm_SoundCloneData.GetLoop())
        {
            StartCoroutine(SetSound_WhenStop());
        }
    }

    private IEnumerator SetSound_WhenStop()
    {
        yield return new WaitUntil(() => GetSoundIsStop() == true);

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

    public void SetSound_Mute(bool bIsMute)
    {
        com_AudioSource.mute = bIsMute;
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

    public bool GetSoundIsMute()
    {
        return com_AudioSource.mute;
    }

    public bool GetSoundIsStop()
    {
        return com_AudioSource.isPlaying == false;
    }

    public bool GetSoundIsPlay()
    {
        return com_AudioSource.isPlaying == true;
    }

    #endregion
}

[System.Serializable]
public class SoundCloneData
{
    private readonly AudioClip au_Clip;
    private readonly bool m_Loop;
    private readonly float m_Volumn_Primary;

    public SoundCloneData(AudioClip au_Clip, bool m_Loop, float m_Volumn_Primary)
    {
        this.au_Clip = au_Clip;
        this.m_Loop = m_Loop;
        this.m_Volumn_Primary = m_Volumn_Primary;
    }

    public AudioClip GetClip()
    {
        return au_Clip;
    }

    public bool GetLoop()
    {
        return m_Loop;
    }

    public float GetVolumn_Primary()
    {
        return m_Volumn_Primary;
    }
}