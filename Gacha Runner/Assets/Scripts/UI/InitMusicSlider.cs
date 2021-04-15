using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InitMusicSlider : MonoBehaviour
{
    Slider slider;
    VolumeManager volumeManager;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        volumeManager = FindObjectOfType<VolumeManager>();
    }

    void Start()
    {
        slider.value = volumeManager.MusicVolume;
    }
}
