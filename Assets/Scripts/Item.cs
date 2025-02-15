using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isHold = false;
    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Interact()
    {
        rb.freezeRotation = true;
        col.isTrigger = true;
        isHold = true;

        UIHandler.Instance.OnDropButton();
    }

    public void StopInteract()
    {
        isHold = false;

        gameObject.transform.position = Camera.main.transform.position;
        rb.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse);

        rb.freezeRotation = false;
        col.isTrigger = false;

        UIHandler.Instance.OffDropButton();
    }

    private void Update()
    {
        // Обновление позиции предмета чтобы игрок "держал" предмет
        if (isHold)
        {
            gameObject.transform.position = PlayerManager.Instance.GetItemHoldTransform().position;
        }
    }
}
