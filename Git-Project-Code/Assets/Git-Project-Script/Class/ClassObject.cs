using System.Collections.Generic;
using UnityEngine;

public class ClassObject
{
    #region ================================================================== GameObject 

    #region Create

    public static GameObject SetGameObjectCreate(GameObject m_Prepab, Transform com_Parent = null)
    {
        if (com_Parent == null)
        {
            return MonoBehaviour.Instantiate(m_Prepab);
        }
        else
        {
            return MonoBehaviour.Instantiate(m_Prepab, com_Parent);
        }
    }

    #endregion

    #region Destroy

    public static void SetGameObjectDestroy(GameObject m_GameObject)
    {
        if (m_GameObject != null)
        {
            MonoBehaviour.Destroy(m_GameObject);
        }
    }

    #endregion

    #region Get

    //NOTE:
    //Folder(s) "Resources" can be created everywhere from root "Assests/*", that can be access by Unity or Application

    //BEWARD:
    //All content(s) in folder(s) "Resources" will be builded to Application, even they m_ightn't be used in Build-Game Application

    public static List<GameObject> GetResourcemPrefab(string m_PathInResources)
    {
        GameObject[] m_Prefam_Array = Resources.LoadAll<GameObject>(ClassString.GetStringReplaceResources(m_PathInResources));
        List<GameObject> m_PrefamList = new List<GameObject>();
        m_PrefamList.AddRange(m_Prefam_Array);
        return m_PrefamList;
    }

    public static List<Sprite> GetResourcemSprite(string m_PathInResources)
    {
        Sprite[] m_Sprite_Array = Resources.LoadAll<Sprite>(ClassString.GetStringReplaceResources(m_PathInResources));
        List<Sprite> m_SpriteList = new List<Sprite>();
        m_SpriteList.AddRange(m_Sprite_Array);
        return m_SpriteList;
    }

    public static List<TextAsset> GetResourcemTextAsset(string m_PathInResources)
    {
        TextAsset[] m_TextAsset_Array = Resources.LoadAll<TextAsset>(ClassString.GetStringReplaceResources(m_PathInResources));
        List<TextAsset> m_TextAssetList = new List<TextAsset>();
        m_TextAssetList.AddRange(m_TextAsset_Array);
        return m_TextAssetList;
    }

    #endregion

    #endregion

    #region ================================================================== Object

    public static bool GetCheckObjectType<Type>(object m_Object)
    {
        return m_Object is Type;
    }

    #endregion
}