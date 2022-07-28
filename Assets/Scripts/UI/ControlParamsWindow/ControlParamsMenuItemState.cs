using ThingData;
using UnityEngine;
using UnityEngine.UI;

public class ControlParamsMenuItemState : ControlParamsMenuItem {
    [SerializeField] private Toggle toggleInput;
    private void OnEnable() => toggleInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => toggleInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(bool state){
        ControlParamSaveData.state_fact = state;
    }

    public override bool IsDataEntered(){
        return ControlParamSaveData.state_fact != null;
    }
}
