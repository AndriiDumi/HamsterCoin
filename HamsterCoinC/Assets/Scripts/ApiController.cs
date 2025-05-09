using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class APIController : MonoBehaviour
{
    private static string registerUrl = "http://localhost:7777/users/registration";
    private static string loginUrl = "http://localhost:7777/login";

    // 📩 Реєстрація
    public static IEnumerator RegisterUser(string mail, string password, string nickname, string promocode, string birthDate, Action<string> onSuccess, Action<string> onError)
    {
        var userData = new RegisterRequest
        {
            mail = mail,
            password = password,
            nickname = nickname,
            promocode = promocode,
            birthDate = birthDate // передається як рядок
        };

        yield return SendRequest(registerUrl, userData, onSuccess, onError);
    }

    // 🔐 Авторизація
    public static IEnumerator LoginUser(string mail, string password, Action<string> onSuccess, Action<string> onError)
    {
        var loginData = new LoginRequest
        {
            mail = mail,
            password = password
        };

        yield return SendRequest(loginUrl, loginData, onSuccess, onError);
    }

    // 📦 Універсальний метод запиту
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

    [Serializable]
    private class RegisterRequest
    {
        public string mail;
        public string password;
        public string nickname;
        public string promocode;
        public string birthDate; // важливо: рядок!
    }

    [Serializable]
    private class LoginRequest
    {
        public string mail;
        public string password;
    }
}
