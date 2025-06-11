
using UnityEngine;
using UnityEngine.UI;

public class UserDropdownMenu : MonoBehaviour
{
    public GameObject menuPanel; // Посилання на вашу Panel
    public Button triggerButton; // Посилання на кнопку

    private void Start()
    {
        // Спочатку ховаємо меню
        menuPanel.SetActive(false);

        // Додаємо обробник події для кнопки
        triggerButton.onClick.AddListener(ToggleMenu);
    }

    public void ToggleMenu()
    {
        // Перемикаємо видимість меню
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}