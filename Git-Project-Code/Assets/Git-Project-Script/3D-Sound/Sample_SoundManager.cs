using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SoundManager : MonoBehaviour
{
    [Header("Sound Manager")]

    [SerializeField]
    private AudioClip au_Sound;

    [SerializeField]
    private AudioClip au_Sound_Loop;

    [SerializeField]
    private GameObject g_SoundClone;

    [Header("Sound List")]

    [SerializeField]
    private List<GameObject> lg_SoundClone_Cur;

    private void Start()
    {
        //Nên giữ cho GameObject này không bị Destroy nếu chuyển Scene, để sử dụng về sau
        DontDestroyOnLoad(this.gameObject);

        lg_SoundClone_Cur = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) //Non-Loop
        {
            GameObject g_SoundClone = Class_Object.Set_Prepab_Create(this.g_SoundClone, this.transform);

            //Không nên lưu các Sound không loop vào danh sách
            //lg_SoundClone_Cur.Add(g_SoundClone); 

            g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(au_Sound, false);
        }
        else
        if (Input.GetKeyDown(KeyCode.X)) //Loop
        {
            GameObject g_SoundClone = Class_Object.Set_Prepab_Create(this.g_SoundClone, this.transform);

            //Nên lưu các Sound có loop vào danh sách để điều chỉnh trong quá trình chơi
            lg_SoundClone_Cur.Add(g_SoundClone);

            g_SoundClone.GetComponent<SoundClone>().Set_PlaySound_2D(au_Sound_Loop, true);
        }
        else
        if (Input.GetKeyDown(KeyCode.M)) //Mute - UnMute
        {
            for (int i = 0; i < lg_SoundClone_Cur.Count; i++) 
            {
                lg_SoundClone_Cur[i].GetComponent<SoundClone>().Set_Sound_Mute(!lg_SoundClone_Cur[i].GetComponent<SoundClone>().Get_Sound_isMute());
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.C)) //Stop
        {
            int i_List_Count = lg_SoundClone_Cur.Count;

            for (int i = 0; i < i_List_Count; i++)
            {
                Destroy(lg_SoundClone_Cur[i]);
            }

            for (int i = 0; i < i_List_Count; i++)
            {
                lg_SoundClone_Cur.RemoveAt(0);
            }
        }
    }
}
