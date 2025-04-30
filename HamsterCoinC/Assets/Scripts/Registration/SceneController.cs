using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Button continueButton;
    public Button backButton;  // Опціонально
    public string nextSceneName;
    public string previousSceneName; // Опціонально

    void Start()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClick);

        if (backButton != null && !string.IsNullOrEmpty(previousSceneName))
            backButton.onClick.AddListener(OnBackClick);
    }

    public void OnContinueClick()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }

    public void OnBackClick()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
            SceneManager.LoadScene(previousSceneName);
    }
}
