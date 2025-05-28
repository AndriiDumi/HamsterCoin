using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance;

    private const string BalanceKey = "TestBalance";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey(BalanceKey))
            {
                SetBalance(1000); // Стартовий баланс для тесту
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetBalance()
    {
        return PlayerPrefs.GetInt(BalanceKey, 0);
    }

    public void SetBalance(int amount)
    {
        PlayerPrefs.SetInt(BalanceKey, amount);
        PlayerPrefs.Save();
    }

    public void AddBalance(int amount)
    {
        SetBalance(GetBalance() + amount);
    }

    public void SubtractBalance(int amount)
    {
        SetBalance(Mathf.Max(0, GetBalance() - amount));
    }

    public void ClearBalance()
    {
        PlayerPrefs.DeleteKey(BalanceKey);
        PlayerPrefs.Save();
    }

    public void ResetBalance()
    {
        SetBalance(1000); // або інше стартове значення
    }

}
