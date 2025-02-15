using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Singleton pattern
    public static CameraManager Instance;

    [Header("Dependency")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [Range(0, 10)]
    [SerializeField] private float cameraSensetivity;

    // Для ограничения вращения камеры через Mathf.Clamp
    private float xRotation;

    private void Awake()
    {
        Instance = this;
    }

    public void RotateCamera(float value)
    {
        float x = value * cameraSensetivity * Time.deltaTime;

        xRotation -= x;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void RotatePlayer(float value)
    {
        float y = value * cameraSensetivity * Time.deltaTime;
        playerTransform.Rotate(Vector3.up, y, Space.Self);
    }
}
