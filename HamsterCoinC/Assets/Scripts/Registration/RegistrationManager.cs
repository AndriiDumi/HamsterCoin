using UnityEngine;

public class RegistrationManager : MonoBehaviour
{
    private static RegistrationManager _instance;
    public static RegistrationManager Instance => _instance;

    private string email;
    private string username;
    private string password;
    private string promoCode;
    private string birthDate;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void SaveEmail(string email)
    {
        this.email = email;
    }

    public void SaveUserData(string username, string password, string promoCode, string birthDate)
    {
        this.username = username;
        this.password = password;
        this.promoCode = promoCode;
        this.birthDate = birthDate;
    }

    public UserData GetUserData()
    {
        return new UserData(email, username, password, promoCode, birthDate);
    }

    public void ShowStep1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step1");
    }

    public void ShowStep2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step2");
    }

    public void ShowStep3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Step3");
    }
}
