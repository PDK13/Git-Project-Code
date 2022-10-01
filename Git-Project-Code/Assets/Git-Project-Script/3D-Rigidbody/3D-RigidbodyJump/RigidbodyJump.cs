using UnityEngine;

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
    private bool m_UseScriptControl = true;

    /// <summary>
    /// Jump
    /// </summary>
    [SerializeField]
    private KeyCode k_Jump = KeyCode.Space;

    /// <summary>
    /// Hold Jump to Jump Higher?
    /// </summary>
    [SerializeField]
    private bool m_HoldJump = false;

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
    private RigidbodyComponent cm_Rigid;

    #endregion

    private void Start()
    {
        cm_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_UseScriptControl)
        {
            SetJumpButton();
        }
    }

    /// <summary>
    /// Set Jump by Keyboard
    /// </summary>
    public void SetJumpButton()
    {
        if (!m_HoldJump && Input.GetKeyDown(k_Jump))
        {
            SetJump();
        }
        else
        if (m_HoldJump && Input.GetKey(k_Jump))
        {
            SetJump();
        }
    }

    /// <summary>
    /// Set Jump Auto
    /// </summary>
    /// <param name="m_JumpHold"></param>
    public void SetJumpAuto(bool m_JumpHold)
    {
        if (m_JumpHold)
        {
            SetJump();
        }
    }

    /// <summary>
    /// Set Jump
    /// </summary>
    public void SetJump()
    {
        if (cm_Rigid.GetCheckFoot())
        {
            cm_Rigid.SetMoveY_Jump(m_JumpVelocity);
        }
    }
}