using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDControls : MonoBehaviour {
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
  
    public event Action OnSettingsClickEvent;
    public event Action OnExitClickEvent;

    private void Awake(){
        settingsButton.onClick.AddListener(OnSettingsNotify);
        exitButton.onClick.AddListener( OnExitNotify);
    }

    private void OnDestroy(){
        settingsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    private void OnSettingsNotify(){
        OnSettingsClickEvent?.Invoke();
    }
    
    private void OnExitNotify(){
        OnExitClickEvent?.Invoke();
    }
}

