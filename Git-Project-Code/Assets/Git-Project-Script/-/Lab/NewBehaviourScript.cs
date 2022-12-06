using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        List<int> m_List = GitEnum.GetEnumListIndex<GitOpption>();
        Debug.Log(m_List[1]);
    }
}
