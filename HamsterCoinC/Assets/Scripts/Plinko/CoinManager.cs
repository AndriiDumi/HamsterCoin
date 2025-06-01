using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinText != null)
        {
            float currentBalance = BalanceManager.Instance.GetBalance();
            coinText.text = currentBalance.ToString("F3").Replace(",", ".");
        }
    }

    public void UseCoins(float amount)
    {
        BalanceManager.Instance.SubtractBalance(amount);
        UpdateUI();
    }

    public void GainedCoins(float amount)
    {
        BalanceManager.Instance.AddBalance(amount);
        UpdateUI();
    }

    public float getCoins()
    {
        return BalanceManager.Instance.GetBalance();
    }
}
