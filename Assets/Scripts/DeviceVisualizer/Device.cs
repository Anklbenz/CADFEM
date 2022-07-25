using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    [Header("AssetSystemName")]
    [SerializeField] private string assetName;
    [Header("SensorData")]
    [SerializeField] private List<Sensor> sensors;
    
    [Header("Description")]
    [SerializeField] private string label;

    [TextArea(3, 10)]
    [SerializeField] private string description;

    [SerializeField] private Sprite sprite;

    [Header("ModelTransforms")]
    [SerializeField] private Transform modelTransform;

    [SerializeField] private Transform doorTransform, outerBoxTransform;
    [SerializeField] private RectTransform schemeTransform;

    public string AssetSystemName => assetName;
    public Sprite Sprite => sprite;
    public string Label => label;
    public string Description => description;

    private ViewController _viewController;
    private SettingsMenuControl _menuControl;
    private SensorsRotateHandler _sensorsRotateHandler;

    private void Awake(){
        _viewController = new ViewController(modelTransform, doorTransform, outerBoxTransform, schemeTransform);
        _sensorsRotateHandler = new SensorsRotateHandler(sensors);
    }

    private void OnDestroy(){
        ControlsDisconnect();
    }

    public void Init(SettingsMenuControl menu, IDataSender dataSender){
        foreach (var sensor in sensors)
            sensor.Initialize(dataSender);
        
        _menuControl = menu;
        ControlsConnect();
    }

    private void LateUpdate(){
        _sensorsRotateHandler.Update();
    }

    private void ControlsConnect(){
        _menuControl.OnModelViewTurnEvent +=_viewController.ModelView;
        _menuControl.OnSchemeViewTurnEvent += _viewController.SchemeView;
        _menuControl.OnForceLineViewTurnEvent +=_viewController.ForceLineView;
        _menuControl.OnFilingViewTurnEvent += _viewController.OnlyFillingView;
        _menuControl.OnRotateSensorTurnEvent += _sensorsRotateHandler.SetRotateMode;
    }

    private void ControlsDisconnect(){
        _menuControl.OnModelViewTurnEvent -=_viewController.ModelView;
        _menuControl.OnSchemeViewTurnEvent -= _viewController.SchemeView;
        _menuControl.OnForceLineViewTurnEvent -=_viewController.ForceLineView;
        _menuControl.OnFilingViewTurnEvent -= _viewController.OnlyFillingView;
        _menuControl.OnRotateSensorTurnEvent -= _sensorsRotateHandler.SetRotateMode;
    }
}