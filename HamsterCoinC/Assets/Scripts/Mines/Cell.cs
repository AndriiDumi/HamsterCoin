using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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

        hasMine = false;
        cellImage.sprite = defaultSprite;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
        button.interactable = true;
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void OnClick()
    {
        if (!button.interactable) return;

        button.interactable = false;

        // Запускаємо анімацію
        StartCoroutine(AnimateReveal());

        if (gameManager != null)
            gameManager.CellClicked(this);
    }

    IEnumerator AnimateReveal()
    {
        float duration = 0.3f;
        float timer = 0f;

        // Початкове зменшення старої картинки
        Vector3 originalScale = cellImage.rectTransform.localScale;

        while (timer < duration)
        {
            float t = timer / duration;
            cellImage.rectTransform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Гарантуємо повне зникнення
        cellImage.rectTransform.localScale = Vector3.zero;

        // Зміна картинки
        cellImage.sprite = hasMine ? bombSprite : safeSprite;

        // Анімація появи нової картинки
        timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            cellImage.rectTransform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        cellImage.rectTransform.localScale = originalScale;
    }
}
