using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private float moveSpeed = 80f;
    [SerializeField] private float scaleUp = 1.5f;
    [SerializeField] private float scaleDuration = 0.2f;

    private TextMeshProUGUI text;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Color startColor;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        startColor = text.color;
    }

    /// Инициализирует всплывающий текст с позицией и запускает анимацию
    public void Initialize(string message, Vector2 canvasLocalPosition, Canvas canvas)
    {
        text.text = message;
        rectTransform.anchoredPosition = canvasLocalPosition + Random.insideUnitCircle * 30f;
        rectTransform.localScale = originalScale * scaleUp;

        StartCoroutine(Animate());
    }

    /// Анимирует движение и затухание текста
    private IEnumerator Animate()
    {
        float timer = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;

        StartCoroutine(ScaleDown());

        while (timer < lifetime)
        {
            timer += Time.deltaTime;
            rectTransform.anchoredPosition = startPos + Vector2.up * moveSpeed * timer;

            float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }

    /// Плавно уменьшает масштаб до оригинального размера
    private IEnumerator ScaleDown()
    {
        float t = 0f;
        Vector3 startScale = rectTransform.localScale;

        while (t < scaleDuration)
        {
            t += Time.deltaTime;
            float progress = t / scaleDuration;
            rectTransform.localScale = Vector3.Lerp(startScale, originalScale, progress);
            yield return null;
        }
    }
}