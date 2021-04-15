using System;
using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        public class MakeRectanglePreviews : MonoBehaviour
        {
            // Dependencies
            private TouchHandler touchHandler;
            private RectanglePreviewPool previewPool;

            [Header("Parameters")]
            [Tooltip("Minimum length a preview needs to be in order to create a rectangle.")]
            [SerializeField] float minLength = 2.5f;
            [Tooltip("Maximum length a preview can be to be in order to create a rectangle.")]
            [SerializeField] float maxLength = 8.5f;
            public float MaxLength
            {
                get
                {
                    return maxLength;
                }
                set
                {
                    maxLength = value;
                }
            }
            [Tooltip("Time (in seconds) a player has to wait to make the next rectangle.")]
            [SerializeField] float cooldown = 2.0f;
            public float Cooldown
            {
                get
                {
                    return cooldown;
                }
                set
                {
                    cooldown = value;
                }
            }

            [Tooltip("Color of previews that are too short and won't make a rectangle.")]
            [SerializeField] Color badPreviewColor = Color.gray;
            [Tooltip("Color of previews long enough to make a rectangle.")]
            [SerializeField] Color goodPreviewColor = Color.red;
            [Tooltip("Color of previews while the user cannot make rectangles yet.")]
            [SerializeField] Color cooldownColor = Color.red;

            // Tool logic variables
            private RectanglePreview activePreview;
            private float currentCooldown;

            public event Action<RectanglePreview> OnGoodPreviewEnd;

            private void Awake()
            {
                touchHandler = FindObjectOfType<TouchHandler>();
                previewPool = FindObjectOfType<RectanglePreviewPool>();
            }

            private void Update()
            {
                // Update cooldown time
                if (currentCooldown > 0)
                {
                    currentCooldown -= Time.deltaTime;
                }
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

                // Clamp length of preview
                if (activePreview.Length > maxLength)
                {
                    Vector2 direction = activePreview.EndPosition - activePreview.StartPosition;
                    direction.Normalize();
                    direction *= maxLength;
                    activePreview.EndPosition = activePreview.StartPosition + direction;
                }

                UpdateColor(activePreview);
            }

            void EndPreview(Vector2 touchPos)
            {
                // Invoke GoodPreview event if conditions met
                if (currentCooldown <= 0 && activePreview.Length >= minLength)
                {
                    OnGoodPreviewEnd?.Invoke(activePreview);
                    currentCooldown = cooldown;
                }

                previewPool.ReturnToPool(activePreview);
                activePreview = null;
            }

            void UpdateColor(RectanglePreview preview)
            {
                if (currentCooldown <= 0)
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
                else
                {
                    preview.spriteRenderer.color = cooldownColor;
                }
            }
        }
    }
}