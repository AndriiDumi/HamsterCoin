using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class APIController : MonoBehaviour
{
    private static string registerUrl = "http://localhost:7777/users/registration";
    private static string loginUrl = "http://localhost:7777/login";
    private static string balanceUrl = "http://localhost:7777/users/refresh-balance";

    // --- Реєстрація користувача ---
    public static IEnumerator RegisterUser(string mail, string password, string nickname, string promocode, string birthDate,
                                           Action<string> onSuccess, Action<string> onError)
    {
        var userData = new RegisterRequest
        {
            mail = mail,
            password = password,
            nickname = nickname,
            promocode = promocode,
            birthDate = birthDate
        };

        yield return SendRequest(registerUrl, userData, onSuccess, onError);
    }

    // --- Вхід користувача з токеном і балансом ---
    public static IEnumerator LoginUser(string mail, string password,
                                        Action<string, float> onSuccess, Action<string> onError)
    {
        var loginData = new LoginRequest
        {
            mail = mail,
            password = password
        };

        string jsonData = JsonUtility.ToJson(loginData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                onSuccess?.Invoke(loginResponse.accessToken, loginResponse.balance);
            }
            catch (Exception ex)
            {
                onError?.Invoke("Помилка парсингу логіну: " + ex.Message);
            }
        }
        else
        {
            onError?.Invoke(request.error + "\n" + request.downloadHandler.text);
        }
    }

    // --- Оновлення балансу (PUT) ---
    public static IEnumerator RefreshBalancePUT(string accessToken, float newBalance,
                                    Action<float> onSuccess, Action<string> onError)
    {
        var balanceData = new BalanceRequest
        {
            balance = newBalance,
            jwTtoken = accessToken  // додаємо токен у тіло запиту
        };
        string jsonData = JsonUtility.ToJson(balanceData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(balanceUrl, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + accessToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Відповідь сервера RefreshBalancePUT: '" + responseText + "'");

            if (string.IsNullOrEmpty(responseText))
            {
                // Відповідь порожня, але запит успішний — вважаємо, що оновлення балансу успішне
                onSuccess?.Invoke(newBalance); // повертаємо той баланс, який відправляли
                yield break;
            }

            try
            {
                BalanceResponse balanceResp = JsonUtility.FromJson<BalanceResponse>(responseText);
                if (balanceResp == null)
                {
                    onError?.Invoke("Баланс не отримано: відповідь не відповідає формату.");
                    yield break;
                }
                onSuccess?.Invoke(balanceResp.balance);
            }
            catch (Exception ex)
            {
                onError?.Invoke("Помилка парсингу балансу: " + ex.Message);
            }
        }
        else
        {
            onError?.Invoke(request.error + "\n" + request.downloadHandler.text);
        }


    }



    // --- Загальний POST-запит (тільки для Register) ---
    private static IEnumerator SendRequest<T>(string url, T data, Action<string> onSuccess, Action<string> onError)
    {
        string jsonData = JsonUtility.ToJson(data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(request.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(request.error + "\n" + request.downloadHandler.text);
        }
    }

    // --- DTO ---
    [Serializable]
    private class RegisterRequest
    {
        public string mail;
        public string password;
        public string nickname;
        public string promocode;
        public string birthDate;
    }

    [Serializable]
    private class LoginRequest
    {
        public string mail;
        public string password;
    }

    [Serializable]
    private class LoginResponse
    {
        public string accessToken;
        public float balance;
    }

    [Serializable]
    private class BalanceRequest
    {
        public float balance;
        public string jwTtoken;
    }


    [Serializable]
    private class BalanceResponse
    {
        public float balance;
    }
}
