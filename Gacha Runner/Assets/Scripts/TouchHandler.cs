using UnityEngine;
using System;

/// <summary>
/// Helper class to make it easier to work with touch inputs.
/// Right now it only handles one touch input (the first finger that touches the screen)(FirstTouch property).
/// Extend as you like.
/// </summary>
public class TouchHandler : MonoBehaviour
{
    public bool Touching { get; private set; }

    // For use outside of TouchHandler
    /// <summary>
    /// Usage(On Update):
    /// if (touchHandler.Touching) {
    ///     touchHandler.FirstTouch.???; // Get any touch information .radius, .deltaTime, .pressure
    ///     Vector3 pos = touchHandler.WorldPosition;
    /// }
    /// 
    /// Or use the events below, you do whatever you want man...
    /// </summary>
    public Touch FirstTouch => Input.GetTouch(0); // First finger that touched the screen
    public Vector2 Position => FirstTouch.position; // Shorthand for first touch's position
    public Vector3 WorldPosition => MainCamera.ScreenToWorldPoint(Position);

    // Events
    public event Action<Touch> OnFirstTouchStart;
    public event Action<Touch, Vector2> OnFirstTouchMoved;
    public event Action<Touch> OnFirstTouchEnd;

    // Update is called once per frame
    void Update()
    {
        UpdateTouching();

        if (Touching)
        {
            // Cache first touch this frame
            Touch fTouch = FirstTouch;
            // Cache position info too
            Vector2 pos = fTouch.position;

            // Invoke events
            switch (fTouch.phase)
            {
                case TouchPhase.Began:
                    OnFirstTouchStart?.Invoke(fTouch);
                    break;
                case TouchPhase.Moved:
                    OnFirstTouchMoved?.Invoke(fTouch, fTouch.deltaPosition);
                    break;
                case TouchPhase.Ended:
                    OnFirstTouchEnd?.Invoke(fTouch);
                    break;
            }
        }
    }

    private void UpdateTouching()
    {
        // The screen is being touched at least at one point
        if (Input.touchCount > 0)
        {
            Touching = true;
        }
        else
        {
            Touching = false;
        }
    }
}