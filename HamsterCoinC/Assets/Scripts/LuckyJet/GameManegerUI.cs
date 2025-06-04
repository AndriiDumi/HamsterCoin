using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text coefficientText;
    public TMP_Text timerText;
    public TMP_Text betValueText;
    public TMP_Text balanceText;
    public TMP_Text profitText;

    public TMP_InputField betInput;
    public Button betButton;
    public Button cashOutButton;

    public Button resetBalanceButton;

    // Кнопки для зміни ставки
    public Button maxBetButton;
    public Button minBetButton;
    public Button doubleBetButton;
    public Button halveBetButton;

    public RoundManager roundManager;
    public BetManager betManager;

    private float currentBet = 0f;

    void Start()
    {
        betButton.onClick.AddListener(PlaceBet);
        cashOutButton.onClick.AddListener(CashOut);

        resetBalanceButton.onClick.AddListener(() => BalanceManager.Instance.ResetBalance());

        // Підключення нових кнопок
        maxBetButton.onClick.AddListener(MaxBet);
        minBetButton.onClick.AddListener(MinBet);
        doubleBetButton.onClick.AddListener(DoubleBet);
        halveBetButton.onClick.AddListener(HalveBet);

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        coefficientText.text = $"x{roundManager.coefficient:F2}";
        balanceText.text = $"{BalanceManager.Instance.GetBalance():F2}";

        float betAmount = betManager.GetRawBetAmount();

        if (roundManager.IsCooldownPhase())
        {
            timerText.text = $"Новий раунд через: {roundManager.GetCooldownTime():F1} сек";
            betValueText.text = $"{betAmount:F2}";

            betButton.gameObject.SetActive(true);
            cashOutButton.gameObject.SetActive(false);
        }
        else if (roundManager.roundActive)
        {
            timerText.text = "";

            float profit = betAmount * roundManager.coefficient;
            betValueText.text = $"{profit:F2}";

            betButton.gameObject.SetActive(false);
            cashOutButton.gameObject.SetActive(true);
        }
        else
        {
            timerText.text = "Очікування...";
            betValueText.text = "";

            betButton.gameObject.SetActive(false);
            cashOutButton.gameObject.SetActive(false);
        }
    }

    public void PlaceBet()
    {
        if (float.TryParse(betInput.text, out currentBet) && currentBet > 0)
        {
            betManager.PlaceBet(currentBet);
        }
        else
        {
            Debug.Log("Некоректна ставка!");
        }
    }

    public void CashOut()
    {
        betManager.CashOut();
    }

    // Методи для нових кнопок — змінюють лише поле вводу:
    public void MaxBet()
    {
        float max = betManager.GetMaxBetAmount();
        betInput.text = max.ToString("F2");
    }

    public void MinBet()
    {
        float min = betManager.GetMinBetAmount();
        betInput.text = min.ToString("F2");
    }

    public void DoubleBet()
    {
        if (float.TryParse(betInput.text, out float currentValue))
        {
            float doubled = betManager.GetDoubleBetAmount(currentValue);
            betInput.text = doubled.ToString("F2");
        }
    }

    public void HalveBet()
    {
        if (float.TryParse(betInput.text, out float currentValue))
        {
            float halved = betManager.GetHalveBetAmount(currentValue);
            betInput.text = halved.ToString("F2");
        }
    }
}
