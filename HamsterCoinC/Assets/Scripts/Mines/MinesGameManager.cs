using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MinesGameManager : MonoBehaviour
{
    [Header("Кнопки ставок")]
    public Button maxBetButton;
    public Button minBetButton;
    public Button multiplyBetButton;
    public Button divideBetButton;

    [Header("Менеджер сітки")]
    public GridManager gridManager;

    [Header("UI Елементи")]
    public TMP_Text statusText;
    public TMP_Text balanceText;
    public TMP_Text currentBetText;
    public TMP_InputField betInput;
    public Button startGameButton;
    public Button takeMoneyButton;

    [Header("Налаштування гри")]
    public float multiplier = 1.2f;

    private bool gameOver = true;
    private bool gameStarted = false;
    private float currentBet = 0f;

    private void OnEnable()
    {
        if (BalanceManager.Instance != null)
            BalanceManager.OnBalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        if (BalanceManager.Instance != null)
            BalanceManager.OnBalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        if (gridManager != null)
            gridManager.gameManager = this;

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

        // Оновити баланс із сервера при старті гри
        if (BalanceManager.Instance != null)
            BalanceManager.Instance.RefreshBalance();

        StartCoroutine(DelayedUIUpdate());
    }

    private IEnumerator DelayedUIUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateUI();
    }

    private void OnBalanceChanged(float newBalance)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (BalanceManager.Instance == null)
        {
            Debug.LogError("BalanceManager.Instance не ініціалізований.");
            return;
        }

        float balance = BalanceManager.Instance.GetBalance();
        balanceText.text = balance.ToString("F2");
        currentBetText.text = currentBet.ToString("F2");

        if (!gameStarted)
            statusText.text = "Введіть ставку та натисніть Почати";
    }

    public void StartGame()
    {
        if (!float.TryParse(betInput.text.Replace(',', '.'), out currentBet) || currentBet <= 0f)
        {
            statusText.text = "Введіть коректну ставку!";
            return;
        }

        float balance = BalanceManager.Instance.GetBalance();

        if (currentBet > balance)
        {
            statusText.text = "Ставка перевищує баланс!";
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
            currentBet = 0f;
            GameOver(false);
        }
        else
        {
            currentBet = Mathf.Ceil(currentBet * multiplier * 100f) / 100f; // округлення до 0.01
        }

        UpdateUI();
    }

    public void TakeMoney()
    {
        if (gameOver || !gameStarted) return;

        BalanceManager.Instance.AddBalance(currentBet);

        currentBet = 0f;
        statusText.text = "Ви забрали гроші!";
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

        statusText.text = win ? "Ви виграли!" : "Програш! Ви втратили ставку.";

        if (gridManager != null)
            gridManager.RevealAllCells();

        UpdateUI();
    }

    private void SetMaxBet()
    {
        if (BalanceManager.Instance != null)
        {
            float maxBet = BalanceManager.Instance.GetBalance();
            betInput.text = maxBet.ToString("F2");
        }
    }

    private void SetMinBet()
    {
        betInput.text = "0.01";
    }

    private void MultiplyBet()
    {
        if (float.TryParse(betInput.text.Replace(',', '.'), out float bet) && BalanceManager.Instance != null)
        {
            float newBet = Mathf.Min(bet * 2f, BalanceManager.Instance.GetBalance());
            betInput.text = newBet.ToString("F2");
        }
    }

    private void DivideBet()
    {
        if (float.TryParse(betInput.text.Replace(',', '.'), out float bet))
        {
            float newBet = Mathf.Max(0.01f, bet / 2f);
            betInput.text = newBet.ToString("F2");
        }
    }
}
