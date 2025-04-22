using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text coefficientText;
    public TMP_Text timerText;
    public TMP_Text betValueText;
    public TMP_InputField betInput;
    public Button betButton;
    public Button cashOutButton;

    public RoundManager roundManager;
    public BetManager betManager;

    private float currentBet = 0f;

    void Start()
    {
        betButton.onClick.AddListener(PlaceBet);
        cashOutButton.onClick.AddListener(CashOut);
        cashOutButton.interactable = false;
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        coefficientText.text = "Коефіцієнт: " + roundManager.coefficient.ToString("F2");

        if (!roundManager.roundActive)
        {
            timerText.text = "Час до наступного раунду: " + roundManager.GetCooldownTime().ToString("F2");
            betValueText.text = "Ваша ставка: 0";
        }
        else
        {
            if (roundManager.betTimeActive)
            {
                // Показуємо таймер для ставок
                timerText.text = "Час на ставку: " + roundManager.betTime.ToString("F2");
                betValueText.text = "Ваша ставка: " + (betManager.GetCurrentBetValue() > 0 ? betManager.GetCurrentBetValue().ToString("F2") : "0");
            }
            else
            {
                // Показуємо основний таймер гри
                betValueText.text = "Ваша ставка: " + betManager.GetCurrentBetValue().ToString("F2");
            }
        }

        cashOutButton.interactable = roundManager.roundActive && !roundManager.betTimeActive;
        betButton.interactable = roundManager.roundActive && roundManager.betTimeActive; // Дозволити ставити ставки тільки під час часу на ставку
    }

    public void PlaceBet()
    {
        if (!roundManager.roundActive || !roundManager.betTimeActive) return;

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
}
