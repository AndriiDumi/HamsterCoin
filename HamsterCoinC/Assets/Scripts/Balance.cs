using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager Instance;
    public TMP_Text balanceText;

    private int balance;
    private string jwtToken;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Отримуємо токен та баланс з PlayerPrefs
            jwtToken = PlayerPrefs.GetString("accessToken", "");
            balance = PlayerPrefs.GetInt("balance", 0);

            UpdateBalanceText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateBalanceText()
    {
        if (balanceText != null)
            balanceText.text = "Баланс: " + balance.ToString();
    }

    public int GetBalance() => balance;

    public void AddBalance(int amount)
    {
        balance += amount;
        SaveAndRefresh();
    }

    public void SubtractBalance(int amount)
    {
        balance -= amount;
        SaveAndRefresh();
    }

    private void SaveAndRefresh()
    {
        PlayerPrefs.SetInt("balance", balance);
        PlayerPrefs.Save();

        UpdateBalanceText();
        StartCoroutine(RefreshBalanceOnServer());
    }

    private IEnumerator RefreshBalanceOnServer()
    {
        var requestData = new RefreshBalanceRequest
        {
            balance = balance,
            jwToken = jwtToken
        };

        string json = JsonUtility.ToJson(requestData);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("http://localhost:7777/users/refresh-balance", "PUT");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Баланс успішно оновлено на сервері");
        }
        else
        {
            Debug.LogError("Помилка оновлення балансу: " + request.error);
            Debug.LogError("Статус код: " + request.responseCode);
            Debug.LogError("Відповідь сервера: " + request.downloadHandler.text);
        }
    }

    [System.Serializable]
    private class RefreshBalanceRequest
    {
        public int balance;
        public string jwToken;
    }
}
