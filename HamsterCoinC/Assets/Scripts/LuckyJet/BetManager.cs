using UnityEngine;

public class BetManager : MonoBehaviour
{
    private float betAmount = 0f;
    private bool betPlaced = false;
    private bool betFixed = false;

    public RoundManager roundManager;
    public bool canBet = false;

    public void PlaceBet(float amount)
    {
        if (!roundManager.IsCooldownPhase() || betFixed)
            return;

        float balance = BalanceManager.Instance.GetBalance();
        if (amount > balance)
        {
            Debug.Log("Недостатньо коштів для ставки!");
            return;
        }

        BalanceManager.Instance.SubtractBalance(amount);
        betAmount = amount;
        betPlaced = true;

        Debug.Log("Ставка зроблена: " + betAmount);
    }

    public void FixBet()
    {
        betFixed = true;
        BlockBet();
    }

    public void CashOut()
    {
        if (roundManager.roundActive && betPlaced)
        {
            float winnings = betAmount * roundManager.coefficient;
            BalanceManager.Instance.AddBalance(winnings);

            Debug.Log("Кеш-аут! Виграш: " + winnings.ToString("F2"));
            ResetBet();
        }
    }

    public float GetCurrentBetValue()
    {
        return betPlaced ? betAmount * roundManager.coefficient : 0f;
    }

    public float GetRawBetAmount() => betPlaced ? betAmount : 0f;

    public void ResetBet()
    {
        betAmount = 0f;
        betPlaced = false;
        betFixed = false;
    }

    public void AllowBet()
    {
        canBet = true;
    }

    public void BlockBet()
    {
        canBet = false;
    }

    // Нові методи — лише розраховують значення, не ставлять ставку!
    public float GetMaxBetAmount()
    {
        return BalanceManager.Instance.GetBalance();
    }

    public float GetMinBetAmount()
    {
        return 1f; // або інше значення, яке ти вважаєш мінімальним
    }

    public float GetDoubleBetAmount(float currentInput)
    {
        return Mathf.Min(currentInput * 2f, BalanceManager.Instance.GetBalance());
    }

    public float GetHalveBetAmount(float currentInput)
    {
        return Mathf.Max(1f, currentInput / 2f);
    }
}
