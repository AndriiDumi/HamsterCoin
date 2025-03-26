using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Step2_UserData : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField promoCodeInput;
    public TMP_InputField dateOfBirthInput;
    public Button continueButton;
    public Button backButton;
    public TMP_Text messageText;

    private RegistrationManager regManager;

    void Start()
    {
        regManager = RegistrationManager.Instance;
        continueButton.onClick.AddListener(SaveAndContinue);
        backButton.onClick.AddListener(() => regManager.ShowStep1());
    }

    void SaveAndContinue()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;
        string promoCode = promoCodeInput.text.Trim();
        string birthDate = dateOfBirthInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(birthDate))
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

        regManager.SaveUserData(username, password, promoCode, birthDate);
        regManager.ShowStep3();
    }
}
