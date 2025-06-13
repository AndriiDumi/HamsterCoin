using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Globalization;

public class Step2_UserData : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField promoCodeInput;
    public TMP_InputField dateOfBirthInput; // формат yyyy-MM-dd
    public Button continueButton;
    public Button backButton;
    public TMP_Text messageText;

    void Start()
    {
        continueButton.onClick.AddListener(SaveAndContinue);
        backButton.onClick.AddListener(BackToStep1);
    }

    void SaveAndContinue()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;
        string promoCode = promoCodeInput.text.Trim();
        string birthDateString = dateOfBirthInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(birthDateString))
        {
            messageText.text = "Заповніть усі поля!";
            return;
        }

        if (password.Length < 6)
        {
            messageText.text = "Пароль має бути мінімум 6 символів!";
            return;
        }

        if (password != confirmPassword)
        {
            messageText.text = "Паролі не співпадають!";
            return;
        }

        string[] formats = { "yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd" };

        if (!DateTime.TryParseExact(birthDateString.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
        {
            messageText.text = $"Невірний формат дати: '{birthDateString}'. Приклад: 2000-01-01";
            return;
        }

        string formattedBirthDate = birthDate.ToString("yyyy-MM-dd");
        string email = username;  // якщо username — це email

        StartCoroutine(APIController.RegisterUser(
            email,
            password,
            username,
            promoCode,
            formattedBirthDate,
            onSuccess: (response) =>
            {
                messageText.text = "Реєстрація успішна!";
                PlayerPrefs.SetString("userEmail", email);
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetString("promoCode", promoCode);
                PlayerPrefs.SetString("birthDate", formattedBirthDate);
                PlayerPrefs.Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene("step1");
            },
            onError: (error) =>
            {
                messageText.text = "Помилка: " + error;
            }
        ));
    }

    void BackToStep1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("step1");
    }
}
