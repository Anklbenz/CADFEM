using System;
using Cysharp.Threading.Tasks;
using RequestParamClasses;
using ThingData;

public class ThingWorksServices : WebRequestSender {

    private const string ASSET_TAG_VALUES_INFO_SERVICE = "GetAssetTagValuesInfo";

    private const string REQUEST_AND_WORK_LOG_GET_VIEW = "RequestAndWorkLog_GetView";
    
    private const string WORK_LOG_GET_INFO = "WorkLog_GetInfo";
    private const string WORK_LOG_CREATE_FROM_REQUEST = "WorkLog_CreateFromRequest";
    private const string WORK_LOG_GET_SELECTED_CP_FROM_LAST_OPERATION = "WorkLog_GetSelectedCPFromLastOperation";
    private const string WORK_LOG_APPEND_OPERATIONS_FROM_WORK_INSTRUCTION = "WorkLog_GetSelectedCPFromLastOperation";
    
    private const string WORK_LOG_GET_OPERATION = "WorkLog_GetOperations";
    private const string WORK_LOG_OPERATION_SET_PAUSED = "WorkLogOperation_SetPaused";
    private const string WORK_LOG_OPERATION_SET_COMPLETED = "WorkLogOperation_SetCompleted";
    private const string WORK_LOG_OPERATION_GET_CONTROL_PARAMS = "WorkLogOperation_GetControlParams";
    private const string WORK_LOG_OPERATION_GET_DROPDOWN_ROWS = "WorkLogOperation_GetDropDownRowsForControlParam";
    private const string WORK_LOG_OPERATION_SAVE_CONTROL_PARAM = "WorkLogOperation_SaveControlParam";
    
    private const string WORK_INSTRUCTION_OPERATION_GET_CONTROL_PARAMS = "WorkInstructionOperation_GetControlParams";
    private const string WORK_INSTRUCTION_OPERATION_GET_DROPDOWN_ROWS = "WorkInstructionOperation_GetDropDownRowsForControlParam";

    private readonly ThingWorksParamCreator _paramCreator;

    public ThingWorksServices(string servicesUrl, string authorizationHeader) : base(servicesUrl, authorizationHeader){
        _paramCreator = new ThingWorksParamCreator();
    }

    public async UniTask<Operation[]> WorkLogGetOperations(int workLogId){
        var workLogIdClass = _paramCreator.WorkLogIdCreate(workLogId);
        var operations = await GetServiceResult<Operations, WorkLogIdParam>(WORK_LOG_GET_OPERATION, workLogIdClass);
        return operations != null ? operations.rows : new Operation[] { new Operation() };
    }

