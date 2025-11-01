using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PetStatBase : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Максимальное значение характеристики")]
    public float maxValue = 100f;
    
    [Tooltip("Включить ли дефолтное уменьшение для быстрого тестирования")]
    public bool isDefaultDecayEnabled = false;
    
    [Header("Текущие значения")]
    [SerializeField] protected float currentValue = 100f;

    // Вводим свойство, чтобы дочерние классы могли получать Голод и Здоровье
    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;
    
    
    
    /*[SerializeField] [Tooltip("Текущее значение характеристики")]
    protected int currentValue = 100;

    [SerializeField] [Tooltip("Максимум")]
    protected int maxValue = 100;

    [SerializeField] [Tooltip("Минимум")]
    protected int minValue = 0;

    [SerializeField] [Tooltip("Как часто будет уменьшаться значение (в секундах)")]
    protected float decayInterval = 5f;

    [SerializeField] [Tooltip("На сколько будет уменьшаться за раз")]
    protected int decayAmount = 1;*/

    [Header("UI Ссылка")]
    // Ссылка на компонент Slider для визуализации
    public Slider statBar;

    protected virtual void Start()
    {
        // currentValue = maxValue;
        UpdateStatBarValue();
    }
    
    /*protected virtual void Start()
    {
        UpdateStatBarValue();
        // Запускаем логику уменьшения характеристики
        StartCoroutine(DecayStat());
    }*/
    
    protected abstract float CalculateDecayRate(float deltaTime);
    
    // --- Общая логика изменения (для игрового цикла) ---
    public void StatUpdate(float totalPassedTime)
    {
        // 1. Рассчитываем итоговую скорость уменьшения (это Rate per Second)
        float decayRatePerSecond = CalculateDecayRate(totalPassedTime);

        // 2. Рассчитываем итоговое изменение за интервал
        float totalChange = decayRatePerSecond * totalPassedTime;

        // 3. Применяем изменение
        currentValue += totalChange;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);

        // 4. Логирование/Уведомление
        if (totalChange < 0)
        {
            OnStatDecayed();
        }
        
        // 5. Обновление UI
        UpdateStatBarValue();
    }

    // Общий метод для увеличения характеристики (кормление, игры)
    public void IncreaseStat(float amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, 0f, maxValue);
        OnStatIncreased();
        UpdateStatBarValue();
    }

    // Заглушки для дочерних классов
    protected virtual void OnStatDecayed() {}
    protected virtual void OnStatIncreased() {}

    private void UpdateStatBarValue()
    {
        if (statBar != null)
        {
            statBar.value = currentValue;
        }
    }

    /*// Корутина для уменьшения характеристики со временем
    private IEnumerator DecayStat()
    {
        while (true)
        {
            // 1. Ждем N секунд
            yield return new WaitForSeconds(decayInterval);

            // 2. Уменьшаем значение
            currentValue -= decayAmount;

            // 3. Проверяем, чтобы не ушло ниже минимума
            if (currentValue < minValue)
            {
                currentValue = minValue;
            }

            UpdateStatBarValue();
            // 4. Выводим в консоль текущее значение
            OnStatDecayed();
        }
    }

    // Метод для увеличения характеристики
    protected void IncreaseStat(int amount)
    {
        currentValue += amount;

        // Проверяем, чтобы не ушло выше максимума
        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }

        UpdateStatBarValue();

        OnStatIncreased();
    }

    // Абстрактные методы для логирования (переопределяются в наследниках)
    protected abstract void OnStatDecayed();
    protected abstract void OnStatIncreased();*/
}
