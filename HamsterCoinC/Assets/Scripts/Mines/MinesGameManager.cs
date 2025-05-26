using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MinesGameManager : MonoBehaviour
{
    [Header("�������� ����")]
    public GridManager gridManager;

    [Header("UI ��������")]
    public TMP_Text statusText;
    public TMP_Text betAmountText;
    public TMP_InputField betInput;
    public Button takeMoneyButton;
    public Button startGameButton;

    [Header("������������ ���")]
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

        betAmountText.text = "�������: 0";
        statusText.text = "������ ������ �� �������� ������";

        if (gridManager != null)
            gridManager.DisableAllCells();
    }

    public void StartGame()
    {
        string token = PlayerPrefs.GetString("jwtToken");
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("? JWT ����� �� ��������. ������� �� ����� �����.");
            SceneManager.LoadScene("step1");
            return;
        }

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
            statusText.text = "? ������ �� ���� ������������ ������!";
            return;
        }

        // ������ ������
        BalanceManager.Instance.SubtractBalance(currentBet);

        betAmountText.text = $"�������: {currentBet}";
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
            betAmountText.text = "�������: 0";
            GameOver(false);
        }
        else
        {
            currentBet = Mathf.CeilToInt(currentBet * multiplier);
            betAmountText.text = $"�������: {currentBet}";
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
        betAmountText.text = "�������: 0";
        statusText.text = "? �� ������� �����!";
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
            ? "?? �� �������!"
            : "?? �������! �� �������� ������.";

        if (gridManager != null)
            gridManager.RevealAllCells();
    }
}
