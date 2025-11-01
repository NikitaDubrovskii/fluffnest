using UnityEngine;

public class PetHealthStat : PetStatBase
{
    public PetStatBase happiness;
    public PetStatBase hunger;

    // Расчет: -1 / 3 секунды = ~ -0.3333 за секунду
    private const float TestDecayPerSecond = -0.3333f;

    protected override float CalculateDecayRate(float deltaTime)
    {
        // ФЛАГ ТЕСТИРОВАНИЯ: -1 за 3 секунды
        if (isDefaultDecayEnabled)
        {
            return TestDecayPerSecond; 
        }

        // РЕЖИМ ЗАВИСИМОСТИ (Уменьшение только при критических условиях)
        float decay = 0f;

        // 1. Ускорение от Критического Голода: Голод < 10
        if (hunger.CurrentValue < 0.1f * hunger.MaxValue)
        {
            decay += -0.007f; // Самое быстрое падение (до нуля за ~4 часа)
        }

        // 2. Ускорение от Критического Несчастья: Счастье < 15
        if (happiness.CurrentValue < 0.15f * happiness.MaxValue)
        {
            decay += -0.006f; // Быстрое падение (до нуля за ~4.6 часа)
        }

        return decay;
    }

    // --- Публичные функции для КНОПОК ---
    public void HealPet()
    {
        IncreaseStat(30f); // +30% здоровья
    }
}