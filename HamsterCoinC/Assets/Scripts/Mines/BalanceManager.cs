using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance;

    private const string BalanceKey = "TestBalance";

    public delegate void BalanceChanged(float newBalance);
    public static event BalanceChanged OnBalanceChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            float savedBalance = PlayerPrefs.GetFloat(BalanceKey, -1f);
            if (savedBalance < 0f)
            {
                Debug.Log("[BalanceManager] Ѕаланс не знайдено Ч встановлюЇмо стартовий.");
                SetBalance(1000f);
            }
            else
            {
                Debug.Log("[BalanceManager] «бережений баланс: " + savedBalance);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetBalance()
    {
        return PlayerPrefs.GetFloat(BalanceKey, 0f);
    }

    public void SetBalance(float amount)
    {
        float clampedAmount = Mathf.Max(0f, amount);
        PlayerPrefs.SetFloat(BalanceKey, clampedAmount);
        PlayerPrefs.Save();
        OnBalanceChanged?.Invoke(clampedAmount);
    }

    public void AddBalance(float amount)
    {
        SetBalance(GetBalance() + amount);
    }

    public void SubtractBalance(float amount)
    {
        SetBalance(GetBalance() - amount);
    }

    public void ClearBalance()
    {
        PlayerPrefs.DeleteKey(BalanceKey);
        PlayerPrefs.Save();
        OnBalanceChanged?.Invoke(0f);
    }

    public void ResetBalance()
    {
        SetBalance(1000f);
    }
}
