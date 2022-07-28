using System.Collections.Generic;
using ThingData;
using TMPro;
using UnityEngine;

public class ControlParamsMenuItemDropDown : ControlParamsMenuItem {
    [SerializeField] private TMP_Dropdown dropdownInput;
    private readonly List<int> _dropdownRowsValues = new();
    private void OnEnable() => dropdownInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => dropdownInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(int index){
        ControlParamSaveData.selected_id = _dropdownRowsValues[index]; // dropdownInput.options[index].text
    }

    public override void Initialize(ControlParam controlParam){
        base.Initialize(controlParam);

        foreach (var dropdownRow in controlParam.DropDownRows){
            var rowId = dropdownRow.id;
            var rowName = dropdownRow.name;

            dropdownInput.options.Add(new TMP_Dropdown.OptionData($"{rowId} - {rowName}"));
            _dropdownRowsValues.Add(rowId);
        }
    }

    public override bool IsDataEntered(){
        return /*CurrentControlParam.DropDownRows.Length == 0 ||*/ dropdownInput.value != 0; //    throw new System.NotImplementedException();
    }
}
