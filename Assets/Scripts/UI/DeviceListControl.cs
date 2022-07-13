using System;
using UnityEngine;

public class DeviceListControl : MonoBehaviour {
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private ListItem itemPrefab;
    
    public event Action ItemSelectedEvent;
    
    public Device SelectedDevice{ get; set; }
    public void Add(Device device){
        var item = Instantiate(itemPrefab, contentParent);

        item.Initialize(device, this);
    }

    public void ItemSelected(Device device){
        SelectedDevice = device;
        ItemSelectedEvent?.Invoke();
    }
    
    //Sort
    
    //Remove
    
    //Update
}
