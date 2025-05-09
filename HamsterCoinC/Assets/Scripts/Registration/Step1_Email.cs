using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public TMP_Text messageText;

    [Serializable]
    public class LoginResponse
    {
        public string accessToken;
        public string refreshToken;
    }

    void Start()
    {
        loginButton.onClick.AddListener(OnLogin);
        registerButton.onClick.AddListener(OnRegister);
    }

    void OnLogin()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            messageText.text = "Введіть email та пароль!";
            return;
        }

        StartCoroutine(APIController.LoginUser(
            mail: email,
            password: password,
            onSuccess: (response) => {
                Debug.Log("Login response: " + response);

                try
                {
                    LoginResponse loginData = JsonUtility.FromJson<LoginResponse>(response);
                    PlayerPrefs.SetString("accessToken", loginData.accessToken);
                    PlayerPrefs.SetString("refreshToken", loginData.refreshToken);
                    PlayerPrefs.SetString("userEmail", email);

                    messageText.text = "Успішний вхід!";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                }
                catch (Exception ex)
                {
                    messageText.text = "Помилка обробки відповіді: " + ex.Message;
                }
            },
            onError: (error) => {
                messageText.text = "Помилка входу: " + error;
            }
        ));
    }

    void OnRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step2");
    }
}
