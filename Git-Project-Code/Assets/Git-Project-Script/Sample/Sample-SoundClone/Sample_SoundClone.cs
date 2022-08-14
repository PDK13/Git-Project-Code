using System.Collections;
using UnityEngine;

public class Sample_SoundClone : MonoBehaviour
{
    private AudioSource com_AudioSource;

    private void Start()
    {
        Set_Component();
    }

    private void Set_Component()
    {
        if (GetComponent<AudioSource>() == null)
        {
            this.gameObject.AddComponent<AudioSource>();
        }

        this.com_AudioSource = GetComponent<AudioSource>();
    }

    #region Play

    public void Set_PlaySound_3D(AudioClip au_Clip, bool b_Loop, Vector2 v2_Pos, float f_Distance)
    {
        Set_Component();

        transform.position = v2_Pos;

        com_AudioSource.spatialBlend = 1;

        com_AudioSource.clip = au_Clip;

        com_AudioSource.maxDistance = f_Distance;

        com_AudioSource.loop = b_Loop;

        com_AudioSource.Play();

        if (!b_Loop)
        {
            StartCoroutine(WaitComplete());
        }  
    }

    public void Set_PlaySound_2D(AudioClip au_Clip, bool b_Loop)
    {
        Set_Component();

        com_AudioSource.spatialBlend = 0;

        com_AudioSource.clip = au_Clip;

        com_AudioSource.loop = b_Loop;

        com_AudioSource.Play();

        if (!b_Loop)
        {
            StartCoroutine(WaitComplete());
        }  
    }

    IEnumerator WaitComplete()
    {
        yield return new WaitUntil(() => Get_Sound_isStop() == true);

        Destroy(this.gameObject);
    }

    #endregion

    #region Set

    public void Set_Sound_Mute(bool b_Mute)
    {
        com_AudioSource.mute = b_Mute;
    }

    public void Set_Sound_Stop()
    {
        com_AudioSource.Stop();
    }

    #endregion

    #region Get

    public bool Get_Sound_Mute()
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
