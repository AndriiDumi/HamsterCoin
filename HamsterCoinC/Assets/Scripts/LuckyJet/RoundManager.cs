using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float coefficient = 1f;
    public float maxCoefficient;
    public float coefficientGrowthRate = 0.1f;
    public float roundCooldown = 3f; // Час до наступного раунду
    public float betTime = 3f; // Час для ставок на початку кожного раунду

    public bool roundActive = false;
    public bool betTimeActive = false; // Час для ставок активний чи ні

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
            // Якщо час для ставок активний
            if (betTimeActive)
            {
                betTimer -= Time.deltaTime;
                if (betTimer <= 0)
                {
                    betTimeActive = false; // Закінчився час на ставку
                    StartRoundGame(); // Почати саму гру
                }
            }
            else
            {
                // Нараховуємо коефіцієнт
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
                gameManager.StartRound(); // Після закінчення часу на відновлення, запускаємо новий раунд
            }
        }
    }

    public void StartRound()
    {
        maxCoefficient = Random.Range(3f, 5f);
        coefficient = 1f;
        roundActive = true;
        betTimeActive = true; // Починається час для ставок
        betTimer = betTime; // Встановлюємо таймер для ставок
        cooldownTimer = roundCooldown; // Встановлюємо таймер для наступного раунду
        gameManager.UpdateUI();
    }

    // Початок гри після ставок
    public void StartRoundGame()
    {
        betTimeActive = false; // Закінчився час на ставку
        // Можна додати додаткові дії для початку гри, наприклад, оновити UI або розпочати збільшення коефіцієнта
    }

    public void EndRound()
    {
        roundActive = false;
        gameManager.UpdateUI();
        betManager.ResetBet(); // Скидаємо ставку після завершення раунду
        // Запускаємо новий раунд лише після досягнення максимального коефіцієнта
        if (cooldownTimer <= 0)
        {
            gameManager.StartRound(); // Запускаємо новий раунд
        }
    }

    public float GetCooldownTime()
    {
        return cooldownTimer;
    }
}
