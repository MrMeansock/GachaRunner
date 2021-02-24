using System.Collections;
using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        public class MakeRectangles : MonoBehaviour
        {
            [Header("Dependencies")]
            [SerializeField] RectanglePool rectanglePool;
            [SerializeField] MakeRectanglePreviews previewMaker;

            [Header("Parameters")]
            [Tooltip("Time (in seconds) until a rectangle despawns when created.")]
            [SerializeField] float rectanglesLifespan = 5.0f;

            private void Awake()
            {
                if (!rectanglePool) rectanglePool = FindObjectOfType<RectanglePool>();
                if (!previewMaker) previewMaker = FindObjectOfType<MakeRectanglePreviews>();
            }

            private void OnEnable()
            {
                previewMaker.OnGoodPreviewEnd += MakeRectangle;
            }

            private void OnDisable()
            {
                previewMaker.OnGoodPreviewEnd -= MakeRectangle;
            }

            public void MakeRectangle(RectanglePreview preview)
            {
                Rectangle rectangle = rectanglePool.GetNextAvailable(preview);
                StartCoroutine(ReturnRectangle(rectangle));
            }

            IEnumerator ReturnRectangle(Rectangle rectangle)
            {
                yield return new WaitForSeconds(rectanglesLifespan);
                rectanglePool.ReturnToPool(rectangle);
            }
        }
    }
}