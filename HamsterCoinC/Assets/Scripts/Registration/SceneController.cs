using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // ������ ��� �������� �� �������� �����
    public Button continueButton;

    // ����� ����� ���������� �����
    public string nextSceneName;

    void Start()
    {
        // ϳ��������� ������� �� ������
        continueButton.onClick.AddListener(OnContinueClick);
    }

    // ������� ��� �������� �� �������� �����
    public void OnContinueClick()
    {
        // ������������ �����
        SceneManager.LoadScene(nextSceneName);
    }
}
