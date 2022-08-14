using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on GameObject and Prepab
/// </summary>
public class Class_Object
{
    #region Prefab 

    #region Prefab Set to Scene 

    public static GameObject Set_Prepab_Create(GameObject g_Prepab, Transform t_Parent = null)
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

    #region Prefab(s) Get from Resource 

    //Get Prefab(s) from Resource in Project (Assetes/resources) or in Application
    public static List<GameObject> Get_Data_Application_Resource_List(string s_Path_Folder)
    {
        GameObject[] l_Prefab_Array = Resources.LoadAll<GameObject>(Class_String.Get_String_Replace_Resources(s_Path_Folder));
        List<GameObject> l_Prefab_List = new List<GameObject>();
        l_Prefab_List.AddRange(l_Prefab_Array);
        return l_Prefab_List;
    }

    //Get Prefab from Resource in Project (Assetes/resources) or in Application
    public static GameObject Get_Data_Application_Resource_Single(string s_Path_Folder, string s_File_Name)
    {
        return (GameObject)Resources.Load(Class_String.Get_String_Replace_Resources(s_Path_Folder) + @"\" + s_File_Name, typeof(GameObject));
    }

    #endregion

    #endregion

    #region Clone & GameObject 

    public static void Set_Destroy_GameObject(GameObject g_GameObject)
    {
        if (g_GameObject != null)
        {
            MonoBehaviour.Destroy(g_GameObject);
        }
    }

    #endregion
}