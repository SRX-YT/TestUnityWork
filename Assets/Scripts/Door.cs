using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [Header("Dependency")]
    [SerializeField] private Transform doorTransform;

    [Header("Settings")]
    [Range(0, 10)]
    [SerializeField] private float duration;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;

    private bool isClosed = true;

    private void Open()
    {
        if (isLeft)
        {
            doorTransform.DORotate(new Vector3(0, 90, 0), duration);
        }
        else if (isRight)
        {
            doorTransform.DORotate(new Vector3(0, -90, 0), duration);
        }
        isClosed = false;
    }

    private void Close()
    {
        doorTransform.DORotate(new Vector3(0, 0, 0), duration);
        isClosed = true;
    }

    public void Interact()
    {
        if (isClosed) { Open(); }
        else { Close(); }
    }
}
