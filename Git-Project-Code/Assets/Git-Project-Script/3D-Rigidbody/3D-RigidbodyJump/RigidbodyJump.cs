﻿using UnityEngine;

/// <summary>
/// Jump Single Time at Once
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]
public class RigidbodyJump : MonoBehaviour
//Control Player Jump
{
    #region Public

    /// <summary>
    /// Keyboard Control Allow?
    /// </summary>
    [Header("Keyboard")]
    [SerializeField]
    private bool m_AllowUseScriptControl = true;

    /// <summary>
    /// Jump
    /// </summary>
    [SerializeField]
    private KeyCode m_KeyJump = KeyCode.Space;

    /// <summary>
    /// Hold Jump to Jump Higher?
    /// </summary>
    [SerializeField]
    private bool m_AllowHoldJump = false;

    /// <summary>
    /// Jump Power
    /// </summary>
    [Header("Jump")]
    private readonly float m_JumpVelocity = 5f;

    #endregion

    #region Private

    /// <summary>
    /// Control Velocity GameObject in 3D
    /// </summary>
    private RigidbodyComponent m_Rigid;

    #endregion

    private void Start()
    {
        m_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_AllowUseScriptControl)
        {
            SetJumpButton();
        }
    }

    /// <summary>
    /// Set Jump by Keyboard
    /// </summary>
    public void SetJumpButton()
    {
        if (!m_AllowHoldJump && Input.GetKeyDown(m_KeyJump))
        {
            SetJump();
        }
        else
        if (m_AllowHoldJump && Input.GetKey(m_KeyJump))
        {
            SetJump();
        }
    }

    /// <summary>
    /// Set Jump Auto
    /// </summary>
    /// <param name="m_JumpHold"></param>
    public void SetJumpAuto(bool m_AllowJumpHold)
    {
        if (m_AllowJumpHold)
        {
            SetJump();
        }
    }

    /// <summary>
    /// Set Jump
    /// </summary>
    public void SetJump()
    {
        if (m_Rigid.GetCheckFoot())
        {
            m_Rigid.SetMoveY_Jump(m_JumpVelocity);
        }
    }
}