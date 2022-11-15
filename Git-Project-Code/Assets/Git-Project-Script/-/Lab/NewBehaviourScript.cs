using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        GameObject m_Object = GitGameObject.SetGameObjectCreate("abc");
        m_Object.AddComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Debug.LogFormat("{0} - {1} - {2}",
            Camera.main.transform.position,
            GitResolution.GetCameraSizeUnit(),
            Camera.main.transform.position.x - GitResolution.GetCameraSizeUnit().x / 2);
    }
}
