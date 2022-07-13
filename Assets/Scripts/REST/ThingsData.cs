using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsData {
    
    [System.Serializable]
    public class CounterData
    {
        public int counter;
    }

    [System.Serializable]
    public class AssetData
    {
        public string avatar;
        public string description;
        public string name;
    }

    [System.Serializable]
    public class AssetsData
    {
        public AssetData[] rows;
    }

    [System.Serializable]
    public class SaveDataInfo
    {
        public int numberProperty;
        public string textProperty;
        public AssetData jsonProperty;
    }
}