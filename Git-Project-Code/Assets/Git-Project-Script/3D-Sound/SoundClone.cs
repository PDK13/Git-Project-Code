using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class SoundClone : MonoBehaviour
{
    private AudioSource com_AudioSource;

    private float f_Volumn_Primary = 1f;

    private void Set_Component_Add()
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }

        com_AudioSource = GetComponent<AudioSource>();
    }

    #region Play

    public void Set_PlaySound_3D(SoundCloneData cs_SoundCloneData, Vector2 v2_Pos, float f_Distance)
    {
        Set_Component_Add();

        com_AudioSource.clip = cs_SoundCloneData.Get_Clip();

        com_AudioSource.loop = cs_SoundCloneData.Get_Loop();

        f_Volumn_Primary = cs_SoundCloneData.Get_Volumn_Primary();

        com_AudioSource.spatialBlend = 1;

        transform.position = v2_Pos;

        com_AudioSource.maxDistance = f_Distance;

        com_AudioSource.Play();

        if (!cs_SoundCloneData.Get_Loop())
        {
            StartCoroutine(Set_Sound_WhenStop());
        }
    }

    public void Set_PlaySound_2D(SoundCloneData cs_SoundCloneData)
    {
        Set_Component_Add();

        com_AudioSource.clip = cs_SoundCloneData.Get_Clip();

        com_AudioSource.loop = cs_SoundCloneData.Get_Loop();

        f_Volumn_Primary = cs_SoundCloneData.Get_Volumn_Primary();

        com_AudioSource.spatialBlend = 0;

        com_AudioSource.Play();

        if (!cs_SoundCloneData.Get_Loop())
        {
            StartCoroutine(Set_Sound_WhenStop());
        }
    }

    private IEnumerator Set_Sound_WhenStop()
    {
        yield return new WaitUntil(() => Get_Sound_isStop() == true);

        Destroy(gameObject);
    }

    #endregion

    #region Set

    public void Set_Sound_Volumn(float f_Volumn)
    {
        if (f_Volumn_Primary * f_Volumn > 1f)
        {
            com_AudioSource.volume = 1f;
        }
        else
        if (f_Volumn_Primary * f_Volumn < 0f)
        {
            com_AudioSource.volume = 0f;
        }
        else
        {
            com_AudioSource.volume = f_Volumn_Primary * f_Volumn;
        }
    }

    public void Set_Sound_Mute(bool b_isMute)
    {
        com_AudioSource.mute = b_isMute;
    }

    public void Set_Sound_Stop()
    {
        com_AudioSource.Stop();
    }

    #endregion

    #region Get

    public AudioClip Get_Sound()
    {
        return com_AudioSource.clip;
    }

    public bool Get_Sound_isMute()
    {
        return com_AudioSource.mute;
    }

    public bool Get_Sound_isStop()
    {
        return com_AudioSource.isPlaying == false;
    }

    public bool Get_Sound_isPlay()
    {
        return com_AudioSource.isPlaying == true;
    }

    #endregion
}

[System.Serializable]
public class SoundCloneData
{
    private readonly AudioClip au_Clip;
    private readonly bool b_Loop;
    private readonly float f_Volumn_Primary;

    public SoundCloneData(AudioClip au_Clip, bool b_Loop, float f_Volumn_Primary)
    {
        this.au_Clip = au_Clip;
        this.b_Loop = b_Loop;
        this.f_Volumn_Primary = f_Volumn_Primary;
    }

    public AudioClip Get_Clip()
    {
        return au_Clip;
    }

    public bool Get_Loop()
    {
        return b_Loop;
    }

    public float Get_Volumn_Primary()
    {
        return f_Volumn_Primary;
    }
}