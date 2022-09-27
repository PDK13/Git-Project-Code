using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip au_Music;

    [SerializeField] private AudioClip au_Sound;

    private SoundManager cs_SoundManager;

    private void Start()
    {
        cs_SoundManager = GetComponent<SoundManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cs_SoundManager.Set_Sound_2D(au_Sound, false);
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            cs_SoundManager.Set_Sound_2D(au_Sound, true);
            
            cs_SoundManager.Set_Music(au_Sound);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            cs_SoundManager.Set_Sound_Mute(true);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            cs_SoundManager.Set_Sound_Stop();
        }
    }
}
