using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // ����� �����, �� ��� ����������
    public string sceneName;

    // ��� ����� ����� ��������� � ������
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
