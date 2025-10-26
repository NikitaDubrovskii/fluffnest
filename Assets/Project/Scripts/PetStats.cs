using UnityEngine;
using System;

public class PetStats : MonoBehaviour
{
    [Header("Текущие значения (0-100)")]
    [SerializeField] private float hunger = 100f;
    [SerializeField] private float happiness = 100f;
    [SerializeField] private float sleep = 100f;
    
    [Header("Скорость снижения (единиц в секунду)")]
    [SerializeField] private float hungerDecreaseRate = 5f;
    [SerializeField] private float happinessDecreaseRate = 3f;
    [SerializeField] private float sleepDecreaseRate = 4f;
    
    [Header("Критические уровни")]
    [SerializeField] private float criticalLevel = 20f;
    
    // Свойства для доступа из других скриптов
    public float Hunger => hunger;
    public float Happiness => happiness;
    public float Sleep => sleep;
    
    // События
    public event Action<float> OnHungerChanged;
    public event Action<float> OnHappinessChanged;
    public event Action<float> OnSleepChanged;
    public event Action<string> OnCriticalState;
    
    private DateTime lastUpdateTime;
    
    void Start()
    {
        lastUpdateTime = DateTime.Now;
        InvokeRepeating(nameof(UpdateStats), 1f, 1f);
    }
    
    void UpdateStats()
    {
        DateTime currentTime = DateTime.Now;
        float deltaSeconds = (float)(currentTime - lastUpdateTime).TotalSeconds;
        lastUpdateTime = currentTime;
        
        ChangeHunger(-hungerDecreaseRate * deltaSeconds);
        ChangeHappiness(-happinessDecreaseRate * deltaSeconds);
        ChangeSleep(-sleepDecreaseRate * deltaSeconds);
        
        CheckCriticalStates();
    }
    
    public void ChangeHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0f, 100f);
        OnHungerChanged?.Invoke(hunger);
    }
    
    public void ChangeHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0f, 100f);
        OnHappinessChanged?.Invoke(happiness);
    }
    
    public void ChangeSleep(float amount)
    {
        sleep = Mathf.Clamp(sleep + amount, 0f, 100f);
        OnSleepChanged?.Invoke(sleep);
    }
    
    void CheckCriticalStates()
    {
        if (hunger < criticalLevel)
            OnCriticalState?.Invoke("Питомец очень голоден! 🍔");
        
        if (happiness < criticalLevel)
            OnCriticalState?.Invoke("Питомец грустный! 😢");
        
        if (sleep < criticalLevel)
            OnCriticalState?.Invoke("Питомец хочет спать! 😴");
    }
    
    public void Feed(float amount = 30f)
    {
        ChangeHunger(amount);
    }
    
    public void Play(float amount = 25f)
    {
        ChangeHappiness(amount);
    }
    
    public void PutToSleep(float amount = 40f)
    {
        ChangeSleep(amount);
    }
}