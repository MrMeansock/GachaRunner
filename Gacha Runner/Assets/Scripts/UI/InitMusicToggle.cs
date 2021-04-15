using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class InitMusicToggle : MonoBehaviour
{
    Toggle toggle;
    VolumeManager volumeManager;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        volumeManager = FindObjectOfType<VolumeManager>();
    }

    void Start()
    {
        toggle.isOn = volumeManager.MusicIsMuted;
    }
}
