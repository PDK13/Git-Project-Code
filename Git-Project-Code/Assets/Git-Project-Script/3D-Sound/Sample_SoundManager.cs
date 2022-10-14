using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip au_Music;

    [SerializeField] private AudioClip au_Sound;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.SetSound2D(au_Sound, false, 1f);
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.SetBackgroundMusic(au_Sound, 1f);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.SetMute(true);
        }
        else
        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.SetSoundStopAll();
        }
    }
}