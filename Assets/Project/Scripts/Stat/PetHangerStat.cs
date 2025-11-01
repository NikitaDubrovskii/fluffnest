using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetHangerStat : MonoBehaviour
{
    [SerializeField] [Tooltip("Текущий голод")] 
    private int hanger = 100;
    
    [SerializeField] [Tooltip("Максимум")] 
    private int maxHanger = 100;
    
    [SerializeField] [Tooltip("Минимум")] 
    private int minHanger = 0;

    [SerializeField] [Tooltip("Как часто будет уменьшаться голод (в секундах)")] 
    private float decayInterval = 5f;
    
    [SerializeField] [Tooltip("На сколько будет уменьшаться за раз")] 
    private int hangerDecay = 1;

    [SerializeField] 
    private Slider healthBar;

    private void Start()
    {
        // Запускаем нашу логику "скуки"
        // Coroutine - это как отдельный процесс, который может "ждать"
        StartCoroutine(DecayHappiness());
    }

    private void Update()
    {
        // Постоянно обновляем значение слайдера, чтобы оно = нашему счастью
        if (healthBar != null)
        {
            healthBar.value = hanger;
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
            hanger -= hangerDecay;

            // 3. Проверяем, чтобы счастье не ушло ниже минимума
            if (hanger < minHanger)
            {
                hanger = minHanger;
            }

            // 4. (Для теста) Выводим в консоль текущее счастье
            Debug.Log("Happiness is now: " + hanger);
        }
    }

    // --- Публичные функции для КНОПОК ---
    // Этот метод мы будем вызывать по нажатию кнопки "полечить"

    public void CureAPet()
    {
        hanger += 10; // Даем +10 здоровья

        // Проверяем, чтобы здоровье не ушло выше максимума
        if (hanger > maxHanger)
        {
            hanger = maxHanger;
        }

        Debug.Log("Cure a pet! Health: " + hanger);
    }
}