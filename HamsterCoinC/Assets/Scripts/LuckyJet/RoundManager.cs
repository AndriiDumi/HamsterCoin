using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float coefficient = 1f;
    public float maxCoefficient;
    public float coefficientGrowthRate = 0.1f;
    public float roundCooldown = 3f; // ��� �� ���������� ������
    public float betTime = 3f; // ��� ��� ������ �� ������� ������� ������

    public bool roundActive = false;
    public bool betTimeActive = false; // ��� ��� ������ �������� �� �

    public GameManager gameManager;
    public BetManager betManager;

    private float cooldownTimer = 0f;
    private float betTimer = 0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (roundActive)
        {
            // ���� ��� ��� ������ ��������
            if (betTimeActive)
            {
                betTimer -= Time.deltaTime;
                if (betTimer <= 0)
                {
                    betTimeActive = false; // ��������� ��� �� ������
                    StartRoundGame(); // ������ ���� ���
                }
            }
            else
            {
                // ���������� ����������
                coefficient += coefficientGrowthRate * Time.deltaTime;

                if (coefficient >= maxCoefficient)
                {
                    EndRound();
                }
            }
        }

        if (!roundActive && cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                gameManager.StartRound(); // ϳ��� ��������� ���� �� ����������, ��������� ����� �����
            }
        }
    }

    public void StartRound()
    {
        maxCoefficient = Random.Range(3f, 5f);
        coefficient = 1f;
        roundActive = true;
        betTimeActive = true; // ���������� ��� ��� ������
        betTimer = betTime; // ������������ ������ ��� ������
        cooldownTimer = roundCooldown; // ������������ ������ ��� ���������� ������
        gameManager.UpdateUI();
    }

    // ������� ��� ���� ������
    public void StartRoundGame()
    {
        betTimeActive = false; // ��������� ��� �� ������
        // ����� ������ �������� 䳿 ��� ������� ���, ���������, ������� UI ��� ��������� ��������� �����������
    }

    public void EndRound()
    {
        roundActive = false;
        gameManager.UpdateUI();
        betManager.ResetBet(); // ������� ������ ���� ���������� ������
        // ��������� ����� ����� ���� ���� ���������� ������������� �����������
        if (cooldownTimer <= 0)
        {
            gameManager.StartRound(); // ��������� ����� �����
        }
    }

    public float GetCooldownTime()
    {
        return cooldownTimer;
    }
}
