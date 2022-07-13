using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControls : MonoBehaviour {
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button deviceSelectButton;

    public event Action OnSettingsClickEvent;
    public event Action OnExitClickEvent;
    public event Action OnDeviceSelectClickEvent;

    private void Awake(){
        settingsButton.onClick.AddListener(()=>OnSettingsClickEvent?.Invoke());
        deviceSelectButton.onClick.AddListener(()=>OnDeviceSelectClickEvent?.Invoke());
    }

    private void OnDestroy(){
        settingsButton.onClick.RemoveAllListeners();
        deviceSelectButton.onClick.RemoveAllListeners();
    }
}
