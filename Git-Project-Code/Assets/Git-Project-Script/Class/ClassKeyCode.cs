using System;
using UnityEngine;

public class ClassKeyCode
{
    #region Key Pressed D

    /// <summary>
    /// Get Key D
    /// </summary>
    /// <returns></returns>
    public static KeyCode GetKeyCodePressedD()
    {
        foreach (KeyCode m_KeyKeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(m_KeyKeyCode))
            {
                return m_KeyKeyCode;
            }
        }
        return KeyCode.None;
    }

    /// <summary>
    /// Get Key U
    /// </summary>
    /// <returns></returns>
    public static KeyCode GetKeyCodePressedU()
    {
        foreach (KeyCode m_KeyKeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(m_KeyKeyCode))
            {
                return m_KeyKeyCode;
            }
        }
        return KeyCode.None;
    }

    /// <summary>
    /// Get Key Hold
    /// </summary>
    /// <returns></returns>
    public static KeyCode GetKeyCodePressedHold()
    {
        foreach (KeyCode m_KeyKeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(m_KeyKeyCode))
            {
                return m_KeyKeyCode;
            }
        }
        return KeyCode.None;
    }

    #endregion

    #region Mouse

    public static void SetMouseVisible(bool mAllowMouseVisble)
    {
        Cursor.visible = mAllowMouseVisble;
    }

    /// <summary>
    /// m_ouse Pos
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouse()
    {
        return Input.mousePosition;
    }

    #region m_ouse Any

    /// <summary>
    /// Get m_ouse D
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseAnyD()
    {
        return
            Input.GetKeyDown(KeyCode.Mouse0) || //L m_ouse
            Input.GetKeyDown(KeyCode.Mouse1) || //R m_ouse
            Input.GetKeyDown(KeyCode.Mouse2) || //Midle m_ouse
            Input.GetKeyDown(KeyCode.Mouse3) ||
            Input.GetKeyDown(KeyCode.Mouse4) ||
            Input.GetKeyDown(KeyCode.Mouse5) ||
            Input.GetKeyDown(KeyCode.Mouse6);
    }

    /// <summary>
    /// Get m_ouse U
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseAnyU()
    {
        return
            Input.GetKeyUp(KeyCode.Mouse0) || //L m_ouse
            Input.GetKeyUp(KeyCode.Mouse1) || //R m_ouse
            Input.GetKeyUp(KeyCode.Mouse2) || //Midle m_ouse
            Input.GetKeyUp(KeyCode.Mouse3) ||
            Input.GetKeyUp(KeyCode.Mouse4) ||
            Input.GetKeyUp(KeyCode.Mouse5) ||
            Input.GetKeyUp(KeyCode.Mouse6);
    }

    /// <summary>
    /// Get m_ouse Hold
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseAnyHold()
    {
        return
            Input.GetKey(KeyCode.Mouse0) || //L m_ouse
            Input.GetKey(KeyCode.Mouse1) || //R m_ouse
            Input.GetKey(KeyCode.Mouse2) || //Midle m_ouse
            Input.GetKey(KeyCode.Mouse3) ||
            Input.GetKey(KeyCode.Mouse4) ||
            Input.GetKey(KeyCode.Mouse5) ||
            Input.GetKey(KeyCode.Mouse6);
    }

    #endregion

    #region m_ouse L

    /// <summary>
    /// Get m_ouse L D
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseLD()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    /// <summary>
    /// Get m_ouse L U
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseLU()
    {
        return Input.GetKeyUp(KeyCode.Mouse0);
    }

    /// <summary>
    /// Get m_ouse L Hold
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseLHold()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    #endregion

    #region m_ouse R

    /// <summary>
    /// Get m_ouse R D
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseRD()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    /// <summary>
    /// Get m_ouse R U
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseRU()
    {
        return Input.GetKeyUp(KeyCode.Mouse1);
    }

    /// <summary>
    /// Get m_ouse R Hold
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseRHold()
    {
        return Input.GetKey(KeyCode.Mouse1);
    }

    #endregion

    #region m_ouse m_id

    /// <summary>
    /// Get m_ouse m_id D
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseMidD()
    {
        return Input.GetKeyDown(KeyCode.Mouse2);
    }

    /// <summary>
    /// Get m_ouse m_id U
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseMidU()
    {
        return Input.GetKeyUp(KeyCode.Mouse2);
    }

    /// <summary>
    /// Get m_ouse m_id Hold
    /// </summary>
    /// <returns></returns>
    public static bool GetCheckMouseMidHold()
    {
        return Input.GetKey(KeyCode.Mouse2);
    }

    #endregion

    #endregion

    #region Keyboard

    /// <summary>
    /// Get Keyboard Hold
    /// </summary>
    /// <param name="m_KeyKeyboard"></param>
    /// <returns></returns>
    public static bool GetCheckKeyboardHold(KeyCode m_KeyKeyboard)
    {
        return Input.GetKey(m_KeyKeyboard);
    }

    /// <summary>
    /// Get Keyboard D
    /// </summary>
    /// <param name="m_KeyKeyboard"></param>
    /// <returns></returns>
    public static bool GetCheckKeyboardD(KeyCode m_KeyKeyboard)
    {
        return Input.GetKeyDown(m_KeyKeyboard);
    }

    /// <summary>
    /// Get Keyboard U
    /// </summary>
    /// <param name="m_KeyKeyboard"></param>
    /// <returns></returns>
    public static bool GetCheckKeyboardU(KeyCode m_KeyKeyboard)
    {
        return Input.GetKeyUp(m_KeyKeyboard);
    }

    #endregion

    #region Key Simple

    /// <summary>
    /// Convert 'KeyCode.toString' to 'SimpleString'
    /// </summary>
    /// <param name="m_KeyKey"></param>
    /// <returns></returns>
    public static string GetKeyCodeSimple(KeyCode m_KeyKey)
    {
        //Key [ ]
        if (m_KeyKey == KeyCode.LeftBracket)
        {
            return "[";
        }

        if (m_KeyKey == KeyCode.RightBracket)
        {
            return "]";
        }
        //Key { }
        if (m_KeyKey == KeyCode.LeftCurlyBracket)
        {
            return "{";
        }

        if (m_KeyKey == KeyCode.RightCurlyBracket)
        {
            return "}";
        }
        //Key ( )
        if (m_KeyKey == KeyCode.LeftParen)
        {
            return "(";
        }

        if (m_KeyKey == KeyCode.RightParen)
        {
            return ")";
        }
        //Key Shift
        if (m_KeyKey == KeyCode.LeftShift)
        {
            return "L-Shift";
        }

        if (m_KeyKey == KeyCode.RightShift)
        {
            return "R-Shift";
        }
        //Key Alt
        if (m_KeyKey == KeyCode.LeftAlt)
        {
            return "L-Alt";
        }

        if (m_KeyKey == KeyCode.RightAlt)
        {
            return "R-Alt";
        }
        //Key Page
        if (m_KeyKey == KeyCode.PageUp)
        {
            return "Page-U";
        }

        if (m_KeyKey == KeyCode.PageDown)
        {
            return "Page-D";
        }
        //Key Another
        if (m_KeyKey == KeyCode.Escape)
        {
            return "Esc";
        }

        if (m_KeyKey == KeyCode.Return)
        {
            return "Enter";
        }

        if (m_KeyKey == KeyCode.Delete)
        {
            return "Del";
        }

        if (m_KeyKey == KeyCode.Backspace)
        {
            return "B-Space";
        }

        return m_KeyKey.ToString();
    }

    #endregion
}