using UnityEngine;
using System;
using System.Collections;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance;
    public static event Action<float> OnBalanceChanged;

    private float currentBalance = 0f;
    private string accessToken = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAccessToken(string token)
    {
        accessToken = token;
        Debug.Log("[BalanceManager] Токен збережено");
    }

    public void SetInitialBalance(float balance)
    {
        currentBalance = balance;
        Debug.Log("[BalanceManager] Початковий баланс встановлено: " + balance);
        OnBalanceChanged?.Invoke(currentBalance);
    }

    public float GetBalance()
    {
        return currentBalance;
    }

    public void AddBalance(float amount)
    {
        SetBalance(currentBalance + amount);
    }

    public void SubtractBalance(float amount)
    {
        SetBalance(currentBalance - amount);
    }

    public void SetBalance(float newBalance)
    {
        float clampedBalance = Mathf.Max(0f, newBalance);
        StartCoroutine(UpdateBalanceOnServer(clampedBalance));
    }
    public void RefreshBalance()
    {
        StartCoroutine(RefreshBalanceFromServer());
    }

    private IEnumerator RefreshBalanceFromServer()
    {
        yield return APIController.RefreshBalancePUT(accessToken, currentBalance,
            onSuccess: (updatedBalance) =>
            {
                currentBalance = updatedBalance;
                Debug.Log("[BalanceManager] Баланс оновлено з сервера: " + updatedBalance);
                OnBalanceChanged?.Invoke(currentBalance);
            },
            onError: (error) =>
            {
                Debug.LogError("[BalanceManager] Помилка при оновленні з сервера: " + error);
            });
    }

    private IEnumerator UpdateBalanceOnServer(float newBalance)
    {
        yield return APIController.RefreshBalancePUT(accessToken, newBalance,
            onSuccess: (updatedBalance) =>
            {
                currentBalance = updatedBalance;
                Debug.Log("[BalanceManager] Баланс оновлено: " + updatedBalance);
                OnBalanceChanged?.Invoke(currentBalance);
            },
            onError: (error) =>
            {
                Debug.LogError("[BalanceManager] Помилка при оновленні: " + error);
            });
    }
}
