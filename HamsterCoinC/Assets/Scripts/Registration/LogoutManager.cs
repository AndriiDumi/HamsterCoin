using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class LogoutManager : MonoBehaviour
{
    public void OnLogoutClick()
    {
        string accessToken = PlayerPrefs.GetString("accessToken", "");

        if (!string.IsNullOrEmpty(accessToken))
        {
            StartCoroutine(SendLogoutRequest(accessToken));
        }
        else
        {
            Debug.Log("���� accessToken. ������ ������� ����.");
            ClearAndReturnToLogin();
        }
   

    }

    IEnumerator SendLogoutRequest(string token)
    {
        UnityWebRequest request = UnityWebRequest.PostWwwForm("http://localhost:7777/logout", "");
        request.SetRequestHeader("Authorization", "Bearer " + token);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("������� ����� � �������.");
        }
        else
        {
            Debug.LogWarning("������� ������: " + request.error);
        }

        ClearAndReturnToLogin();
    }

    void ClearAndReturnToLogin()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("step1"); // ���������� �� ����� �����
    }
}
