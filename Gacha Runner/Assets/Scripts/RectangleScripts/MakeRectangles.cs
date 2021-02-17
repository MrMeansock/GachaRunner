using System.Collections;
using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        public class MakeRectangles : MonoBehaviour
        {
            [Header("Prefabs")]

            [Header("Parameters")]
            [Tooltip("Time (in seconds) until a rectangle despawns when created.")]
            [SerializeField] float rectanglesLifespan = 5.0f;

            // Dependencies
            private TouchHandler touchHandler;
            private RectanglePreviewPool previewPool;
            private RectanglePool rectanglePool;

            // Tool logic variables
            private RectanglePreview activePreview;

            private void Awake()
            {
                if (!touchHandler) touchHandler = FindObjectOfType<TouchHandler>();
                if (!previewPool) previewPool = FindObjectOfType<RectanglePreviewPool>();
                if (!rectanglePool) rectanglePool = FindObjectOfType<RectanglePool>();
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

            void StartPreview(Touch touch)
            {
                activePreview = previewPool.GetNextAvailable(MainCamera.ScreenToWorldPoint(touch.position));
            }

            void UpdatePreview(Touch touch, Vector2 deltaPosition)
            {
                activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touch.position);
            }

            void EndPreview(Touch touch)
            {
                activePreview.EndPosition = MainCamera.ScreenToWorldPoint(touch.position);
                Rectangle rectangle = rectanglePool.GetNextAvailable(activePreview);
                StartCoroutine(ReturnRectangle(rectangle));
                previewPool.ReturnToPool(activePreview);
                activePreview = null;
            }

            IEnumerator ReturnRectangle(Rectangle rectangle)
            {
                yield return new WaitForSeconds(rectanglesLifespan);
                rectanglePool.ReturnToPool(rectangle);
            }
        }
    }
}