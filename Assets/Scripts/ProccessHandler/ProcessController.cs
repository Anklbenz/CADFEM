using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ProcessController : MonoBehaviour {
    private const string LOGIN = "Basic QWRtaW5pc3RyYXRvcjoxMjM0NXF3ZXJ0YXNkZkc=";
    private const string SERVICES_URL_TEST = "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/Things/ServiceVizor.VuforiaAR_TH/Services/";

    [SerializeField] private TransferObject transferObject;
    [SerializeField] private Transform creationPoint;

    [Header("UI Prefabs")]
    [SerializeField] private TaskListWindow taskListWindow;

    [SerializeField] private DialogWindow dialogWindow;

    [SerializeField] private SettingsMenuControl settingsMenuControl;
    [SerializeField] private HUDControls hudControls;

    [Header("OperationTracker")]
    [SerializeField]
    private OperationsWindow operationsWindow;

    private UIController _uiController;
    private ThingWorksServices _thingWorksServices;
    private TaskSelector _taskSelector;
    private OperationsPerformingProcess _operationsPerformingProcess;

    private SensorDataHandler _sensorDataHandler;
    private Device _createdDevice;

    private void Start(){
        var prefab = transferObject.prefab;
        if (prefab == null) throw new Exception("Prefab not found");

        _thingWorksServices = new ThingWorksServices(SERVICES_URL_TEST, LOGIN);
        _thingWorksServices.OnServerResponseEvent += RequestResultLogger;

        _taskSelector = new TaskSelector(_thingWorksServices, taskListWindow, dialogWindow);
        _operationsPerformingProcess = new OperationsPerformingProcess(_thingWorksServices, operationsWindow);

        _sensorDataHandler = new SensorDataHandler(_thingWorksServices);
        _uiController = new UIController(hudControls, settingsMenuControl);

        InstantiateDevice(prefab, settingsMenuControl);

        Play();
    }

    private async void Play(){
        var selectedWorkLogId = await _taskSelector.TaskSelectionProcess(_createdDevice.AssetSystemName);
        var workLogInfo = await _thingWorksServices.WorkLogGetInfo(selectedWorkLogId);

        if (workLogInfo.is_fault_request){
            await _operationsPerformingProcess.TrackOperationsInitialize(selectedWorkLogId);
            await _operationsPerformingProcess.AppendOperationsFromWorkInstruction(selectedWorkLogId);
            await _operationsPerformingProcess.TrackOperationsInitialize(selectedWorkLogId);
        }
        else{
            await _operationsPerformingProcess.TrackOperationsInitialize(selectedWorkLogId);
        }

        _operationsPerformingProcess.TrackOperationsFinalize();
    }

    private void InstantiateDevice(Device prefab, SettingsMenuControl settingsMenu){
        _createdDevice = Instantiate(prefab, creationPoint.position, Quaternion.identity, creationPoint.transform);
        _createdDevice.Init(settingsMenu, _sensorDataHandler);
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.X))
            _sensorDataHandler.GetDataRequest(_createdDevice.AssetSystemName);
    }

    private void RequestResultLogger(long responseCode){
        Debug.Log(responseCode);
    }
}