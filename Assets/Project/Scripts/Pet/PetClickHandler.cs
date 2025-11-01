using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PetClickHandler : MonoBehaviour
{
    [SerializeField] private PetHappinessStat petHappinessStat;

    // Сопоставление "Имя сцены → действие"
    private Dictionary<string, IPetClickAction> sceneActions;
    private InputAction _clickAction;

    private void Awake()
    {
        // Инициализация словаря действий
        sceneActions = new Dictionary<string, IPetClickAction>
        {
            { "MainScene", new PlayWithPetAction(petHappinessStat) }
        };
    }
    
    private void OnEnable()
    {
        // Действие, которое реагирует и на клик, и на тап
        _clickAction = new InputAction(type: InputActionType.PassThrough);

        // Привязка к кнопке мыши
        _clickAction.AddBinding("<Mouse>/leftButton");

        // Привязка к первому касанию на экране
        _clickAction.AddBinding("<Touchscreen>/primaryTouch/tap");

        // Подписываемся на событие
        _clickAction.performed += OnClick;
        _clickAction.Enable();
    }

    private void OnDisable()
    {
        _clickAction.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        string scene = SceneManager.GetActiveScene().name;
        if (!sceneActions.ContainsKey(scene))
        {
            Debug.Log($"Нет действия для сцены {scene}");
            return;
        }

        Vector2 inputPosition;

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            inputPosition = Mouse.current.position.ReadValue();
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        else
            return;

        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Выполняем действие, соответствующее сцене
                sceneActions[scene].OnPetClicked(gameObject);
            }
        }
    }
    
}