using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinesGameManager : MonoBehaviour
{
    public GridManager gridManager;
    public TMP_Text statusText;
    public Button restartButton;

    private bool gameOver = false;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        restartButton.gameObject.SetActive(false);
    }

    public void CellClicked(Cell cell)
    {
        if (gameOver) return;

        if (cell.hasMine)
        {
            GameOver(false);
        }
        else
        {
            // ���� ����� ������ �������� ���� ��� ��������� ����� � ���� ����������.
        }
    }

    void GameOver(bool win)
    {
        gameOver = true;
        statusText.text = win ? "�� �������!" : "�������! �� ���� ���.";
        restartButton.gameObject.SetActive(true);
        gridManager.RevealAllCells();
    }

    public void RestartGame()
    {
        gameOver = false;
        statusText.text = "";
        restartButton.gameObject.SetActive(false);
        gridManager.RegenerateGrid();
    }
}
