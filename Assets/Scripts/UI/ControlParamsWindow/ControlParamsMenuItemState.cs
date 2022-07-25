using ThingData;
using UnityEngine;
using UnityEngine.UI;

public class ControlParamsMenuItemState : ControlParamsMenuItem {
    [SerializeField] private Toggle toggleInput;
    private void OnEnable() => toggleInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => toggleInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(bool state) => CurrentControlParam.state_fact = state;

}
