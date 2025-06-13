using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public TMP_Text messageText;

    void Start()
    {
        string accessToken = PlayerPrefs.GetString("jwtToken", "");
        string refreshToken = PlayerPrefs.GetString("refreshToken", "");

        if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            Debug.Log("Токени знайдені, переходимо до MainMenu без логіну.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

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
            onSuccess: (accessToken, balance) =>
            {
                Debug.Log("Успішний вхід. Токен: " + accessToken + " | Баланс: " + balance);

                // Зберігаємо у PlayerPrefs
                PlayerPrefs.SetString("jwtToken", accessToken);
                PlayerPrefs.SetFloat("balance", balance);
                PlayerPrefs.SetString("userEmail", email);
                PlayerPrefs.Save();

                // Передаємо у BalanceManager
                if (BalanceManager.Instance != null)
                {
                    BalanceManager.Instance.SetAccessToken(accessToken);
                    BalanceManager.Instance.SetInitialBalance(balance);
                }

                messageText.text = "Успішний вхід!";
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            },
            onError: (error) =>
            {
                Debug.LogError("Помилка входу: " + error);
                messageText.text = "Помилка входу: " + error;
            }
        ));
    }

    void OnRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step2");
    }
}
