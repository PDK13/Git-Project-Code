using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on GameObject and Prepab
/// </summary>
public class Class_Object
{
    #region Prefab 

    #region Prefab Set to Scene 

    /// <summary>
    /// Create Clone from Prepab inside Parent GameObject
    /// </summary>
    /// <param name="g_Prepab"></param>
    /// <param name="t_Parent"></param>
    /// <returns></returns>
    public static GameObject Set_Prepab_Create(GameObject g_Prepab, Transform t_Parent)
    {
        return MonoBehaviour.Instantiate(g_Prepab, t_Parent) as GameObject;
    }

    /// <summary>
    /// Create Clone from Prepab inside Scene
    /// </summary>
    /// <param name="g_Prepab"></param>
    /// <param name="v_Pos"></param>
    /// <param name="q_Rotation"></param>
    /// <returns></returns>
    public static GameObject Set_Prepab_Create(GameObject g_Prepab, Vector3 v_Pos, Quaternion q_Rotation)
    {
        return MonoBehaviour.Instantiate(g_Prepab, v_Pos, q_Rotation) as GameObject;
    }

    #endregion

    #region Prefab(s) Get from Resource 

    /// <summary>
    /// Get Prefab(s) from Resource in Project (Assetes/resources) or in Application
    /// </summary>
    /// <param name="s_Path_Folder"></param>
    /// <returns></returns>
    public static List<GameObject> Get_Data_Application_Resource_List(string s_Path_Folder)
    {
        GameObject[] l_Prefab_Array = Resources.LoadAll<GameObject>(
            Class_String.Get_String_Replace_Resources(s_Path_Folder));

        List<GameObject> l_Prefab_List = new List<GameObject>();

        l_Prefab_List.AddRange(l_Prefab_Array);

        return l_Prefab_List;
    }

    /// <summary>
    /// Get Prefab from Resource in Project (Assetes/resources) or in Application
    /// </summary>
    /// <param name="s_Path"></param>
    /// <returns></returns>
    public static GameObject Get_Data_Application_Resource_Single(string s_Path_Folder, string s_File_Name)
    {
        return (GameObject)Resources.Load(
            Class_String.Get_String_Replace_Resources(s_Path_Folder) + @"\" + s_File_Name, typeof(GameObject));
    }

    #endregion

    #endregion

    #region Clone & GameObject 

    /// <summary>
    /// Destroy a Game Object
    /// </summary>
    /// <param name="g_GameObject"></param>
    public static void Set_Destroy_GameObject(GameObject g_GameObject)
    {
        if (g_GameObject != null)
        {
            MonoBehaviour.Destroy(g_GameObject);
        }
    }

    #endregion
}