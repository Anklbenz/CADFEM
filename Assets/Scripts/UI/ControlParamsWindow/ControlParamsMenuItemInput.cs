using TMPro;
using UnityEngine;

public class ControlParamsMenuItemInput : ControlParamsMenuItem {
    [SerializeField] private TMP_InputField valueInput;
    private void OnEnable() => valueInput.onValueChanged.AddListener(ChangeInputValue);
    private void OnDisable() => valueInput.onValueChanged.RemoveListener(ChangeInputValue);

    private void ChangeInputValue(string text) =>  CurrentControlParam.value_fact = text;
   
}
