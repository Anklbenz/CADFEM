using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    [Header("Description")]
    [SerializeField] private string label;

    [TextArea(3, 10)]
    [SerializeField] private string description;

    [SerializeField] private Sprite sprite;

    [Header("SensorData")]
    [SerializeField] private List<Sensor> sensors;

    [SerializeField] private RectTransform schemeInstanceRect;

    [Header("ModelTransforms")]
    [SerializeField] private Transform modelTransform;

    [SerializeField] private Transform doorTransform, outerBoxTransform;

    public Sprite Sprite => sprite;
    public string Label => label;
    public string Description => description;

    private ViewController _viewController;
    private SettingsMenuControls _menuControls;
    private SensorVisualHandler _sensorVisualHandler;

    private void Awake(){
        _viewController = new ViewController(modelTransform, doorTransform, outerBoxTransform, schemeInstanceRect);
        _sensorVisualHandler = new SensorVisualHandler(sensors);
    }

    private void OnDestroy(){
        ControlsDisconnect();
    }

    public void Init(SettingsMenuControls menu){
        _menuControls = menu;
        ControlsConnect();
    }

    private void LateUpdate(){
        _sensorVisualHandler.Update();
    }

    private void ControlsConnect(){
        _menuControls.rotateSensorToggle.onValueChanged.AddListener(_sensorVisualHandler.SetRotateMode);

        _menuControls.modelViewToggle.onValueChanged.AddListener(_viewController.ModelView);
        _menuControls.schemeViewToggle.onValueChanged.AddListener(_viewController.SchemeView);
        _menuControls.forceLineToggle.onValueChanged.AddListener(_viewController.ForceLineView);
        _menuControls.onFillingViewToggle.onValueChanged.AddListener(_viewController.OnlyFillingView);
    }

    private void ControlsDisconnect(){
        _menuControls.rotateSensorToggle.onValueChanged.RemoveListener(_sensorVisualHandler.SetRotateMode);

        _menuControls.modelViewToggle.onValueChanged.RemoveListener(_viewController.ModelView);
        _menuControls.schemeViewToggle.onValueChanged.RemoveListener(_viewController.SchemeView);
        _menuControls.forceLineToggle.onValueChanged.RemoveListener(_viewController.ForceLineView);
        _menuControls.onFillingViewToggle.onValueChanged.RemoveListener(_viewController.OnlyFillingView);
    }
}