using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class AutoLogin : MonoBehaviour
{
    void Start()
    {
        string accessToken = PlayerPrefs.GetString("accessToken", null);
        string refreshToken = PlayerPrefs.GetString("refreshToken", null);

        if (!string.IsNullOrEmpty(accessToken))
        {
            StartCoroutine(CheckAccessToken(accessToken, refreshToken));
        }
        else
        {
            SceneManager.LoadScene("Step1"); // лог≥н-сцена
        }
    }

    IEnumerator CheckAccessToken(string token, string refreshToken)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:7777/check-token");
        request.SetRequestHeader("Authorization", "Bearer " + token);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // accessToken OK Ч переходимо в гру
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            // accessToken нед≥йсний, пробуЇмо оновити
            StartCoroutine(RefreshAccessToken(refreshToken));
        }
    }

    IEnumerator RefreshAccessToken(string refreshToken)
    {
        string url = "http://localhost:7777/refresh-token";
        string json = JsonUtility.ToJson(new RefreshRequest(refreshToken));

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            LoginPanel.LoginResponse loginData = JsonUtility.FromJson<LoginPanel.LoginResponse>(request.downloadHandler.text);

            PlayerPrefs.SetString("accessToken", loginData.accessToken);
            PlayerPrefs.SetString("refreshToken", loginData.refreshToken);
            PlayerPrefs.SetString("userEmail", loginData.email);
            PlayerPrefs.SetString("nick", loginData.nick);
            PlayerPrefs.SetInt("balance", loginData.balance);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainScene");
        }
        else
        {
            PlayerPrefs.DeleteAll(); // очищаЇмо все
            SceneManager.LoadScene("Step1"); // повертаЇмось на лог≥н
        }
    }


    [System.Serializable]
    public class RefreshRequest
    {
        public string refreshToken;
        public RefreshRequest(string token)
        {
            refreshToken = token;
        }
    }
}
