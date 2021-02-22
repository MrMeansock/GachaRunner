using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        [RequireComponent(typeof(GameObjectPool))]
        public class RectanglePreviewPool : MonoBehaviour
        {
            GameObjectPool pool;

            private void Awake()
            {
                pool = GetComponent<GameObjectPool>();
            }

            /// <summary>
            /// Returns next available rectangle preview and sets it to active.
            /// Returns null if none can be sent .
            /// </summary>
            /// <returns>(Possibly) Rectangle preview fetched from pool.</returns>
            public RectanglePreview GetNextAvailable(Vector3 startPosition)
            {
                GameObject previewObject = pool.GetNextAvailable();

                if (previewObject)
                {
                    RectanglePreview previewScript = previewObject.GetComponent<RectanglePreview>();

                    if (previewScript)
                    {
                        previewScript.Init(startPosition);
                        return previewScript;
                    }
                }

                return null;
            }

            /// <summary>
            /// Returns a requested rectangle preview to the pool.
            /// Will not take rectangle previews that weren't initially in the pool.
            /// </summary>
            /// <param name="gameObject">Object returned.</param>
            public virtual void ReturnToPool(RectanglePreview preview) => pool.ReturnToPool(preview.gameObject);
        }
    }
}