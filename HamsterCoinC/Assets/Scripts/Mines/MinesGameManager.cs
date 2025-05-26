using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MinesGameManager : MonoBehaviour
{
    [Header("Менеджер сітки")]
    public GridManager gridManager;

    [Header("UI Елементи")]
    public TMP_Text statusText;
    public TMP_Text betAmountText;
    public TMP_InputField betInput;
    public Button takeMoneyButton;
    public Button startGameButton;

    [Header("Налаштування гри")]
    public float multiplier = 1.2f;

    private bool gameOver = true;
    private bool gameStarted = false;

    private int currentBet = 0;

    private void Start()
    {
        if (gridManager != null)
            gridManager.gameManager = this;

        takeMoneyButton.onClick.AddListener(TakeMoney);
        startGameButton.onClick.AddListener(StartGame);

        takeMoneyButton.gameObject.SetActive(false);

        betAmountText.text = "Депозит: 0";
        statusText.text = "Введіть ставку та натисніть Почати";

        if (gridManager != null)
            gridManager.DisableAllCells();
    }

    public void StartGame()
    {
        string token = PlayerPrefs.GetString("jwtToken");
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("? JWT токен не знайдено. Перехід на сцену входу.");
            SceneManager.LoadScene("step1");
            return;
        }

        if (!int.TryParse(betInput.text, out currentBet) || currentBet <= 0)
        {
            statusText.text = "? Введіть коректну ставку!";
            return;
        }

        if (BalanceManager.Instance == null)
        {
            Debug.LogError("? BalanceManager.Instance не знайдено!");
            return;
        }

        if (currentBet > BalanceManager.Instance.GetBalance())
        {
            statusText.text = "? Ставка не може перевищувати баланс!";
            return;
        }

        // Знімаємо ставку
        BalanceManager.Instance.SubtractBalance(currentBet);

        betAmountText.text = $"Депозит: {currentBet}";
        gameOver = false;
        gameStarted = true;
        statusText.text = "";
        takeMoneyButton.gameObject.SetActive(true);

        if (gridManager != null)
            gridManager.RegenerateGrid();
    }

    public void CellClicked(Cell cell)
    {
        if (gameOver || !gameStarted || cell == null) return;

        if (cell.hasMine)
        {
            currentBet = 0;
            betAmountText.text = "Депозит: 0";
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

        if (BalanceManager.Instance != null)
        {
            BalanceManager.Instance.AddBalance(currentBet);
        }

        currentBet = 0;
        betAmountText.text = "Депозит: 0";
        statusText.text = "? Ви забрали гроші!";
        takeMoneyButton.gameObject.SetActive(false);

        gameOver = true;
        gameStarted = false;

        if (gridManager != null)
            gridManager.DisableAllCells();
    }

    void GameOver(bool win)
    {
        gameOver = true;
        gameStarted = false;

        takeMoneyButton.gameObject.SetActive(false);

        statusText.text = win
            ? "?? Ви виграли!"
            : "?? Програш! Ви втратили ставку.";

        if (gridManager != null)
            gridManager.RevealAllCells();
    }
}
