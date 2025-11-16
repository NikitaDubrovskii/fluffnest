using UnityEngine;

public class PetHealthStat : PetStatBase
{
    [SerializeField] private PetStatBase happiness;
    [SerializeField] private PetStatBase hunger;

    private const float TestDecayPerSecond = -0.3333f;
    private const float HungerDecayRate = -0.007f;
    private const float HappinessDecayRate = -0.006f;
    private const float HungerThreshold = 0.1f;
    private const float HappinessThreshold = 0.15f;
    private const float HealAmount = 30f;

    /// Рассчитывает скорость изменения здоровья в зависимости от счастья и голода
    protected override float CalculateDecayRate(float deltaTime, bool isTestMode)
    {
        if (isTestMode)
        {
            return TestDecayPerSecond;
        }

        float decay = 0f;

        if (hunger.CurrentValue < HungerThreshold * hunger.MaxValue)
        {
            decay += HungerDecayRate;
        }

        if (happiness.CurrentValue < HappinessThreshold * happiness.MaxValue)
        {
            decay += HappinessDecayRate;
        }

        return decay;
    }

    /// Лечит питомца, восстанавливая здоровье
    public void HealPet()
    {
        IncreaseStat(HealAmount);
    }
}