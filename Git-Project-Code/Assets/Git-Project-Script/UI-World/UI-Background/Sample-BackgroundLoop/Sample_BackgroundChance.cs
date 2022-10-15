using UnityEngine;

public class Sample_BackgroundChance : MonoBehaviour
{
    [Header("Keyboard")]

    [SerializeField] private KeyCode m_KeyBackgroundNext = KeyCode.RightArrow;
    [SerializeField] private KeyCode m_KeyBackgroundPrev = KeyCode.LeftArrow;

    [Header("BackgroundNum 0 -> 3")]
    public int backgroundNum;
    public Sprite[] LayerSprites;
    private readonly GameObject[] LayerObject = new GameObject[5];
    private readonly int m_ax_backgroundNum = 3;

    private void Start()
    {
        for (int i = 0; i < LayerObject.Length; i++)
        {
            LayerObject[i] = GameObject.Find("Layer_" + i);
        }

        ChangeSprite();
    }

    private void Update()
    {
        //for presentation without U
        if (Input.GetKeyDown(m_KeyBackgroundNext))
        {
            NextBG();
        }

        if (Input.GetKeyDown(m_KeyBackgroundPrev))
        {
            BackBG();
        }
    }

    private void ChangeSprite()
    {
        LayerObject[0].GetComponent<SpriteRenderer>().sprite = LayerSprites[backgroundNum * 5];
        for (int i = 1; i < LayerObject.Length; i++)
        {
            Sprite changeSprite = LayerSprites[backgroundNum * 5 + i];
            //Change Layer1->7
            LayerObject[i].GetComponent<SpriteRenderer>().sprite = changeSprite;
            //Change "Layer(*)x" sprites in children of Layer1->7
            LayerObject[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
            LayerObject[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }

    public void NextBG()
    {
        backgroundNum = backgroundNum + 1;
        if (backgroundNum > m_ax_backgroundNum)
        {
            backgroundNum = 0;
        }

        ChangeSprite();
    }
    public void BackBG()
    {
        backgroundNum = backgroundNum - 1;
        if (backgroundNum < 0)
        {
            backgroundNum = m_ax_backgroundNum;
        }

        ChangeSprite();
    }
}
