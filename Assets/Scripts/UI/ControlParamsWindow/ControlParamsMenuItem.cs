using RequestParamClasses;
using ThingData;
using TMPro;
using UnityEngine;

public abstract class ControlParamsMenuItem : MonoBehaviour {
    private const string NOMINAL_VALUE_TEXT = "Рефереснсное значения для показателя";

    [SerializeField] protected TMP_Text descriptionText, nominalText;
 
    public WorkLogOperationSaveCpParam ControlParamSaveData;

    public virtual void Initialize(ControlParam controlParam){
       
        ControlParamSaveData = new WorkLogOperationSaveCpParam
        {
            work_log_operation_cp_id = controlParam.id
        };

        descriptionText.text = controlParam.name;
        var nominalHintMessage = $"{NOMINAL_VALUE_TEXT} {controlParam.value_nominal} {controlParam.value_unit}";
        nominalText.text = controlParam.value_nominal != null ? nominalHintMessage : "";
    }

    public void Delete(){
       Destroy(gameObject);
    }

    public abstract bool IsDataEntered();
}
