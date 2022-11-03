using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite m_Sprite;

    public SpriteRenderer m_SpriteRenderer;

    public Camera m_Camera;

    public Canvas m_Canvas;

    private void Start()
    {
        //Debug.LogFormat("[Debug] Sprite: {0} || {1}", GitSprite.GetSizePixel(m_Sprite), GitSprite.GetSizeUnit(m_Sprite));

        //Othor:
        //SIZE = 2 UNIT WORLD in HEIGHT (1 Square 1x1 Unit WORLD take 2 Unit Height on screen to fill full)

        //1 x 1080 / 1920 = 0.5625
        //0.5625 x 2 = 1.125
    }

    private void LateUpdate()
    {
        //Debug.LogFormat("[Debug] Screen: {0} || Camera: {1} | {2}", GitScreen.GetSizePixel(), GitCamera.GetSizePixel(m_Camera), GitCamera.GetSizeUnit(m_Camera));

        //GitSprite.SetFixedCamera(m_SpriteRenderer, m_Camera, new Vector2(1, 1));
    }
}