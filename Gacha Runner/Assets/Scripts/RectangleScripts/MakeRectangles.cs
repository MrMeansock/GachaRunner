using UnityEngine;

public class MakeRectangles : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Rectangle rectanglePrefab = null;
    [SerializeField] RectanglePreview previewPrefab = null;

    [Header("Parameters")]
    [Tooltip("Time (in seconds) until a rectangle despawns when created.")]
    [SerializeField] float rectanglesLifespan = 5.0f;

    // Dependencies
    private TouchHandler touchHandler;

    // Tool logic variables
    private RectanglePreview activePreview;

    private void Awake()
    {
        touchHandler = FindObjectOfType<TouchHandler>();
    }

    private void OnEnable()
    {
        touchHandler.OnFirstTouchStart += StartRectangle;
        touchHandler.OnFirstTouchMoved += UpdateRectangle;
        touchHandler.OnFirstTouchEnd += EndRectangle;
    }

    private void OnDisable()
    {
        touchHandler.OnFirstTouchStart -= StartRectangle;
        touchHandler.OnFirstTouchMoved -= UpdateRectangle;
        touchHandler.OnFirstTouchEnd -= EndRectangle;
    }

    void StartRectangle(Touch touch)
    {
        activePreview = RectanglePreview.MakePreview(previewPrefab, MainCamera.ScreenToWorldPoint(touch.position)); 
    }

    void UpdateRectangle(Touch touch, Vector2 deltaPosition)
    {
        activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touch.position);
    }

    void EndRectangle(Touch touch)
    {
        activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touch.position);
        Rectangle.MakeRectangle(rectanglePrefab, activePreview, rectanglesLifespan);
        Destroy(activePreview.gameObject);
        activePreview = null;
    }
}
