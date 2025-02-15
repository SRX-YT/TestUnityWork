using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton pattern
    public static PlayerManager Instance;

    // TODO: сделать геттеры сеттеры и поставить приват, не успеваю по срокам
    public bool isHoldItem = false;
    public GameObject currentItem;

    [Header("Dependency")]
    [SerializeField] private CharacterController playerCharacterController;
    [SerializeField] private Transform itemHoldTransform;

    [Header("Settings")]
    [Range(0, 10)]
    [SerializeField] private float playerSpeed;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerMove(Vector2 directionVector)
    {
        float z = directionVector.y;
        float x = directionVector.x;

        Vector3 axis = transform.right * x + transform.forward * z;

        playerCharacterController.Move(axis * Time.deltaTime * playerSpeed);
    }

    public Transform GetItemHoldTransform()
    {
        return itemHoldTransform;
    }
}
