using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PetClickHandler : MonoBehaviour
{
    [Header("Статистика")]
    [SerializeField] private PetHappinessStat petHappinessStat;

    [Header("UI Элементы")]
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private int pointsPerClick = 1;

    [Header("Подсказка")]
    [SerializeField] private GameObject textPrefab;

    private GameObject currentHint;
    private bool hasBeenClicked = false;
    private Dictionary<string, IPetClickAction> sceneActions;
    private InputAction clickAction;

    /// Инициализирует словарь действий и создает подсказку
    private void Awake()
    {
        sceneActions = new Dictionary<string, IPetClickAction>
        {
            { "MainScene", new PlayWithPetAction(petHappinessStat) }
        };

        if (textPrefab != null && uiCanvas != null && !hasBeenClicked)
        {
            CreateHint();
        }
    }

    /// Настраивает обработку ввода для мыши и касаний
    private void OnEnable()
    {
        clickAction = new InputAction(type: InputActionType.PassThrough);
        clickAction.AddBinding("<Mouse>/leftButton");
        clickAction.AddBinding("<Touchscreen>/primaryTouch/tap");
        clickAction.performed += OnClick;
        clickAction.Enable();
    }

    private void OnDisable()
    {
        clickAction.Disable();
    }

    /// Обрабатывает клик по питомцу
    private void OnClick(InputAction.CallbackContext context)
    {
        string scene = SceneManager.GetActiveScene().name;
        if (!sceneActions.ContainsKey(scene)) return;

        Vector2 inputPosition = GetInputPosition();
        if (inputPosition == Vector2.zero) return;

        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            if (!hasBeenClicked && currentHint != null)
            {
                Destroy(currentHint);
                hasBeenClicked = true;
            }

            CreateFloatingText("+" + pointsPerClick, hit.point);
            sceneActions[scene].OnPetClicked(gameObject);
        }
    }

    /// Получает позицию ввода от мыши или касания
    private Vector2 GetInputPosition()
    {
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            return Mouse.current.position.ReadValue();
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            return Touchscreen.current.primaryTouch.position.ReadValue();
        return Vector2.zero;
    }

    /// Создает всплывающий текст в точке клика
    private void CreateFloatingText(string message, Vector3 worldHitPoint)
    {
        if (floatingTextPrefab == null || uiCanvas == null) return;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldHitPoint);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                uiCanvas.GetComponent<RectTransform>(), screenPoint, null, out Vector2 localPoint))
        {
            GameObject textObj = Instantiate(floatingTextPrefab, uiCanvas.transform);
            FloatingText floatingText = textObj.GetComponent<FloatingText>();
            floatingText?.Initialize(message, localPoint, uiCanvas);
        }
    }

    /// Создает подсказку над головой питомца
    private void CreateHint()
    {
        if (textPrefab == null || uiCanvas == null) return;

        currentHint = Instantiate(textPrefab, uiCanvas.transform);
        currentHint.name = "PetHint";

        TextMeshProUGUI tmp = currentHint.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = "Make me happy! Tap!";
            tmp.color = new Color(1f, 0.8f, 0.3f);
            tmp.fontSize = 28;
            tmp.alignment = TextAlignmentOptions.Center;
        }

        StartCoroutine(FollowPet());
        StartCoroutine(PulseHint());
    }

    /// Обновляет позицию подсказки для следования за питомцем
    private IEnumerator FollowPet()
    {
        RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
        RectTransform hintRect = currentHint.GetComponent<RectTransform>();

        if (uiCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogError("Canvas должен быть Screen Space - Overlay!");
        }

        Vector3 headOffset = new Vector3(0, 1.5f, 0);

        while (currentHint != null)
        {
            Vector3 worldPos = transform.position + headOffset;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect, screenPoint, null, out Vector2 localPoint))
            {
                hintRect.anchoredPosition = localPoint;
            }

            yield return null;
        }
    }

    /// Анимирует пульсацию подсказки
    private IEnumerator PulseHint()
    {
        RectTransform rect = currentHint.GetComponent<RectTransform>();
        Vector3 original = rect.localScale;

        while (currentHint != null)
        {
            float t = Mathf.Sin(Time.time * 3f) * 0.1f + 1f;
            rect.localScale = original * t;
            yield return null;
        }
    }
}