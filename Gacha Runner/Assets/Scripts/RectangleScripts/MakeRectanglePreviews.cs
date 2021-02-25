using System;
using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        public class MakeRectanglePreviews : MonoBehaviour
        {
            [Header("Dependencies")]
            [SerializeField] TouchHandler touchHandler;
            [SerializeField] RectanglePreviewPool previewPool;

            [Header("Parameters")]
            [Tooltip("Minimum length a preview needs to be in order to create a rectangle.")]
            [SerializeField] float minLength = 2.5f;
            [Tooltip("Color of previews that are too short and won't make a rectangle.")]
            [SerializeField] Color badPreviewColor = Color.gray;
            [Tooltip("Color of previews long enough to make a rectangle.")]
            [SerializeField] Color goodPreviewColor = Color.red;

            // Tool logic variables
            private RectanglePreview activePreview;

            public event Action<RectanglePreview> OnGoodPreviewEnd;

            private void Awake()
            {
                if (!touchHandler) touchHandler = FindObjectOfType<TouchHandler>();
                if (!previewPool) previewPool = FindObjectOfType<RectanglePreviewPool>();
            }

            private void OnEnable()
            {
                touchHandler.OnFirstTouchStart += StartPreview;
                touchHandler.OnFirstTouchMoved += UpdatePreview;
                touchHandler.OnFirstTouchEnd += EndPreview;
            }

            private void OnDisable()
            {
                touchHandler.OnFirstTouchStart -= StartPreview;
                touchHandler.OnFirstTouchMoved -= UpdatePreview;
                touchHandler.OnFirstTouchEnd -= EndPreview;
            }

            void StartPreview(Vector2 touchPos)
            {
                activePreview = previewPool.GetNextAvailable(MainCamera.ScreenToWorldPoint(touchPos));
                UpdateColor(activePreview);
            }

            void UpdatePreview(Vector2 touchPos, Vector2 deltaPosition)
            {
                activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touchPos);
                UpdateColor(activePreview);
            }

            void EndPreview(Vector2 touchPos)
            {
                activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touchPos);
                UpdateColor(activePreview);

                // Invoke GoodPreview event if conditions met
                if (activePreview.Length >= minLength)
                {
                    OnGoodPreviewEnd?.Invoke(activePreview);
                }

                previewPool.ReturnToPool(activePreview);
                activePreview = null;
            }

            void UpdateColor(RectanglePreview preview)
            {
                if (preview.Length < minLength)
                {
                    preview.spriteRenderer.color = badPreviewColor;
                }
                else
                {
                    preview.spriteRenderer.color = goodPreviewColor;
                }
            }
        }
    }
}