using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoundManager roundManager;
    public UIManager uiManager;

    void Start()
    {
        roundManager.StartRound(); // ������ �����
    }

    public void UpdateUI()
    {
        uiManager.UpdateUI();
    }

    public void StartRound()
    {
        roundManager.StartRound();
    }
}
