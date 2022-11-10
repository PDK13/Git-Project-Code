using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float m_Deg = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(this.transform.position, this.transform.position + GitVector.GetDir(Vector3.right * 5, m_Deg, GitVector.Axis.Forward));
    }
}
