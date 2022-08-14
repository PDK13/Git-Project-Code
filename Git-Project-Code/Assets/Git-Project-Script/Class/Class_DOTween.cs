//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using DG.Tweening;
//using UnityEngine.UI;

///// <summary>
///// Quick use DOTween in easy way for beginer, can't use for difficult situation!
///// </summary>
//public class Class_DOTween
//{
//    //Duration: Time for doing command
//    //Delay: Time wait before doing command

//    #region UI

//    #region UI RecTransform

//    #region UI Anchor-Pos

//    public static void Set_UI_Pos_ChanceTo(RectTransform rec_UI, Vector2 v2_Pos_ChanceTo, float f_Duration = 1f, float f_Delay = 0f, bool b_ValueInt = false)
//    {
//        rec_UI.DOAnchorPos(v2_Pos_ChanceTo, f_Duration, b_ValueInt).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Pos_ChanceTo_X(RectTransform rec_UI, float f_Pos_ChanceTo_X, float f_Duration = 1f, float f_Delay = 0f, bool b_ValueInt = false)
//    {
//        rec_UI.DOAnchorPosX(f_Pos_ChanceTo_X, f_Duration, b_ValueInt).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Pos_ChanceTo_Y(RectTransform rec_UI, float f_Pos_ChanceTo_Y, float f_Duration = 1f, float f_Delay = 0f, bool b_ValueInt = false)
//    {
//        rec_UI.DOAnchorPosY(f_Pos_ChanceTo_Y, f_Duration, b_ValueInt).SetDelay(f_Delay);
//    }

//    #endregion

//    #region UI Anchor Pos Shake

//    public static void Set_UI_Shake(RectTransform rec_UI, float f_Duration = 1f, float f_Strength = 50f, int i_Loop = 10, float f_Randomness = 0f, bool b_FadeOut = true, bool b_ValueInt = false)
//    {
//        rec_UI.DOShakeAnchorPos(f_Duration, f_Strength, i_Loop, f_Randomness, b_ValueInt, b_FadeOut);
//    }

//    #endregion

//    #region UI Scale

//    public static void Set_UI_Scale_ChanceTo(RectTransform rec_UI, Vector2 v2_Scale_ChanceTo, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DOScale(v2_Scale_ChanceTo, f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Scale_ChanceTo_X(RectTransform rec_UI, float f_Scale_ChanceTo_X, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DOScaleX(f_Scale_ChanceTo_X, f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Scale_ChanceTo_Y(RectTransform rec_UI, float f_Scale_ChanceTo_Y, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DOScaleY(f_Scale_ChanceTo_Y, f_Duration).SetDelay(f_Delay);
//    }

//    #endregion

//    #region UI Rotation (Quaternion)

//    public static void Set_UI_Rotation_ChanceTo(RectTransform rec_UI, Vector3 v3_Rotation_ChanceTo, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DORotate(v3_Rotation_ChanceTo, f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Rotation_ChanceTo_X(RectTransform rec_UI, float f_Scale_ChanceTo_X, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(f_Scale_ChanceTo_X, rec_UI.rotation.y, rec_UI.rotation.z), f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Rotation_ChanceTo_Y(RectTransform rec_UI, float f_Scale_ChanceTo_Y, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(rec_UI.rotation.x, f_Scale_ChanceTo_Y, rec_UI.rotation.z), f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Rotation_ChanceTo_Z(RectTransform rec_UI, float f_Scale_ChanceTo_Z, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        rec_UI.DORotate(new Vector3(rec_UI.rotation.x, rec_UI.rotation.y, f_Scale_ChanceTo_Z), f_Duration).SetDelay(f_Delay);
//    }

//    #endregion

//    #endregion

//    #region UI Fade

//    public static void Set_UI_Fade_ChanceTo(CanvasGroup com_CanvasGroup, float f_Fade = 1f, float f_Duration = 1f)
//    {
//        if (f_Fade > 1f)
//        {
//            com_CanvasGroup.DOFade(1f, f_Duration);
//        }
//        else
//        if (f_Fade < 0f)
//        {
//            com_CanvasGroup.DOFade(0f, f_Duration);
//        }
//        else
//        {
//            com_CanvasGroup.DOFade(f_Fade, f_Duration);
//        }
//    }

//    public static void Set_UI_Fade_ChanceTo(Text t_Text, float f_Fade = 1f, float f_Duration = 1f)
//    {
//        if (f_Fade > 1f)
//        {
//            t_Text.DOFade(1f, f_Duration);
//        }
//        else
//        if (f_Fade < 0f)
//        {
//            t_Text.DOFade(0f, f_Duration);
//        }
//        else
//        {
//            t_Text.DOFade(f_Fade, f_Duration);
//        }
//    }

//    public static void Set_UI_Fade_ChanceTo(Image i_Image, float f_Fade = 1f, float f_Duration = 1f)
//    {
//        if (f_Fade > 1f)
//        {
//            i_Image.DOFade(1f, f_Duration);
//        }
//        else
//        if (f_Fade < 0f)
//        {
//            i_Image.DOFade(0f, f_Duration);
//        }
//        else
//        {
//            i_Image.DOFade(f_Fade, f_Duration);
//        }
//    }

//    #endregion

//    #region UI Color

//    public static void Set_UI_Color_ChanceTo(Text t_Text, Color c_Color_ChanceTo, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        t_Text.DOColor(c_Color_ChanceTo, f_Duration).SetDelay(f_Delay);
//    }

//    public static void Set_UI_Color_ChanceTo(Image i_Image, Color c_Color_ChanceTo, float f_Duration = 1f, float f_Delay = 0f)
//    {
//        i_Image.DOColor(c_Color_ChanceTo, f_Duration).SetDelay(f_Delay);
//    }

//    #endregion

//    #endregion

//    #region Value

//    public static IEnumerator Set_DOTween_Sample(int i_Number, Text t_Text)
//    {
//        DOTween.To(() => 0, x => i_Number = x, 100, 1f);

//        t_Text.text = i_Number.ToString();

//        yield return i_Number == 100;
//    }

//    #endregion
//}
