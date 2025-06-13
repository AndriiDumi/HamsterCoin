using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;

    private void OnEnable()
    {
        // ³����� �������� ������ � BalanceManager
        balanceText.text = "������: " + BalanceManager.Instance.GetBalance().ToString("F2");

        // ϳ��������� �� ��������� �������, ��� ���������� UI, ���� ������ ��������
        BalanceManager.OnBalanceChanged += UpdateBalanceText;
    }

    private void OnDisable()
    {
        BalanceManager.OnBalanceChanged -= UpdateBalanceText;
    }

    private void UpdateBalanceText(float newBalance)
    {
        balanceText.text = "������: " + newBalance.ToString("F2");
    }
}
