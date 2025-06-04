using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    public static ScreenSizeManager instance;

    private float screenHeight;
    private float screenWidth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateScreenSize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateScreenSize()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
    }

    public float GetScreenHeight()
    {
        return screenHeight;
    }

    public float GetScreenWidth()
    {
        return screenWidth;
    }

    private void Update()
    {
        // При зміні розміру екрану можна оновлювати значення
        UpdateScreenSize();
    }
}
