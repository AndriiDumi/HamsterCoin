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
        public string nick;
        public int balance;
        public string email;
        public string accessToken;
        public string refreshToken;
    }

    void Start()
    {
        // Якщо токени вже є — автоматичний вхід
        string accessToken = PlayerPrefs.GetString("accessToken", "");
        string refreshToken = PlayerPrefs.GetString("refreshToken", "");

        if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            Debug.Log("Знайдені токени. Вхід без авторизації.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        // Прив’язка кнопок
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
            onSuccess: (response) =>
            {
                Debug.Log("Login response: " + response);

                try
                {
                    LoginResponse loginData = JsonUtility.FromJson<LoginResponse>(response);

                    PlayerPrefs.SetString("accessToken", loginData.accessToken);
                    PlayerPrefs.SetString("refreshToken", loginData.refreshToken);
                    PlayerPrefs.SetString("userEmail", loginData.email);
                    PlayerPrefs.SetString("nick", loginData.nick);
                    PlayerPrefs.SetInt("balance", loginData.balance);

                    PlayerPrefs.Save();

                    messageText.text = "Успішний вхід!";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                }
                catch (Exception ex)
                {
                    messageText.text = "Помилка обробки відповіді: " + ex.Message;
                }
            },
            onError: (error) =>
            {
                messageText.text = "Помилка входу: " + error;
            }
        ));
    }

    void OnRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step2");
    }
}
