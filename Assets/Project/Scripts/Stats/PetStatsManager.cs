using System.Collections;
using UnityEngine;

public class PetStatsManager : MonoBehaviour
{
    [Header("Настройки Оптимизации")]
    [Tooltip("Интервал обновления характеристик в секундах")]
    [SerializeField] private float updateIntervalSeconds = 1.0f;

    [Tooltip("Включить ли режим быстрого тестирования")]
    [SerializeField] private bool isDefaultDecayEnabled = false;

    [Header("Ссылки на характеристики")]
    [SerializeField] private PetStatBase happiness;
    [SerializeField] private PetStatBase hunger;
    [SerializeField] private PetStatBase health;

    private void Start()
    {
        StartCoroutine(StatUpdateLoop());
    }

    /// Цикл обновления характеристик с заданным интервалом
    private IEnumerator StatUpdateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateIntervalSeconds);
            UpdateAllStats(updateIntervalSeconds);
        }
    }

    /// Обновляет все характеристики питомца
    private void UpdateAllStats(float passedTime)
    {
        if (hunger != null) hunger.StatUpdate(passedTime, isDefaultDecayEnabled);
        if (happiness != null) happiness.StatUpdate(passedTime, isDefaultDecayEnabled);
        if (health != null) health.StatUpdate(passedTime, isDefaultDecayEnabled);
    }
}