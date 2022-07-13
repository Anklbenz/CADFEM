using System.Collections.Generic;
using UnityEngine;

public class DeviceMappingHandler : MonoBehaviour {
    [SerializeField] private List<Device> deviceList;
    [SerializeField] private DeviceListControl deviceListControl;
    [SerializeField] private SettingsMenuControls settingsMenuControls;
    [SerializeField] private Transform creationPoint;

    private Device _createdDevice;

    private void Start(){
        deviceListControl.ItemSelectedEvent += PrepareAndCreate;
        Initialize();
    }

    private void OnDestroy(){
        deviceListControl.ItemSelectedEvent -= PrepareAndCreate;
    }

    private void Initialize(){
        foreach (var device in deviceList)
            deviceListControl.Add(device);
    }

    private void PrepareAndCreate(){
        if (_createdDevice != null)
            Destroy(_createdDevice.gameObject);

        var prefab = deviceListControl.SelectedDevice;
        _createdDevice = Instantiate(prefab, creationPoint.position, Quaternion.identity, creationPoint.transform);
        _createdDevice.Init(settingsMenuControls);
    }
}
