using TMPro;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    public TMP_Text balanceText;
    private float balance = 1000.0f; // початковий баланс

    void Start()
    {
        UpdateBalanceUI();
    }

    public void AddMoney(float amount)
    {
        balance += amount;
        UpdateBalanceUI();
    }

    public void SubtractMoney(float amount)
    {
        balance -= amount;
        UpdateBalanceUI();
    }

    void UpdateBalanceUI()
    {
        balanceText.text = balance.ToString("F2") + " ?";
    }

    public float GetBalance()
    {
        return balance;
    }
}
