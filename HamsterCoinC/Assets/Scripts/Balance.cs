using TMPro;
using UnityEngine;

public class BalanceDisplay : MonoBehaviour
{
    public TMP_Text balanceText;

    void Start()
    {
        int balance = PlayerPrefs.GetInt("balance", 0);
        balanceText.text = "Баланс: " + balance.ToString();
    }
}
