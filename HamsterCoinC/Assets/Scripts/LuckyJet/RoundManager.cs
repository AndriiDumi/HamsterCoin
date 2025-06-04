using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float coefficient = 1f;
    public float maxCoefficient;
    public float coefficientGrowthRate = 0.1f;
    public float roundCooldown = 3f;

    public RocketAnimationUI rocketAnimation;

    public bool roundActive = false;

    public GameManager gameManager;
    public BetManager betManager;

    private float cooldownTimer = 0f;

    [Header("Loading UI")]
    public GameObject loadingPanel; // UI-панель з крутилкою

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        loadingPanel.SetActive(false);
    }

    void Update()
    {
        if (roundActive)
        {
            coefficient += coefficientGrowthRate * Time.deltaTime;

            if (coefficient >= maxCoefficient)
            {
                EndRound();
            }
        }
        else if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                // Закінчуємо завантаження, починаємо раунд
                loadingPanel.SetActive(false);
                rocketAnimation.LaunchRocket(); // Анімація ракети
                betManager.FixBet(); // Фіксуємо ставку
                gameManager.UpdateUI();

                roundActive = true;
            }
        }
    }

    public void StartRound()
    {
        maxCoefficient = Random.Range(3f, 5f);
        coefficient = 1f;
        roundActive = false;
        cooldownTimer = roundCooldown;

        betManager.AllowBet(); // Дозволити ставку
        betManager.ResetBet(); // Скинути попередню ставку

        gameManager.UpdateUI();

        // Показати екран завантаження і сховати анімацію
        loadingPanel.SetActive(true);
        rocketAnimation.StopRocket();
    }

    public void EndRound()
    {
        roundActive = false;
        rocketAnimation.StopRocket();
        betManager.BlockBet();

        // Починаємо наступний раунд (екран завантаження)
        StartRound();
    }

    public float GetCooldownTime() => cooldownTimer;
    public bool IsCooldownPhase() => !roundActive && cooldownTimer > 0;
}

