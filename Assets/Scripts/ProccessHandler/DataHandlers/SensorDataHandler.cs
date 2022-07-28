using System;
using System.Linq;
using ThingData;

public class SensorDataHandler : IDataSender {
    public event Action OnDataChangedEvent;

    private readonly ThingWorksServices _thingWorksServices;
    private readonly string _assetName;
    private SensorData[] _sensorsDataArray;

    public SensorDataHandler(ThingWorksServices thingWorksServices){
        _thingWorksServices = thingWorksServices;
    }

    public SensorData GetData(string tag){
        return _sensorsDataArray.FirstOrDefault(sensorData => sensorData.tag == tag);
    }

    public async void GetDataRequest(string assetName){
        var assets = await _thingWorksServices.GetAssetTagValuesInfo(assetName);
        _sensorsDataArray = assets;
         OnDataChangedEvent?.Invoke();
    }
}
