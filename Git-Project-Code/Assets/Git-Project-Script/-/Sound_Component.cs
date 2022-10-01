using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Sound_Component : MonoBehaviour
//Nếu muốn GameObject phát ra âm thanh, gán hàm này và tự lập trình sử dụng hàm dưới trong các Script khác
{
    public void Set_3DSound()
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
        {
            return;
        }

        GetComponent<AudioSource>().spatialBlend = 1f;
    }

    public void Set_2DSound()
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
        {
            return;
        }

        GetComponent<AudioSource>().spatialBlend = 0f;
    }

    public void Set_PlaySound(AudioClip a_Sound, float m_Volumn, bool m_Loop) //Gọi hàm khi cần phát âm thanh
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
        {
            return;
        }

        if (GetComponent<AudioSource>().clip != a_Sound)
        {
            GetComponent<AudioSource>().clip = a_Sound;
            GetComponent<AudioSource>().volume = m_Volumn;
            GetComponent<AudioSource>().loop = m_Loop;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().volume = m_Volumn;
            GetComponent<AudioSource>().loop = m_Loop;
        }
        //GetComponent<AudioSource>().PlayOneShot(a_Sound, m_Volumn);
    }

    public void Set_PlaySound(AudioClip a_Sound, float m_Volumn, bool m_Loop, bool m_Continue) //Gọi hàm khi cần phát âm thanh
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
        {
            return;
        }

        GetComponent<AudioSource>().clip = a_Sound;
        GetComponent<AudioSource>().volume = m_Volumn;
        GetComponent<AudioSource>().loop = m_Loop;
        if (!m_Continue)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void Set_MuteSound(bool m_MuteSound)
    {
        if (GetComponent<AudioSource>() == null) //Thêm Component Audio Source
        {
            return;
        }

        GetComponent<AudioSource>().mute = m_MuteSound;
    }
}
