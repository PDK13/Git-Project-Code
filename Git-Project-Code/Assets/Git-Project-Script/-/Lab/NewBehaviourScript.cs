using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.LogFormat("{0} - {1} - {2}",
            Camera.main.transform.position,
            GitResolution.GetCameraSizeUnit(),
            Camera.main.transform.position.x - GitResolution.GetCameraSizeUnit().x / 2);
    }
}
