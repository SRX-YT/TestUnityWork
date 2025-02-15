using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Singleton pattern
    public static InputHandler Instance;

    [Header("Dependency")]
    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private RectTransform joystickArea;
    [SerializeField] private Camera mainCamera;

    [Header("Settings")]
    [SerializeField] private LayerMask interactableLayers;

    private Touch touch;
    private bool isTouchOnInteractableObject = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckJoystickInput();
        CheckCameraInput();
    }

    private void CheckJoystickInput()
    {
        if (movementJoystick.Direction != Vector2.zero)
        {
            Vector2 direction = movementJoystick.Direction;
            PlayerManager.Instance.PlayerMove(direction);
        }
    }

    private void CheckCameraInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!IsTouchInJoystickArea(touch.position))
                {
                    isTouchOnInteractableObject = IsTouchOnInteractableObject(touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!IsTouchInJoystickArea(touch.position))
                {
                    float rotationX = touch.deltaPosition.x;
                    float rotationY = touch.deltaPosition.y;
                    CameraManager.Instance.RotatePlayer(rotationX);
                    CameraManager.Instance.RotateCamera(rotationY);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (isTouchOnInteractableObject)
                {
                    InteractWithObject(touch.position);
                }

                isTouchOnInteractableObject = false;
            }
        }
    }

    /*
     * Проверка находится ли палец игрока в зоне джойстика
     * для того чтобы камера не вращалась при ходьбе
     */
    private bool IsTouchInJoystickArea(Vector2 touchPosition)
    {
        Vector3[] corners = new Vector3[4];
        joystickArea.GetWorldCorners(corners);
        return touchPosition.x >= corners[0].x && touchPosition.x <= corners[2].x &&
               touchPosition.y >= corners[0].y && touchPosition.y <= corners[1].y;
    }

    /*
     * Для кратковременного отслеживания нажал ли игрок на предмет
     */
    private bool IsTouchOnInteractableObject(Vector2 touchPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayers))
        {
            return true;
        }

        return false;
    }

    private void InteractWithObject(Vector2 touchPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20f, interactableLayers))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                hit.transform.GetComponent<Door>().Interact();
            }
            else if (hit.collider.gameObject.tag == "Item")
            {
                if (!PlayerManager.Instance.isHoldItem)
                {
                    PlayerManager.Instance.currentItem = hit.transform.gameObject;
                    hit.transform.GetComponent<Item>().Interact();
                }
            }
        }
    }
}
