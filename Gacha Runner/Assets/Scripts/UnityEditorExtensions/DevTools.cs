#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// For helper scripts on the toolbar
public class DevTools : MonoBehaviour
{
    [MenuItem("Dev Tools/Flush PlayerPrefs")]
    static void FlushPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
#endif