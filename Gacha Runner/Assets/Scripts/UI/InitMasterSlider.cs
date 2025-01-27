﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InitMasterSlider : MonoBehaviour
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
        slider.value = volumeManager.MasterVolume;
    }
}
