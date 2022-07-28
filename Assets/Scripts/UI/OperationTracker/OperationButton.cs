using ThingData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OperationButton : MonoBehaviour {
    [SerializeField] private TMP_Text idLabel;
    [SerializeField] private Button button;
    [SerializeField] private Image backImage;

    private Operation _operation;

    public void Initialize(Operation operation){
        _operation = operation;
        idLabel.text = operation.mark_with_zeroes;
        
        UpdateColor();
    }

    public void UpdateColor(){
        if (ColorUtility.TryParseHtmlString(_operation.operation_status_color, out var color))
            backImage.color = color;
    }

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}