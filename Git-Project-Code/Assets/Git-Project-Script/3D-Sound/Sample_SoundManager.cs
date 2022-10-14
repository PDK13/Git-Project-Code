using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_Music;

    [SerializeField] private AudioClip m_Sound;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.SetSound2D(m_Sound, false, 1f);
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.SetBackgroundMusic(m_Sound, 1f);
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