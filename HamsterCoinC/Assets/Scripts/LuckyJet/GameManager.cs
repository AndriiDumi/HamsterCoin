using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoundManager roundManager;
    public UIManager uiManager;

    void Start()
    {
        // Запуск першого раунду
        roundManager.StartRound();
    }

    // Оновлення UI
    public void UpdateUI()
    {
        uiManager.UpdateUI();
    }

    // Початок нового раунду після завершення попереднього
    public void StartRound()
    {
        roundManager.StartRound(); // Запуск нового раунду
    }
}
