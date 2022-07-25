using System;
using UnityEngine.SceneManagement;

public class UIController : IDisposable {
    private readonly SettingsMenuControl _settingsMenu;
    private readonly HUDControls _hud;

    public UIController(HUDControls hud, SettingsMenuControl settingsMenu){
        _settingsMenu = settingsMenu;
        _hud = hud;

        _hud.OnSettingsClickEvent += SettingsPanelOpen;
        _hud.OnExitClickEvent += TransitionToDeviceListScene;
    }
    public void Dispose(){
        _hud.OnSettingsClickEvent -= SettingsPanelOpen;
        _hud.OnExitClickEvent -=  TransitionToDeviceListScene;
    }

    private void SettingsPanelOpen(){
        _settingsMenu.gameObject.SetActive(true);
    }
    
    private void TransitionToDeviceListScene(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}