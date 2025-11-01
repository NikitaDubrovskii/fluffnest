using UnityEngine;

public class PetHappinessStat : PetStatBase
{
    public PetStatBase health;
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

        // РЕЖИМ ЗАВИСИМОСТИ (Уменьшение только при плохих условиях)
        float decay = 0f;
        
        // 1. Ускорение от Голода: Голод < 20
        if (hunger.CurrentValue < 0.2f * hunger.MaxValue) 
        {
            decay += -0.003f; // Медленное падение (до нуля за ~9.2 часа)
        }

        // 2. Ускорение от Здоровья: Здоровье < 30
        if (health.CurrentValue < 0.3f * health.MaxValue)
        {
            decay += -0.005f; // Более быстрое падение (до нуля за ~5.5 часа)
        }
        
        return decay;
    }

    // --- Публичные функции для КНОПОК ---
    public void PlayWithPet()
    {
        IncreaseStat(25f); // +25% счастья
    }
}