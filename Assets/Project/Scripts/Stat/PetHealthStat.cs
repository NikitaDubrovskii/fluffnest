using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetHealthStat : MonoBehaviour
{
    [SerializeField] [Tooltip("Текущее здоровье")] 
    private int health = 100;
    
    [SerializeField] [Tooltip("Максимум")] 
    private int maxHealth = 100;
    
    [SerializeField] [Tooltip("Минимум")] 
    private int minHealth = 0;

    [SerializeField] [Tooltip("Как часто будет уменьшаться здоровье (в секундах)")] 
    private float decayInterval = 5f;
    
    [SerializeField] [Tooltip("На сколько будет уменьшаться за раз")] 
    private int healthDecay = 1;

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
            healthBar.value = health;
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
            health -= healthDecay;

            // 3. Проверяем, чтобы счастье не ушло ниже минимума
            if (health < minHealth)
            {
                health = minHealth;
            }

            // 4. (Для теста) Выводим в консоль текущее счастье
            Debug.Log("Happiness is now: " + health);
        }
    }

    // --- Публичные функции для КНОПОК ---
    // Этот метод мы будем вызывать по нажатию кнопки "полечить"

    public void CureAPet()
    {
        health += 10; // Даем +10 здоровья

        // Проверяем, чтобы здоровье не ушло выше максимума
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        Debug.Log("Cure a pet! Health: " + health);
    }
}