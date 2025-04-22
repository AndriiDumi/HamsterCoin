using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Step1_Email : MonoBehaviour
{
    public TMP_InputField emailInput;
    public Button continueButton;
    public TMP_Text messageText;

    private RegistrationManager regManager;

    void Start()
    {
        regManager = RegistrationManager.Instance;
        continueButton.onClick.AddListener(SaveAndContinue);
    }

    void SaveAndContinue()
    {
        string email = emailInput.text.Trim();

        if (!IsValidEmail(email))
        {
            messageText.text = "Невірний email!";
            return;
        }

        regManager.SaveEmail(email);
        regManager.ShowStep2();
    }

    bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
