using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameObject settingsCanvas;

    public void SettingsTurn(){
        settingsCanvas.gameObject.SetActive(true);
    }

    public void SettingsTurOff(){
        settingsCanvas.gameObject.SetActive(false);
    }
}
