using UnityEngine;

public class UIHandler : MonoBehaviour
{
    // Singleton pattern
    public static UIHandler Instance;

    [Header("Dependency")]
    [SerializeField] private GameObject dropButton;

    private void Awake()
    {
        Instance = this;
    }

    /*
     * Включение, отключение кнопки выброса предмета
     * Также его выброс
     */

    public void OnDropButton()
    {
        dropButton.SetActive(true);
    }

    public void OffDropButton()
    {
        dropButton.SetActive(false);
    }

    public void UseDropButton()
    {
        PlayerManager.Instance.currentItem.GetComponent<Item>().StopInteract();
        PlayerManager.Instance.isHoldItem = false;
    }
}