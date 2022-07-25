using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ProcessController : MonoBehaviour {

    private const string LOGIN = "Basic QWRtaW5pc3RyYXRvcjoxMjM0NXF3ZXJ0YXNkZkc=";
    private const string SERVICES_URL_TEST = "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/Things/ServiceVizor.VuforiaAR_TH/Services/";
    private const string DAILY_ORDER_KEY = "WORK_LOG";
    private const string WORK_REQUEST_KEY = "REQUEST";

    [SerializeField] private TransferObject transferObject;
    [SerializeField] private Transform creationPoint;

    [Header("UI Prefabs")]
    [SerializeField] private TaskListWindow taskListWindow;

    [SerializeField] private SettingsMenuControl settingsMenuControl;
    [SerializeField] private HUDControls hudControls;

    [Header("OperationTracker")]
    [SerializeField]
    private OperationsPerformingWindow operationsPerformingWindow;


    private UIController _uiController;
    private WebRequestSender _webRequestSender;
    private TaskSelectProcess _taskSelectProcess;
    private OperationsPerformingProcess _operationsPerformingProcess;

    private SensorDataHandler _sensorDataHandler;
    private Device _createdDevice;

    private void Start(){
        var prefab = transferObject.prefab;
        if (prefab == null) throw new Exception("Prefab not found");

        _webRequestSender = new WebRequestSender(SERVICES_URL_TEST, LOGIN);
        _webRequestSender.OnServerResponseEvent += RequestResultLogger;

        _taskSelectProcess = new TaskSelectProcess(_webRequestSender, taskListWindow);
        _operationsPerformingProcess = new OperationsPerformingProcess(_webRequestSender, operationsPerformingWindow);

        _sensorDataHandler = new SensorDataHandler(_webRequestSender);
        _uiController = new UIController(hudControls, settingsMenuControl);

        InstantiateDevice(prefab, settingsMenuControl);

        Play();
    }

    private async void Play(){
        var taskData = await _taskSelectProcess.SelectTask(_createdDevice.AssetSystemName);

        if (taskData.row_type_code == DAILY_ORDER_KEY){
            await _operationsPerformingProcess.Initialize(taskData.work_log_id);

            _operationsPerformingProcess.TrackOperations();
        }
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