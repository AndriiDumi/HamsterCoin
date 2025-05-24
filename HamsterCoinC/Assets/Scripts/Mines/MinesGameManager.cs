using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinesGameManager : MonoBehaviour
{
    [Header("Менеджер сітки")]
    public GridManager gridManager;

    [Header("UI Елементи")]
    public TMP_Text statusText;
    public TMP_Text balanceText;
    public TMP_Text betAmountText;
    public TMP_InputField betInput;
    public Button takeMoneyButton;
    public Button startGameButton;

    [Header("Налаштування гри")]
    public float multiplier = 1.2f;

    private bool gameOver = true;
    private bool gameStarted = false;

    private int balance = 1000;
    private int currentBet = 0;

    private void Start()
    {
        gridManager.gameManager = this;

        takeMoneyButton.onClick.AddListener(TakeMoney);
        startGameButton.onClick.AddListener(StartGame);

        takeMoneyButton.gameObject.SetActive(false);

        UpdateBalanceUI();
        betAmountText.text = "Депозит: 0";
        statusText.text = "Введіть ставку та натисніть Почати";

        // Забороняємо кліки на клітинки до старту гри
        gridManager.DisableAllCells();
    }

    public void StartGame()
    {
        if (!int.TryParse(betInput.text, out currentBet) || currentBet <= 0)
        {
            statusText.text = "Введіть коректну ставку!";
            return;
        }

        if (currentBet > balance)
        {
            statusText.text = "Ставка не може перевищувати баланс!";
            return;
        }

        balance -= currentBet;
        UpdateBalanceUI();

        betAmountText.text = $"Депозит: {currentBet}";

        gameOver = false;
        gameStarted = true;
        statusText.text = "";
        takeMoneyButton.gameObject.SetActive(true);

        gridManager.RegenerateGrid();
    }

    public void CellClicked(Cell cell)
    {
        if (gameOver || !gameStarted) return;

        if (cell.hasMine)
        {
            currentBet = 0;
            betAmountText.text = $"Депозит: {currentBet}";
            GameOver(false);
        }
        else
        {
            currentBet = Mathf.CeilToInt(currentBet * multiplier);
            betAmountText.text = $"Депозит: {currentBet}";
        }
    }

    public void TakeMoney()
    {
        if (gameOver || !gameStarted) return;

        balance += currentBet;
        currentBet = 0;
        UpdateBalanceUI();

        statusText.text = "Ви забрали гроші!";
        takeMoneyButton.gameObject.SetActive(false);
        gameOver = true;
        gameStarted = false;

        gridManager.DisableAllCells();
    }

    void GameOver(bool win)
    {
        gameOver = true;
        gameStarted = false;

        takeMoneyButton.gameObject.SetActive(false);

        if (win)
        {
            statusText.text = "Ви виграли!";
        }
        else
        {
            statusText.text = "Програш! Ви втратили ставку.";
        }

        gridManager.RevealAllCells();
    }

    void UpdateBalanceUI()
    {
        balanceText.text = $"Баланс: {balance}";
    }
}
