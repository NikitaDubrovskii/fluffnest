using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private int pointsPerClick = 1;

    private Canvas uiCanvas;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        uiCanvas = FindObjectOfType<Canvas>();

        if (uiCanvas == null)
        {
            Debug.LogError("Canvas не найден! Добавь Canvas в сцену.");
        }
    }

    /// Обрабатывает клик по объекту и создает всплывающий текст
    public void OnPointerClick(PointerEventData eventData)
    {
        if (floatingTextPrefab == null || uiCanvas == null) return;

        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2 screenPoint = mainCamera.WorldToScreenPoint(hit.point);
            GameObject textObj = Instantiate(floatingTextPrefab, uiCanvas.transform);
            FloatingText floatingText = textObj.GetComponent<FloatingText>();
            floatingText.Initialize("+" + pointsPerClick, screenPoint, uiCanvas);
        }
    }
}