using System;
using System.Linq;
using ServiceRequestsData;
using ThingData;

public class SensorDataHandler : IDataSender {

    private const string ASSET_TAG_VALUES_INFO_SERVICE = "GetAssetTagValuesInfo";
    public event Action OnDataChangedEvent;

    private readonly WebRequestSender _webRequestSender;
    private readonly string _assetName;
    private SensorData[] _sensorsDataArray;

    public SensorDataHandler(WebRequestSender webRequestSender){
        _webRequestSender = webRequestSender;
    }

    public SensorData GetData(string tag){
        return _sensorsDataArray.FirstOrDefault(sensorData => sensorData.tag == tag);
    }

    public async void GetDataRequest(string assetName){
        var assetNameParam = CreateAssetNameParam(assetName);
        var assets = await _webRequestSender.GetServiceResult<SensorsData, AssetName>(ASSET_TAG_VALUES_INFO_SERVICE, assetNameParam);

        if (assets != null){
            _sensorsDataArray = assets.rows;
            OnDataChangedEvent?.Invoke();
        }
        else{
            //some log or message about request result in _apiRequestDataGetter.ResponseCode
        }
    }

    private AssetName CreateAssetNameParam(string assetName) => new AssetName() { thing = assetName };
}
