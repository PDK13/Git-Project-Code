using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Collider : MonoBehaviour
{
    private enum ColliderChoice { Red_WithRigid, Blue_NoRigid, }

    [SerializeField] private ColliderChoice m_ColliderChoice = ColliderChoice.Red_WithRigid;

    [SerializeField] private Transform m_Red;
    [SerializeField] private Transform m_Blue;

    private void FixedUpdate()
    {
        switch (m_ColliderChoice)
        {
            case ColliderChoice.Red_WithRigid:
                m_Red.gameObject.SetActive(true);
                m_Blue.gameObject.SetActive(false);
                break;
            case ColliderChoice.Blue_NoRigid:
                m_Red.gameObject.SetActive(false);
                m_Blue.gameObject.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This Methode will find any Collider Trigger in it own and in it's Child that haven't Rigidbody attach to it!!
        //If don't want any Collider Trigger this Methode, add Rigidbody to it!!

        Debug.Log("[Debug] Trigger Enter!!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //This Methode will find any Collider in it own and in it's Child that haven't Rigidbody attach to it!!
        //If don't want any Collider Trigger this Methode, add Rigidbody to it!!

        Debug.Log("[Debug] Collision Enter!!");
    }
}