    public async UniTask<ControlParam[]> WorkInstructionOperationGetControlParams(int workInstructionOperationId){
        var workLogOperationIdClass = _paramCreator.WorkInstructionOperationIdCreate(workInstructionOperationId);
        var parameters =
            await GetServiceResult<ControlParams, WorkInstructionOperationIdParam>(WORK_INSTRUCTION_OPERATION_GET_CONTROL_PARAMS,
                workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<ControlParamDropDownRow[]> WorkInstructionOperationGetDropDownRowsForControlParams(int workInstructionOperationCpId){
        var workInstructionOperationCpIdClass = _paramCreator.WorkInstructionOperationCpIdCreate(workInstructionOperationCpId);
        var parameters =
            await GetServiceResult<ControlParamDropDownRows, WorkInstructionOperationCpIdParam>(WORK_INSTRUCTION_OPERATION_GET_DROPDOWN_ROWS,
                workInstructionOperationCpIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("Drop down params cannot be null");
    }

    public async UniTask<TaskData[]> RequestAndWorkLogGetView(string assetName){
        var assetNameParam = _paramCreator.ThingNameCreate(assetName);
        var tasksList = await GetServiceResult<TasksList, ThingParam>(REQUEST_AND_WORK_LOG_GET_VIEW, assetNameParam);
        return tasksList != null ? tasksList.rows : throw new ArgumentException("TasksList cannot be null");
    }

    public async UniTask<SensorData[]> GetAssetTagValuesInfo(string assetName){
        var assetNameParam = _paramCreator.ThingNameCreate(assetName);
        var sensorsData = await GetServiceResult<SensorsData, ThingParam>(ASSET_TAG_VALUES_INFO_SERVICE, assetNameParam);
        return sensorsData != null ? sensorsData.rows : throw new ArgumentException("Sensor Data cannot be null");
    }

    public async UniTask<CreatedWorkLog> WorkLogCreateFromRequest(int requestId){
        var requestIdParam = _paramCreator.RequestIdCreate(requestId);
        var createdWorkLogs = await GetServiceResult<CreatedWorkLogs, RequestId>(WORK_LOG_CREATE_FROM_REQUEST, requestIdParam);
        return createdWorkLogs != null ? createdWorkLogs.rows[0] : throw new ArgumentException("Created WorkLog Data cannot be null");
    }
    
    public async UniTask<GetInfo> WorkLogGetInfo(int workLogId){
        var workLogIdParam = _paramCreator.WorkLogIdCreate(workLogId);
        var getInfoArray = await GetServiceResult<GetInfoArray, WorkLogIdParam>(WORK_LOG_GET_INFO, workLogIdParam);
        return getInfoArray != null ? getInfoArray.rows[0] : throw new ArgumentException("Get Info Data Data cannot be null");
    }

    public async UniTask<SelectedCPFromLastOperation> WorkLogGetSelectedCPFromLastOperation(int workLogId){
        var workLogIdParam = _paramCreator.WorkLogIdCreate(workLogId);
        var selectedCpFromLastOperationArray = await GetServiceResult<SelectedCPFromLastOperationArray, WorkLogIdParam>(WORK_LOG_GET_SELECTED_CP_FROM_LAST_OPERATION, workLogIdParam);
        return selectedCpFromLastOperationArray != null ? selectedCpFromLastOperationArray.rows[0] : throw new ArgumentException("Selected CP from last operation is NULL");
    }
    
    public async UniTask<Status> WorkLogAppendOperationsFromWorkInstruction(int workLogId, int workInstructionId){
        var appendOperationFromWorkInstructionParam = _paramCreator.WorkLogIdWorkInstructionIdCreate(workLogId, workInstructionId);
        var status = await GetServiceResult<Status, WorkLogIdWorkInstructionIdParam>(WORK_LOG_APPEND_OPERATIONS_FROM_WORK_INSTRUCTION , appendOperationFromWorkInstructionParam );
        return status;
    }

    public async UniTask<ControlParam[]> WorkLogOperationGetControlParams(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        var parameters =
            await GetServiceResult<ControlParams, WorkLogOperationIdParam>(WORK_LOG_OPERATION_GET_CONTROL_PARAMS, workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<WorkLogOperationDropDownRow[]> WorkLogOperationGetDropDownRowsForControlParam(int workLogOperationCpId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationCpIdCreate(workLogOperationCpId);
        var parameters =
            await GetServiceResult<WorkLogOperationDropDownRows, WorkLogOperationCpIdParam>(WORK_LOG_OPERATION_GET_DROPDOWN_ROWS,
                workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<Status> WorkLogOperationSetCompleted(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        return await GetServiceResult<Status, WorkLogOperationIdParam>(WORK_LOG_OPERATION_SET_COMPLETED, workLogOperationIdClass);
    }

    public async UniTask<Status> WorkLogOperationSetPaused(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        return await GetServiceResult<Status, WorkLogOperationIdParam>(WORK_LOG_OPERATION_SET_PAUSED, workLogOperationIdClass);
    }

    public async UniTask<Status> WorkLogOperationSaveControlParam(WorkLogOperationSaveCpParam saveParam){
        return await GetServiceResult<Status, WorkLogOperationSaveCpParam>(WORK_LOG_OPERATION_SAVE_CONTROL_PARAM, saveParam);
    }
}
