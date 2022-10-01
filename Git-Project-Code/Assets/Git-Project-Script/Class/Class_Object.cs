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
            return MonoBehaviour.Instantiate(g_Prepab);
        }
        else
        {
            return MonoBehaviour.Instantiate(g_Prepab, t_Parent);
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

    public static List<GameObject> GetResources_Prefab(string s_Path_inResources)
    {
        GameObject[] l_Prefam_Array = Resources.LoadAll<GameObject>(ClassString.GetStringReplace_Resources(s_Path_inResources));
        List<GameObject> l_Prefam_List = new List<GameObject>();
        l_Prefam_List.AddRange(l_Prefam_Array);
        return l_Prefam_List;
    }

    public static List<Sprite> GetResources_Sprite(string s_Path_inResources)
    {
        Sprite[] l_Sprite_Array = Resources.LoadAll<Sprite>(ClassString.GetStringReplace_Resources(s_Path_inResources));
        List<Sprite> l_Sprite_List = new List<Sprite>();
        l_Sprite_List.AddRange(l_Sprite_Array);
        return l_Sprite_List;
    }

    public static List<TextAsset> GetResources_TextAsset(string s_Path_inResources)
    {
        TextAsset[] l_TextAsset_Array = Resources.LoadAll<TextAsset>(ClassString.GetStringReplace_Resources(s_Path_inResources));
        List<TextAsset> l_TextAsset_List = new List<TextAsset>();
        l_TextAsset_List.AddRange(l_TextAsset_Array);
        return l_TextAsset_List;
    }

    #endregion

    #endregion

    #region ================================================================== Object

    public static bool GetObjectIsType<Type>(object m_Object)
    {
        return m_Object is Type;
    }

    #endregion
}