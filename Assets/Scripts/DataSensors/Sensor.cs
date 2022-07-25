using UnityEngine;
using UnityEngine.UI;

public class Sensor : MonoBehaviour, IRotatable {
    [Header("SystemTag")]
    [SerializeField] private string sensorTag;

    [Header("DataControls")]
    [SerializeField] private Text tagIdText;

    [SerializeField] private Text tagIdFirstSymbolText, valueText, unitsText;

    public bool Rotatable{ get; set; }

    private IDataSender _dataSender;

    public void Initialize(IDataSender dataSender){
        _dataSender = dataSender;
        _dataSender.OnDataChangedEvent += UpdateData;
    }

//    private void OnDestroy() => _dataSender.OnDataChangedEvent -= UpdateData;


    private void UpdateData(){
        var data = _dataSender.GetData(sensorTag);

        tagIdText.text = data.tag;
        valueText.text = data.value.ToString();
        unitsText.text = data.unit;

        if (ColorUtility.TryParseHtmlString(data.fill, out var color))
            valueText.color = color;

        if (data.tag.Length > 0)
            tagIdFirstSymbolText.text = data.tag[0].ToString();
    }
}