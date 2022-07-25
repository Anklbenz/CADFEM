using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuControl : MonoBehaviour {
    [SerializeField] private Button closeButton;

    [Header("Menu Toggles")]
    [SerializeField] private Toggle modelViewToggle;

    [SerializeField] private Toggle onFillingViewToggle, forceLineToggle, schemeViewToggle, rotateSensorToggle;

    public event Action<bool> OnModelViewTurnEvent,
        OnFilingViewTurnEvent,
        OnForceLineViewTurnEvent,
        OnSchemeViewTurnEvent,
        OnRotateSensorTurnEvent;

    private void Awake(){
        closeButton.onClick.AddListener(Hide);
        modelViewToggle.onValueChanged.AddListener(ModelViewMode);
        onFillingViewToggle.onValueChanged.AddListener(FilingViewMode);
        forceLineToggle.onValueChanged.AddListener(ForceLineViewMode);
        schemeViewToggle.onValueChanged.AddListener(SchemeViewMode);
        rotateSensorToggle.onValueChanged.AddListener(SensorTurnMode);
    }

    private void Hide()=> this.gameObject.SetActive(false);
    private void ModelViewMode(bool state) => OnModelViewTurnEvent?.Invoke(state);
    private void FilingViewMode(bool state) => OnFilingViewTurnEvent?.Invoke(state);
    private void ForceLineViewMode(bool state) => OnForceLineViewTurnEvent?.Invoke(state);
    private void SchemeViewMode(bool state) => OnSchemeViewTurnEvent?.Invoke(state);
    private void SensorTurnMode(bool state) => OnRotateSensorTurnEvent?.Invoke(state);
}
