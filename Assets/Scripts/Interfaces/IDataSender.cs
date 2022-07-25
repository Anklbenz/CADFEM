using System;
using ThingData;

public interface IDataSender {
    event Action OnDataChangedEvent;
    SensorData GetData(string tag);
}