using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetHappinessStat : MonoBehaviour
{
    [SerializeField] [Tooltip("Текущее счастье")] 
    private int happiness = 100;
    
    [SerializeField] [Tooltip("Максимум")] 
    private int maxHappiness = 100;
    
    [SerializeField] [Tooltip("Минимум")] 
    private int minHappiness = 0;

    [SerializeField] [Tooltip("Как часто будет уменьшаться счастье (в секундах)")] 
    private float decayInterval = 5f;
    
    [SerializeField] [Tooltip("На сколько будет уменьшаться за раз")] 
    private int happinessDecay = 1;

    [SerializeField] 
    private Slider happinessBar;
    
    private void Start()
    {
        // Запускаем нашу логику "скуки"
        // Coroutine - это как отдельный процесс, который может "ждать"
        StartCoroutine(DecayHappiness());
    }

    private void Update()
    {
        // Постоянно обновляем значение слайдера, чтобы оно = нашему счастью
        if (happinessBar != null)
        {
            happinessBar.value = happiness;
        }
    }

    // Это Корутина (Coroutine). Она будет работать "параллельно"
    // Ее задача - отсчитать 5 секунд, уменьшить счастье, и снова отсчитать 5 секунд
    private IEnumerator DecayHappiness()
    {
        // Бесконечный цикл, который работает, пока живет питомец
        while (true)
        {
            // 1. Ждем N секунд
            yield return new WaitForSeconds(decayInterval);

            // 2. Уменьшаем счастье
            happiness -= happinessDecay;

            // 3. Проверяем, чтобы счастье не ушло ниже минимума
            if (happiness < minHappiness)
            {
                happiness = minHappiness;
            }

            // 4. (Для теста) Выводим в консоль текущее счастье
            Debug.Log("Happiness is now: " + happiness);
        }
    }

    // --- Публичные функции для КНОПОК ---
    // Этот метод мы будем вызывать по нажатию кнопки "Поиграть"

    public void PlayWithPet()
    {
        happiness += 10; // Даем +10 счастья

        // Проверяем, чтобы счастье не ушло выше максимума
        if (happiness > maxHappiness)
        {
            happiness = maxHappiness;
        }

        Debug.Log("Played with pet! Happiness: " + happiness);
    }
}