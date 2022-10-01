//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using DG.Tweening;
//using UnityEngine.UI;

///// <summary>
///// Quick use DOTween in easy way for beginer, can't use for difficult situation!
///// </summary>
//public class Clasm_DOTween
//{
//    //Duration: Time for doing command
//    //Delay: Time wait before doing command

//    #region UI

//    #region UI RecTransform

//    #region UI Anchor-Pos

//    public static void SetUI_Pom_ChanceTo(RectTransform rec_UI, Vector2 v2_Pom_ChanceTo, float m_Duration = 1f, float m_Delay = 0f, bool m_ValueInt = false)
//    {
//        rec_UI.DOAnchorPos(v2_Pom_ChanceTo, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    public static void SetUI_Pom_ChanceTo_X(RectTransform rec_UI, float m_Pom_ChanceTo_X, float m_Duration = 1f, float m_Delay = 0f, bool m_ValueInt = false)
//    {
//        rec_UI.DOAnchorPosX(m_Pom_ChanceTo_X, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    public static void SetUI_Pom_ChanceTo_Y(RectTransform rec_UI, float m_Pom_ChanceTo_Y, float m_Duration = 1f, float m_Delay = 0f, bool m_ValueInt = false)
//    {
//        rec_UI.DOAnchorPosY(m_Pom_ChanceTo_Y, m_Duration, m_ValueInt).SetDelay(m_Delay);
//    }

//    #endregion

//    #region UI Anchor Pos Shake

//    public static void SetUI_Shake(RectTransform rec_UI, float m_Duration = 1f, float m_Strength = 50f, int m_Loop = 10, float m_Randomness = 0f, bool m_FadeOut = true, bool m_ValueInt = false)
//    {
//        rec_UI.DOShakeAnchorPos(m_Duration, m_Strength, m_Loop, m_Randomness, m_ValueInt, m_FadeOut);
//    }

//    #endregion

//    #region UI Scale

//    public static void SetUI_Scale_ChanceTo(RectTransform rec_UI, Vector2 v2_Scale_ChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DOScale(v2_Scale_ChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Scale_ChanceTo_X(RectTransform rec_UI, float m_Scale_ChanceTo_X, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DOScaleX(m_Scale_ChanceTo_X, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Scale_ChanceTo_Y(RectTransform rec_UI, float m_Scale_ChanceTo_Y, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DOScaleY(m_Scale_ChanceTo_Y, m_Duration).SetDelay(m_Delay);
//    }

//    #endregion

//    #region UI Rotation (Quaternion)

//    public static void SetUI_Rotation_ChanceTo(RectTransform rec_UI, Vector3 v3_Rotation_ChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DORotate(v3_Rotation_ChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Rotation_ChanceTo_X(RectTransform rec_UI, float m_Scale_ChanceTo_X, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(m_Scale_ChanceTo_X, rec_UI.rotation.y, rec_UI.rotation.z), m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Rotation_ChanceTo_Y(RectTransform rec_UI, float m_Scale_ChanceTo_Y, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(rec_UI.rotation.x, m_Scale_ChanceTo_Y, rec_UI.rotation.z), m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Rotation_ChanceTo_Z(RectTransform rec_UI, float m_Scale_ChanceTo_Z, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(rec_UI.rotation.x, rec_UI.rotation.y, m_Scale_ChanceTo_Z), m_Duration).SetDelay(m_Delay);
//    }

//    #endregion

//    #endregion

//    #region UI Fade

//    public static void SetUI_Fade_ChanceTo(CanvasGroup com_CanvasGroup, float m_Fade = 1f, float m_Duration = 1f)
//    {
//        if (m_Fade > 1f)
//        {
//            com_CanvasGroup.DOFade(1f, m_Duration);
//        }
//        else
//        if (m_Fade < 0f)
//        {
//            com_CanvasGroup.DOFade(0f, m_Duration);
//        }
//        else
//        {
//            com_CanvasGroup.DOFade(m_Fade, m_Duration);
//        }
//    }

//    public static void SetUI_Fade_ChanceTo(Text t_Text, float m_Fade = 1f, float m_Duration = 1f)
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

//    public static void SetUI_Fade_ChanceTo(Image m_Image, float m_Fade = 1f, float m_Duration = 1f)
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

//    public static void SetUI_Color_ChanceTo(Text t_Text, Color c_Color_ChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        t_Text.DOColor(c_Color_ChanceTo, m_Duration).SetDelay(m_Delay);
//    }

//    public static void SetUI_Color_ChanceTo(Image m_Image, Color c_Color_ChanceTo, float m_Duration = 1f, float m_Delay = 0f)
//    {
//        m_Image.DOColor(c_Color_ChanceTo, m_Duration).SetDelay(m_Delay);
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
