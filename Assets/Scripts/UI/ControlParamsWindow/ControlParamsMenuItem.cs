using ThingData;
using UnityEngine;
using UnityEngine.UI;

public abstract class ControlParamsMenuItem : MonoBehaviour {
    private const string NOMINAL_VALUE_TEXT = "Рефереснсное значения для показателя";

    [SerializeField] protected Text descriptionText, nominalText;
 
    protected ControlParam CurrentControlParam;

    public void Initialize(ControlParam controlParam){
        CurrentControlParam = controlParam;

        descriptionText.text = controlParam.name;
        var nominalHintMessage = $"{NOMINAL_VALUE_TEXT} {controlParam.value_nominal} {controlParam.value_unit}";
        nominalText.text = controlParam.value_nominal != null ? nominalHintMessage : "";
    }


    public ControlParam GetInputData() => CurrentControlParam;
}
