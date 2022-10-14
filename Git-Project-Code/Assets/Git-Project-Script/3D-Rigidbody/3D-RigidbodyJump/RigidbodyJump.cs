using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]
public class RigidbodyJump : MonoBehaviour
//Control Player Jump
{
    [Header("Keyboard")]
    [SerializeField]
    private bool m_AllowUseScriptControl = true;

    [SerializeField]
    private KeyCode m_KeyJump = KeyCode.Space;

    [SerializeField]
    private bool m_AllowHoldJump = false;

    [Header("Jump")]
    private readonly float m_JumpVelocity = 5f;

    private RigidbodyComponent m_Rigid;

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

    public void SetJumpAuto(bool mAllowJumpHold)
    {
        if (mAllowJumpHold)
        {
            SetJump();
        }
    }

    public void SetJump()
    {
        if (m_Rigid.GetCheckFoot())
        {
            m_Rigid.SetMoveY_Jump(m_JumpVelocity);
        }
    }
}