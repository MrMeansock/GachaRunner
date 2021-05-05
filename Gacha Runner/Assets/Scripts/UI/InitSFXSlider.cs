using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InitSFXSlider : MonoBehaviour
{
    Slider slider;
    VolumeManager volumeManager;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        volumeManager = FindObjectOfType<VolumeManager>();
        slider.value = volumeManager.SFXVolume;
    }
}
