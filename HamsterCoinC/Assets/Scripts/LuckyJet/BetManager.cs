using UnityEngine;

public class BetManager : MonoBehaviour
{
    private float betAmount = 0f;
    private bool betPlaced = false;

    public RoundManager roundManager;

    public void PlaceBet(float amount)
    {
        if (!roundManager.roundActive || betPlaced) return;

        betAmount = amount;
        betPlaced = true;

        Debug.Log("Ставка зроблена: " + betAmount);
    }

    public void CashOut()
    {
        if (roundManager.roundActive && betPlaced)
        {
            float winnings = betAmount * roundManager.coefficient;
            Debug.Log("Гравець забрав ставку! Виграш: " + winnings);

            // Не завершуємо раунд після забрання ставки, лише очищаємо ставку
            ResetBet();
        }
    }

    public float GetCurrentBetValue()
    {
        return betPlaced ? betAmount * roundManager.coefficient : 0f;
    }

    public void ResetBet()
    {
        betAmount = 0f;
        betPlaced = false;
    }
}
