using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Назва сцени, на яку переходимо
    public string sceneName;

    // Цей метод можна викликати з кнопки
    public void SwitchScene()
    {

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Mines");
        }
    }
}
