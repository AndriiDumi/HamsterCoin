using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x, y;
    public bool hasMine = false;

    public Button button;
    public Image cellImage;

    public Sprite defaultSprite;
    public Sprite bombSprite;
    public Sprite safeSprite;

    private MinesGameManager gameManager;

    public void Init(int x, int y, MinesGameManager gm)
    {
        this.x = x;
        this.y = y;
        this.gameManager = gm;

        cellImage.sprite = defaultSprite;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (hasMine)
        {
            cellImage.sprite = bombSprite;
        }
        else
        {
            cellImage.sprite = safeSprite;
        }

        button.interactable = false;
        gameManager.CellClicked(this);
    }
}
