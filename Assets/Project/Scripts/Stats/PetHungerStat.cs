public class PetHungerStat : PetStatBase
{
    private const float BaseDecayPerSecond = -0.003472f;
    private const float TestDecayPerSecond = -0.3333f;
    private const float FeedAmount = 40f;

    /// Рассчитывает скорость уменьшения голода
    protected override float CalculateDecayRate(float deltaTime, bool isTestMode)
    {
        return isTestMode ? TestDecayPerSecond : BaseDecayPerSecond;
    }

    /// Кормит питомца, восстанавливая голод
    public void FeedPet()
    {
        IncreaseStat(FeedAmount);
    }
}