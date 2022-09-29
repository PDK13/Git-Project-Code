using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on GameObject and Prepab
/// </summary>
public class Class_Object
{
    #region ================================================================== GameObject 

    #region Create

    public static GameObject Set_GameObject_Create(GameObject g_Prepab, Transform t_Parent = null)
    {
        if (t_Parent == null)
        {
            return MonoBehaviour.Instantiate(g_Prepab) as GameObject;
        }
        else
        {
            return MonoBehaviour.Instantiate(g_Prepab, t_Parent) as GameObject;
        }
    }

    #endregion

    #region Destroy

    public static void Set_GameObject_Destroy(GameObject g_GameObject)
    {
        if (g_GameObject != null)
        {
            MonoBehaviour.Destroy(g_GameObject);
        }
    }

    #endregion

    #region Get

    //NOTE:
    //Folder(s) "Resources" can be created everywhere from root "Assests/*", that can be access by Unity or Application

    //BEWARD:
    //All content(s) in folder(s) "Resources" will be builded to Application, even they mightn't be used in Build-Game Application

    public static List<GameObject> Get_Resources_Prefab(string s_Path_inResources)
    {
        GameObject[] l_Prefab_Array = Resources.LoadAll<GameObject>(Class_String.Get_String_Replace_Resources(s_Path_inResources));
        List<GameObject> l_Prefab_List = new List<GameObject>();
        l_Prefab_List.AddRange(l_Prefab_Array);
        return l_Prefab_List;
    }

    public static List<Sprite> Get_Resources_Sprite(string s_Path_inResources)
    {
        Sprite[] l_Sprite_Array = Resources.LoadAll<Sprite>(Class_String.Get_String_Replace_Resources(s_Path_inResources));
        List<Sprite> l_Sprite_List = new List<Sprite>();
        l_Sprite_List.AddRange(l_Sprite_Array);
        return l_Sprite_List;
    }

    public static List<TextAsset> Get_Resources_TextAsset(string s_Path_inResources)
    {
        TextAsset[] l_TextAsset_Array = Resources.LoadAll<TextAsset>(Class_String.Get_String_Replace_Resources(s_Path_inResources));
        List<TextAsset> l_TextAsset_List = new List<TextAsset>();
        l_TextAsset_List.AddRange(l_TextAsset_Array);
        return l_TextAsset_List;
    }

    #endregion

    #endregion

    #region ================================================================== Object

    public static bool Get_Object_isType<Type>(object m_Object)
    {
        return m_Object is Type;
    }

    #endregion
}