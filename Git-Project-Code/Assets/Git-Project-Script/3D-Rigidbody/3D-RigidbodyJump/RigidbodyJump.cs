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
    private readonly float f_JumpVelocity = 5f;

    #endregion

    #region Private

    /// <summary>
    /// Control Velocity GameObject in 3D
    /// </summary>
    private RigidbodyComponent cs_Rigid;

    #endregion

    private void Start()
    {
        cs_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_UseScriptControl)
        {
            Set_JumpButton();
        }
    }

    /// <summary>
    /// Set Jump by Keyboard
    /// </summary>
    public void Set_JumpButton()
    {
        if (!m_HoldJump && Input.GetKeyDown(k_Jump))
        {
            Set_Jump();
        }
        else
        if (m_HoldJump && Input.GetKey(k_Jump))
        {
            Set_Jump();
        }
    }

    /// <summary>
    /// Set Jump Auto
    /// </summary>
    /// <param name="m_JumpHold"></param>
    public void Set_JumpAuto(bool m_JumpHold)
    {
        if (m_JumpHold)
        {
            Set_Jump();
        }
    }

    /// <summary>
    /// Set Jump
    /// </summary>
    public void Set_Jump()
    {
        if (cs_Rigid.GetCheckFoot())
        {
            cs_Rigid.Set_MoveY_Jump(f_JumpVelocity);
        }
    }
}