using ThingData;
using UnityEngine;
using UnityEngine.UI;

public class OperationButton : MonoBehaviour {
    [SerializeField] private Text idLabel;
    [SerializeField] private Button button;
    [SerializeField] private Image backImage;

    public Color Color{
        get => backImage.color;
        set => backImage.color = value;
    }
    // Action<Operation> callback,
    public void Initialize(Operation operation) 
    {
        idLabel.text = operation.mark_with_zeroes;
      //  button.onClick.AddListener(delegate { callback(operation); });
    }

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}