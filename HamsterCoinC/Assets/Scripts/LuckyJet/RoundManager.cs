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
    public GameObject loadingPanel; // UI-������ � ���������

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
                // �������� ������������, �������� �����
                loadingPanel.SetActive(false);
                rocketAnimation.LaunchRocket(); // ������� ������
                betManager.FixBet(); // Գ����� ������
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

        betManager.AllowBet(); // ��������� ������
        betManager.ResetBet(); // ������� ��������� ������

        gameManager.UpdateUI();

        // �������� ����� ������������ � ������� �������
        loadingPanel.SetActive(true);
        rocketAnimation.StopRocket();
    }

    public void EndRound()
    {
        roundActive = false;
        rocketAnimation.StopRocket();
        betManager.BlockBet();

        // �������� ��������� ����� (����� ������������)
        StartRound();
    }

    public float GetCooldownTime() => cooldownTimer;
    public bool IsCooldownPhase() => !roundActive && cooldownTimer > 0;
}

