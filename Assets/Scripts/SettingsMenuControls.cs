using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuControls : MonoBehaviour {
    [SerializeField] private Button closeButton;

    [Header("Menu Toggles")] 
    public Toggle modelViewToggle;
    public Toggle onFillingViewToggle, forceLineToggle, schemeViewToggle, rotateSensorToggle;

    public event Action OnSettingsCloseEvent;

    private void Awake(){
        closeButton.onClick.AddListener(() => OnSettingsCloseEvent?.Invoke());
    }
}
