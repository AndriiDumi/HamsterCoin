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
        coefficientText.text = "����������: " + roundManager.coefficient.ToString("F2");

        if (!roundManager.roundActive)
        {
            timerText.text = "��� �� ���������� ������: " + roundManager.GetCooldownTime().ToString("F2");
            betValueText.text = "���� ������: 0";
        }
        else
        {
            if (roundManager.betTimeActive)
            {
                // �������� ������ ��� ������
                timerText.text = "��� �� ������: " + roundManager.betTime.ToString("F2");
                betValueText.text = "���� ������: " + (betManager.GetCurrentBetValue() > 0 ? betManager.GetCurrentBetValue().ToString("F2") : "0");
            }
            else
            {
                // �������� �������� ������ ���
                betValueText.text = "���� ������: " + betManager.GetCurrentBetValue().ToString("F2");
            }
        }

        cashOutButton.interactable = roundManager.roundActive && !roundManager.betTimeActive;
        betButton.interactable = roundManager.roundActive && roundManager.betTimeActive; // ��������� ������� ������ ����� �� ��� ���� �� ������
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
            Debug.Log("���������� ������!");
        }
    }

    public void CashOut()
    {
        betManager.CashOut();
    }
}
