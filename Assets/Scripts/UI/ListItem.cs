using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {
    [SerializeField] private Image itemImage;
    [SerializeField] private Button button;
    [SerializeField] private Text itemLabel, itemDescription;
    
    public void Initialize(Device device, DeviceListControl listControl){
        itemLabel.text = device.Label;
        itemDescription.text = device.Description;
        itemImage.sprite = device.Sprite;

        button.onClick.AddListener(()=>listControl.ItemSelected(device));
    }

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}