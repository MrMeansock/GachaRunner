using UnityEngine;

// Util to cache the main camera only once, make it static
// and make screen to world conversions static (common operation)
public class MainCamera : MonoBehaviour
{
    public static Camera Cam { get; private set; }

    private void Awake()
    {
        // Cache the camera because Camera.main is a search operation
        Cam = Camera.main;
    }

    public static Vector3 ScreenToWorldPoint(Vector3 position) => Cam.ScreenToWorldPoint(position);
}
