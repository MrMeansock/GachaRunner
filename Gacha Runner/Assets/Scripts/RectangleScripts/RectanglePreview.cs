using UnityEngine;
using System;

public class RectanglePreview : MonoBehaviour
{
    private Vector2 startPosition;
    public Vector2 StartPosition {
        get => startPosition;
        set
        {
            startPosition = value;
            OnResize?.Invoke();
        }
    }

    private Vector2 endPosition;
    public Vector2 EndPosition {
        get => endPosition;
        set
        {
            endPosition = value;
            OnResize?.Invoke();
        }
    }

    private event Action OnResize;

    private void OnEnable()
    {
        OnResize += UpdatePosition;
        OnResize += UpdateRotation;
        OnResize += UpdateScale;
    }

    private void OnDisable()
    {
        OnResize -= UpdatePosition;
        OnResize -= UpdateRotation;
        OnResize -= UpdateScale;
    }

    private void UpdatePosition()
    {
        Vector2 midpoint = (EndPosition - StartPosition) * 0.5f;
        transform.position = StartPosition + midpoint;
    }

    private void UpdateScale()
    {
        Vector2 vecTo = EndPosition - StartPosition; // Vector from start to end
        transform.localScale = new Vector3(vecTo.magnitude, transform.localScale.y, transform.localScale.z);
    }

    private void UpdateRotation()
    {
        float angleBetween = Vector2.SignedAngle(EndPosition - StartPosition, Vector2.right);
        transform.rotation = Quaternion.Euler(0, 0, -angleBetween);
    }

    public static RectanglePreview MakePreview(RectanglePreview previewPrefab, Vector3 startPosition)
    {
        RectanglePreview preview = Instantiate(previewPrefab, startPosition, Quaternion.identity);
        preview.StartPosition = startPosition;
        preview.EndPosition = startPosition;
        return preview;
    }
}