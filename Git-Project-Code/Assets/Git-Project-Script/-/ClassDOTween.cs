//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using DG.Tweening;
//using UnityEngine.UI;

///// <summary>
///// Quick use DOTween in easy way for beginer, can't use for difficult situation!
///// </summary>
//public class ClassDOTween
//{
//    //Duration: Time for doing command
//    //Delay: Time wait before doing command

//    #region UI

//    #region UI RecTransform

//    #region UI Anchor-Pos

//    public static void SetUI_PosChanceTo(RectTransform comUI, Vector2 v2_PosChanceTo, float m_Duration = 1f, float m_Delay = 0f, bool mAllowValueInt = false)
//    {
//        comUI.DOAnchorPos(v2_PosChanceTo, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    public static void SetUI_PosChanceToX(RectTransform comUI, float m_PosChanceToX, float m_Duration = 1f, float m_Delay = 0f, bool mAllowValueInt = false)
//    {
//        comUI.DOAnchorPosX(m_PosChanceToX, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    public static void SetUI_PosChanceToY(RectTransform comUI, float m_PosChanceToY, float m_Duration = 1f, float m_Delay = 0f, bool mAllowValueInt = false)
//    {
//        comUI.DOAnchorPosY(m_PosChanceToY, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    #endregion

//    #region UI Anchor Pos Shake

//    public static void SetUI_Shake(RectTransform comUI, float m_Duration = 1f, float m_Strength = 50f, int m_Loop = 10, float m_Randomness = 0f, bool mAllowFadeOut = true, bool mAllowValueInt = false)
//    {
//        comUI.DOShakeAnchorPos(m_Duration, m_Strength, m_Loop, m_Randomness, m_ValueInt, m_FadeOut);
//    }

//    #endregion

//    #region UI Scale

//    public static void SetUI_ScaleChanceTo(RectTransform comUI, Vector2 v2_ScaleChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DOScale(v2_ScaleChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_ScaleChanceToX(RectTransform comUI, float m_ScaleChanceToX, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DOScaleX(m_ScaleChanceToX, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_ScaleChanceToY(RectTransform comUI, float m_ScaleChanceToY, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DOScaleY(m_ScaleChanceToY, m_Duration).SetDelay(m_Delay);
//    }

//    #endregion

//    #region UI Rotation (Quaternion)

//    public static void SetUIRotationChanceTo(RectTransform comUI, Vector3 mRotationChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DORotate(m_RotationChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUIRotationChanceToX(RectTransform comUI, float m_ScaleChanceToX, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DORotate(new Vector3(m_ScaleChanceToX, comUI.rotation.y, comUI.rotation.z), m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUIRotationChanceToY(RectTransform comUI, float m_ScaleChanceToY, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DORotate(new Vector3(comUI.rotation.x, m_ScaleChanceToY, comUI.rotation.z), m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUIRotationChanceToZ(RectTransform comUI, float m_ScaleChanceToZ, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        comUI.DORotate(new Vector3(comUI.rotation.x, comUI.rotation.y, m_ScaleChanceToZ), m_Duration).SetDelay(m_Delay);
//    }

//    #endregion

//    #endregion

//    #region UI Fade

//    public static void SetUI_FadeChanceTo(CanvasGroup m_CanvasGroup, float m_Fade = 1f, float m_Duration = 1f)
//    {
//        if (m_Fade > 1f)
//        {
//            m_CanvasGroup.DOFade(1f, m_Duration);
//        }
//        else
//        if (m_Fade < 0f)
//        {
//            m_CanvasGroup.DOFade(0f, m_Duration);
//        }
//        else
//        {
//            m_CanvasGroup.DOFade(m_Fade, m_Duration);
//        }
//    }

//    public static void SetUI_FadeChanceTo(Text t_Text, float m_Fade = 1f, float m_Duration = 1f)
//    {
//        if (m_Fade > 1f)
//        {
//            t_Text.DOFade(1f, m_Duration);
//        }
//        else
//        if (m_Fade < 0f)
//        {
//            t_Text.DOFade(0f, m_Duration);
//        }
//        else
//        {
//            t_Text.DOFade(m_Fade, m_Duration);
//        }
//    }

//    public static void SetUI_FadeChanceTo(Image m_Image, float m_Fade = 1f, float m_Duration = 1f)
//    {
//        if (m_Fade > 1f)
//        {
//            m_Image.DOFade(1f, m_Duration);
//        }
//        else
//        if (m_Fade < 0f)
//        {
//            m_Image.DOFade(0f, m_Duration);
//        }
//        else
//        {
//            m_Image.DOFade(m_Fade, m_Duration);
//        }
//    }

//    #endregion

//    #region UI Color

//    public static void SetUI_ColorChanceTo(Text t_Text, Color c_ColorChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        t_Text.DOColor(c_ColorChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_ColorChanceTo(Image m_Image, Color c_ColorChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        m_Image.DOColor(c_ColorChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    #endregion

//    #endregion

//    #region Value

//    public static IEnumerator SetDOTween_Sample(int m_Number, Text t_Text)
//    {
//        DOTween.To(() => 0, x => m_Number = x, 100, 1f);

//        t_Text.text = m_Number.ToString();

//        yield return m_Number == 100;
//    }

//    #endregion
//}
