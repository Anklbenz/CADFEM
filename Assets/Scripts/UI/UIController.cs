using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] private SettingsMenuControls settingsMenuControls;
    [SerializeField] private HUDControls hud;
    [SerializeField] private DeviceListControl deviceList;

    private void Awake(){
        deviceList.ItemSelectedEvent+= WorkMode;
        settingsMenuControls.OnSettingsCloseEvent += WorkMode;
        hud.OnSettingsClickEvent += SettingsMode;
        hud.OnDeviceSelectClickEvent += DeviceSelectMode;
        
        DeviceSelectMode();
    }

    private void WorkMode(){
        DeviceListVisible(false);
        HUDVisible(true);
        SettingsMenuVisible(false);
    }

    private void SettingsMode(){
        SettingsMenuVisible(true);
    }

    private void DeviceSelectMode(){
        HUDVisible(false);
        SettingsMenuVisible(false);
        DeviceListVisible(true);
    }

    private void SettingsMenuVisible(bool state){
        settingsMenuControls.gameObject.SetActive(state);
    }

    private void HUDVisible(bool state){
        hud.gameObject.SetActive(state);
    }

    private void DeviceListVisible(bool state){
        deviceList.gameObject.SetActive(state);
    }
}
