using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip au_Music;

    [SerializeField] private AudioClip au_Sound;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.SetSound_2D(au_Sound, false, 1f);
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.SetBackground_Music(au_Sound, 1f);
        }
        else
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.SetSound_Mute(true);
        }
        else
        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.SetSound_Stop_All();
        }
    }
}