using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;

    private void OnEnable()
    {
        // Відразу показуємо баланс з BalanceManager
        balanceText.text = "Баланс: " + BalanceManager.Instance.GetBalance().ToString("F2");

        // Підписуємося на оновлення балансу, щоб оновлювати UI, якщо баланс зміниться
        BalanceManager.OnBalanceChanged += UpdateBalanceText;
    }

    private void OnDisable()
    {
        BalanceManager.OnBalanceChanged -= UpdateBalanceText;
    }

    private void UpdateBalanceText(float newBalance)
    {
        balanceText.text = "Баланс: " + newBalance.ToString("F2");
    }
}
