using UnityEngine;

public class RocketAnimationUI : MonoBehaviour
{
    public RectTransform rocketUI; // Хомяк
    public RectTransform waveUI;   // Хвиля

    public Vector2 startRocketPos = new Vector2(-500f, -200f);
    public Vector2 endRocketPos = new Vector2(500f, 300f);

    public Vector2 startWavePos = new Vector2(-500f, -300f);
    public Vector2 endWavePos = new Vector2(500f, -250f);

    public Vector2 startWaveScale = new Vector2(0.5f, 0.5f);
    public Vector2 midWaveScale = new Vector2(1.5f, 1.2f);
    public Vector2 endWaveScale = new Vector2(1.8f, 1.4f);

    private float growDuration = 5f;
    private float moveDuration = 10f;

    private float elapsedTime = 0f;
    private bool isAnimating = false;

    public float surfAmplitude = 20f;   // Амплітуда коливань хомяка
    public float surfFrequency = 1.5f;  // Частота коливань (в колах в секунду)

    void Start()
    {
        // Для прикладу, запускаємо анімацію відразу
        // LaunchRocket();
    }

    public void LaunchRocket()
    {
        elapsedTime = 0f;
        isAnimating = true;

        rocketUI.anchoredPosition = startRocketPos;
        waveUI.anchoredPosition = startWavePos;
        waveUI.localScale = startWaveScale;

        rocketUI.gameObject.SetActive(true);
        waveUI.gameObject.SetActive(true);
    }

    public void StopRocket()
    {
        isAnimating = false;
        rocketUI.gameObject.SetActive(false);
        waveUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isAnimating) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime < growDuration)
        {
            float t = elapsedTime / growDuration;
            waveUI.localScale = Vector2.Lerp(startWaveScale, midWaveScale, t);
            waveUI.anchoredPosition = Vector2.Lerp(startWavePos, startWavePos, t); // майже не рухаємось
        }
        else if (elapsedTime < growDuration + moveDuration)
        {
            float t = (elapsedTime - growDuration) / moveDuration;
            waveUI.localScale = Vector2.Lerp(midWaveScale, endWaveScale, t);
            waveUI.anchoredPosition = Vector2.Lerp(startWavePos, endWavePos, t);
        }
        else
        {
            // Хвиля залишається на кінцевій позиції і масштабі
            waveUI.anchoredPosition = endWavePos;
            waveUI.localScale = endWaveScale;
        }

        // Хомяк рухається плавно з коливаннями
        Vector2 basePos;

        if (elapsedTime < growDuration)
        {
            float t = elapsedTime / growDuration;
            basePos = Vector2.Lerp(startRocketPos, startRocketPos + new Vector2(0, 50f), t);
        }
        else if (elapsedTime < growDuration + moveDuration)
        {
            float t = (elapsedTime - growDuration) / moveDuration;
            basePos = Vector2.Lerp(startRocketPos + new Vector2(0, 50f), endRocketPos, t);
        }
        else
        {
            basePos = endRocketPos;
        }

        float surfOffsetY = Mathf.Sin(elapsedTime * surfFrequency * Mathf.PI * 2) * surfAmplitude;
        rocketUI.anchoredPosition = new Vector2(basePos.x, basePos.y + surfOffsetY);
    }
    public void HideAnimation()
    {
        rocketUI.gameObject.SetActive(false);
        waveUI.gameObject.SetActive(false);
    }

}


