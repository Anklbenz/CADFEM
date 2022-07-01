using System;
using System.Collections.Generic;
using UnityEngine;

public class DeviceList : MonoBehaviour {
    [SerializeField] private List<Device> deviceList;
    [SerializeField] private Transform creationPoint;
    [SerializeField] private SettingsMenuControls menu;
    private Device _createdDevice;

    public void CreateItem(){
        _createdDevice = Instantiate(deviceList[0],  creationPoint.position, Quaternion.identity, this.transform);
        _createdDevice.Init(menu);
    }

    public void DeleteItem(){
        Destroy(_createdDevice.gameObject);
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.A))
            CreateItem();

        if (Input.GetKeyDown(KeyCode.L))
            DeleteItem();
    }
}
