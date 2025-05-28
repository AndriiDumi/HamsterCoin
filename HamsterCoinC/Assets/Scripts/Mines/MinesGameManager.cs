using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinesGameManager : MonoBehaviour
{
    [Header("������ ������")]
    public Button maxBetButton;
    public Button minBetButton;
    public Button multiplyBetButton;
    public Button divideBetButton;

    [Header("�������� ����")]
    public GridManager gridManager;

    [Header("UI ��������")]
    public TMP_Text statusText;
    public TMP_Text balanceText;
    public TMP_Text currentBetText;
    public TMP_InputField betInput;
    public Button startGameButton;
    public Button takeMoneyButton;

    [Header("������������ ���")]
    public float multiplier = 1.2f;

    private bool gameOver = true;
    private bool gameStarted = false;
    private int currentBet = 0;

    private void Start()
    {
        if (gridManager != null)
            gridManager.gameManager = this;

        // �������� ���� �� ������
        startGameButton.onClick.AddListener(StartGame);
        takeMoneyButton.onClick.AddListener(TakeMoney);
        maxBetButton.onClick.AddListener(SetMaxBet);
        minBetButton.onClick.AddListener(SetMinBet);
        multiplyBetButton.onClick.AddListener(MultiplyBet);
        divideBetButton.onClick.AddListener(DivideBet);

        startGameButton.gameObject.SetActive(true);
        takeMoneyButton.gameObject.SetActive(false);

        if (gridManager != null)
            gridManager.DisableAllCells();

        UpdateUI();
    }

    private void UpdateUI()
    {
        int balance = BalanceManager.Instance.GetBalance();
        balanceText.text = BalanceManager.Instance.GetBalance().ToString();
        currentBetText.text = currentBet.ToString();

        if (!gameStarted)
            statusText.text = "������ ������ �� �������� ������";
    }

    public void StartGame()
    {
        if (!int.TryParse(betInput.text, out currentBet) || currentBet <= 0)
        {
            statusText.text = "? ������ �������� ������!";
            return;
        }

        if (BalanceManager.Instance == null)
        {
            Debug.LogError("? BalanceManager.Instance �� ��������!");
            return;
        }

        if (currentBet > BalanceManager.Instance.GetBalance())
        {
            statusText.text = "? ������ �������� ������!";
            return;
        }

        BalanceManager.Instance.SubtractBalance(currentBet);

        gameOver = false;
        gameStarted = true;
        statusText.text = "";

        startGameButton.gameObject.SetActive(false);
        takeMoneyButton.gameObject.SetActive(true);

        if (gridManager != null)
            gridManager.RegenerateGrid();

        UpdateUI();
    }

    public void CellClicked(Cell cell)
    {
        if (gameOver || !gameStarted || cell == null) return;

        if (cell.hasMine)
        {
            currentBet = 0;
            GameOver(false);
        }
        else
        {
            currentBet = Mathf.CeilToInt(currentBet * multiplier);
        }

        UpdateUI();
    }

    public void TakeMoney()
    {
        if (gameOver || !gameStarted) return;

        if (BalanceManager.Instance != null)
        {
            BalanceManager.Instance.AddBalance(currentBet);
        }

        currentBet = 0;
        statusText.text = "?? �� ������� �����!";
        gameOver = true;
        gameStarted = false;

        takeMoneyButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);

        if (gridManager != null)
            gridManager.DisableAllCells();

        UpdateUI();
    }

    void GameOver(bool win)
    {
        gameOver = true;
        gameStarted = false;

        takeMoneyButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);

        statusText.text = win
            ? "?? �� �������!"
            : "?? �������! �� �������� ������.";

        if (gridManager != null)
            gridManager.RevealAllCells();

        UpdateUI();
    }

    private void SetMaxBet()
    {
        int maxBet = BalanceManager.Instance.GetBalance();
        betInput.text = maxBet.ToString();
    }

    private void SetMinBet()
    {
        betInput.text = "1";
    }

    private void MultiplyBet()
    {
        if (int.TryParse(betInput.text, out int bet))
        {
            int newBet = bet * 2;
            int max = BalanceManager.Instance.GetBalance();
            betInput.text = Mathf.Min(newBet, max).ToString();
        }
    }

    private void DivideBet()
    {
        if (int.TryParse(betInput.text, out int bet))
        {
            int newBet = Mathf.Max(1, bet / 2);
            betInput.text = newBet.ToString();
        }
    }
}
