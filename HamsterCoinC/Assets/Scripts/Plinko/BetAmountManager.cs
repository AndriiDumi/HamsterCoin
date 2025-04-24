using System;
using TMPro;
using UnityEngine;

public class BetAmountManager : MonoBehaviour
{
    private float betAmount;
    [SerializeField] private TextMeshProUGUI betAmountText;

    public static BetAmountManager instance { get; private set; }

    private int dropCount = 50;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        betAmount = Math.Min(5f, CoinManager.instance.getCoins());

        if (betAmount < 0.01f)
        {
            betAmount = 0.01f;
        }

        betAmountText.text = "DROP\n" + betAmount.ToString("F2").Replace(",", ".");
    }

    public void AdditionOfOne()
    {
        SoundManager.instance.PlayDefault();
        betAmount = Math.Min(betAmount + 1, CoinManager.instance.getCoins());
        if (betAmount < 0.01f)
        {
            betAmount = 0.01f;
        }
        betAmountText.text = "DROP\n" + betAmount.ToString("F2").Replace(",", ".");
    }

    public void MinusOfOne()
    {
        SoundManager.instance.PlayDefault();
        betAmount = Math.Min(betAmount - 1, CoinManager.instance.getCoins());
        if (betAmount < 0.01f)
        {
            betAmount = 0.01f;
        }
        betAmountText.text = "DROP\n" + betAmount.ToString("F2").Replace(",", ".");
    }

    public void Duplication()
    {
        SoundManager.instance.PlayDefault();
        betAmount = Math.Min(betAmount * 2, CoinManager.instance.getCoins());
        if (betAmount < 0.01f)
        {
            betAmount = 0.01f;
        }
        betAmountText.text = "DROP\n" + betAmount.ToString("F2").Replace(",", ".");
    }

    public void Division()
    {
        SoundManager.instance.PlayDefault();
        betAmount = Math.Min(betAmount / 2, CoinManager.instance.getCoins());
        if (betAmount < 0.01f)
        {
            betAmount = 0.01f;
        }
        betAmountText.text = "DROP\n" + betAmount.ToString("F2").Replace(",", ".");
    }

    public void PlaceBet()
    {
        if (CoinManager.instance.getCoins() >= betAmount)
        {
            SoundManager.instance.PlayDefault();

            CoinManager.instance.UseCoins(betAmount);
            BallSpawner.instance.SpawnBall(betAmount);

            dropCount--;
        }
        else
        {
            SoundManager.instance.ErrorPlay();
        }
    }

    public float GetBetAmount()
    {
        return betAmount;
    }
}
