using TMPro;
using UnityEngine;

public class ControlParamsMenuItemDropDown : ControlParamsMenuItem {
    [SerializeField] private TMP_Dropdown dropdownInput;
    private void OnEnable() => dropdownInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => dropdownInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(int index){
        CurrentControlParam.drop_down_selected = dropdownInput.options[index].text;
    }
}

