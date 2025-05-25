using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("Scene Names")]
    public string nextScene;
    public string previousScene = "MainMenu"; // �� �������������

    [Header("Buttons")]
    public Button continueBtn;
    public Button backBtn;
    public Button quitBtn;

    [Header("Animation")]
    public Animator screenFade;
    public float fadeTime = 0.5f;

    void Start()
    {
        // ϳ������� ������
        if (continueBtn != null)
            continueBtn.onClick.AddListener(() => LoadScene(nextScene));

        if (backBtn != null)
            backBtn.onClick.AddListener(() => LoadScene(previousScene));

        if (quitBtn != null)
            quitBtn.onClick.AddListener(QuitGame);
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty!");
            return;
        }

        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        // ��������� ������� ����������
        if (screenFade != null)
        {
            screenFade.SetTrigger("FadeOut");
            yield return new WaitForSeconds(fadeTime);
        }

        // ����������� �����
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}