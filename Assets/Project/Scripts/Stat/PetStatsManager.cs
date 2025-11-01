using System.Collections;
using UnityEngine;

public class PetStatsManager : MonoBehaviour
{
    [Header("Настройки Оптимизации")]
    [Tooltip("Интервал обновления характеристик в секундах. Рекомендуется 1.0f.")]
    public float updateIntervalSeconds = 1.0f;
    
    [Header("Ссылки на характеристики")]
    public PetStatBase happiness;
    public PetStatBase hunger;
    public PetStatBase health;
    
    void Start()
    {
        // Запускаем корутину для регулярного обновления
        StartCoroutine(StatUpdateLoop());
    }
    
    private IEnumerator StatUpdateLoop()
    {
        // Бесконечный цикл, который выполняется с задержкой
        while (true)
        {
            // Ждем указанный интервал
            yield return new WaitForSeconds(updateIntervalSeconds); 

            // Время, которое "прошло" для расчета, равно интервалу
            float passedTime = updateIntervalSeconds;
            
            // Обновляем все характеристики (за раз!)
            UpdateAllStats(passedTime);
        }
    }

    private void UpdateAllStats(float passedTime)
    {
        // Обновление Голода и Счастья (зависимости)
        if (hunger != null) hunger.StatUpdate(passedTime);
        if (happiness != null) happiness.StatUpdate(passedTime);
        
        // Обновление Здоровья
        if (health != null) health.StatUpdate(passedTime);
        
        // Debug.Log("Stats updated for " + passedTime + " seconds.");
    }
}