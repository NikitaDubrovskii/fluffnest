using UnityEngine;

public class PetHappinessStat : PetStatBase
{
    [SerializeField] private PetStatBase health;
    [SerializeField] private PetStatBase hunger;

    private const float TestDecayPerSecond = -0.3333f;
    private const float HungerDecayRate = -0.003f;
    private const float HealthDecayRate = -0.005f;
    private const float HungerThreshold = 0.2f;
    private const float HealthThreshold = 0.3f;

    /// Рассчитывает скорость изменения счастья в зависимости от здоровья и голода
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

        if (health.CurrentValue < HealthThreshold * health.MaxValue)
        {
            decay += HealthDecayRate;
        }

        return decay;
    }

    /// Увеличивает счастье питомца при игре
    public void PlayWithPet()
    {
        IncreaseStat(1f);
    }
}