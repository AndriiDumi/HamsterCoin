using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Кнопка для переходу до наступної сцени
    public Button continueButton;

    // Назва сцени наступного кроку
    public string nextSceneName;

    void Start()
    {
        // Підключення функції до кнопки
        continueButton.onClick.AddListener(OnContinueClick);
    }

    // Функція для переходу на наступну сцену
    public void OnContinueClick()
    {
        // Завантаження сцени
        SceneManager.LoadScene(nextSceneName);
    }
}
