using UnityEngine;

public class PetHungerStat : PetStatBase
{
    // Расчет: -100 / 28800 секунд (3 раза кушать) = ~ -0.003472 за секунду
    private const float BaseDecayPerSecond = -0.003472f;
    
    // Расчет: -1 / 3 секунды = ~ -0.3333 за секунду
    private const float TestDecayPerSecond = -0.3333f;

    protected override float CalculateDecayRate(float deltaTime)
    {
        // ФЛАГ ТЕСТИРОВАНИЯ: -1 за 3 секунды
        if (isDefaultDecayEnabled)
        {
            return TestDecayPerSecond; 
        }

        // РЕЖИМ РЕАЛЬНОГО ВРЕМЕНИ: Голод падает до нуля за 24 часа
        return BaseDecayPerSecond;
    }

    // --- Публичные функции для КНОПОК ---
    public void FeedPet()
    {
        IncreaseStat(40f); // Восстанавливаем 40% от Голода за раз
    }
}