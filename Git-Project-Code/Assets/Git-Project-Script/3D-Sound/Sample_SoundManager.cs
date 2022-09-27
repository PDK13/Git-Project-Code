using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip au_Music;

    [SerializeField] private AudioClip au_Sound;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.Set_Sound_2D(au_Sound, false);
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.Set_Sound_2D(au_Sound, true);

            SoundManager.Set_Music(au_Sound);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.Set_Sound_Mute(true);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.Set_Sound_Stop();
        }
    }
}