using UnityEngine;
using UnityEngine.UI;

public abstract class PetStatBase : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Максимальное значение характеристики")]
    [SerializeField] private float maxValue = 100f;

    [Header("Текущие значения")]
    [SerializeField] protected float currentValue = 100f;

    [Header("UI Ссылка")]
    public Slider statBar;

    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;

    protected virtual void Start()
    {
        UpdateStatBarValue();
    }

    protected abstract float CalculateDecayRate(float deltaTime, bool isTestMode);

    /// Обновляет значение характеристики с учетом времени и режима
    public void StatUpdate(float totalPassedTime, bool isTestMode)
    {
        float decayRatePerSecond = CalculateDecayRate(totalPassedTime, isTestMode);
        float totalChange = decayRatePerSecond * totalPassedTime;

        currentValue = Mathf.Clamp(currentValue + totalChange, 0f, maxValue);

        if (totalChange < 0)
        {
            OnStatDecayed();
        }

        UpdateStatBarValue();
    }

    /// Увеличивает значение характеристики на указанную величину
    protected void IncreaseStat(float amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, 0f, maxValue);
        OnStatIncreased();
        UpdateStatBarValue();
    }

    private void UpdateStatBarValue()
    {
        if (statBar != null)
        {
            statBar.value = currentValue;
        }
    }

    protected virtual void OnStatDecayed() { }
    protected virtual void OnStatIncreased() { }
}