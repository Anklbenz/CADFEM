using TMPro;
using UnityEngine;

public class ControlParamsMenuItemInput : ControlParamsMenuItem {
    [SerializeField] private TMP_InputField valueInput;
    private void OnEnable() => valueInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => valueInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(string text){
        if (float.TryParse(text, out var value)){
            ControlParamSaveData.value_fact = value;
        }
    }

    public override bool IsDataEntered(){
        return ControlParamSaveData.value_fact != null;
    }
}
