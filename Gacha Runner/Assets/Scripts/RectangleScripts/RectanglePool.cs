using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        [RequireComponent(typeof(GameObjectPool))]
        public class RectanglePool : MonoBehaviour
        {
            GameObjectPool pool;

            private void Awake()
            {
                pool = GetComponent<GameObjectPool>();
            }

            /// <summary>
            /// Returns next available rectangle and sets it to active.
            /// Returns null if none can be sent .
            /// </summary>
            /// <returns>(Possibly) Rectangle fetched from pool.</returns>
            public Rectangle GetNextAvailable(RectanglePreview preview)
            {
                GameObject rectangleObject = pool.GetNextAvailable();
                
                if (rectangleObject)
                {
                    Rectangle rectangleScript = rectangleObject.GetComponent<Rectangle>();

                    if (rectangleScript)
                    {
                        rectangleScript.Init(preview);
                        return rectangleScript;
                    }
                }

                return null;
            }

            /// <summary>
            /// Returns a requested rectangle to the pool.
            /// Will not take rectangles that weren't initially in the pool.
            /// </summary>
            /// <param name="gameObject">Object returned.</param>
            public virtual void ReturnToPool(Rectangle rectangle) => pool.ReturnToPool(rectangle.gameObject);
        }
    }
}