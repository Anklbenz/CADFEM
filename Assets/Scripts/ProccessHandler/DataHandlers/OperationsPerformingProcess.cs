using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ServiceRequestsData;
using ThingData;

public class OperationsPerformingProcess {
    private const string GET_OPERATIONS_SERVICE = "WorkLog_GetOperations";
    private const string GET_CONTROL_PARAMS_SERVICE = "WorkLogOperation_GetControlParams";
    private const string GET_PARAM_DROP_DOWN_ROW_SERVICE = "WorkInstructionOperation_GetDropDownRowsForControlParam";
    private const string CP_DROP_DOWN_CODE = "CP_DROP_DOWN";
  
    private readonly WebRequestSender _webRequestSender;
    private readonly OperationsPerformingWindow _operationsWindow;
    private Queue<Operation> _operationsQueue;

    public OperationsPerformingProcess(WebRequestSender webRequestSender, OperationsPerformingWindow operationWindow){
        _webRequestSender = webRequestSender;
        _operationsWindow = operationWindow;
    }

    public async UniTask Initialize(int workLogId){
        var idParam = CreateWorkLogIdParam(workLogId);
        var operations = await GetOperations(idParam);

        foreach (var operation in operations){
            if (operation.control_params_count > 0)
                await InitializeControlParams(operation);

            _operationsWindow.AddOperationButton(operation);
        }

        _operationsQueue = new Queue<Operation>(operations);
    }
    
    public async void TrackOperations(){
        _operationsWindow.gameObject.SetActive(true);

        var operation = _operationsQueue.Peek();

       await _operationsWindow.OperationPerformingProcess(operation);
    }

    private async UniTask InitializeControlParams(Operation operation){
        var operationIdParam = CreateOperationIdParam(operation.id);
        var controlParams = await GetParams(operationIdParam);
        operation.ControlParams = await InitializeDropDown(controlParams);
    }

    private async UniTask<ControlParam[]> InitializeDropDown(ControlParam[] controlParams){
        foreach (var param in controlParams){
            if (param.operation_cp_type_code != CP_DROP_DOWN_CODE) continue;
            var controlParamId = CreateControlParameterIdParam(param.id);
            param.DropDownRows = await GetDropDownParams(controlParamId);
        }

        return controlParams;
    }
    
    private async UniTask<Operation[]> GetOperations(WorkLogIdParam idParam){
        var operations = await _webRequestSender.GetServiceResult<Operations, WorkLogIdParam>(GET_OPERATIONS_SERVICE, idParam);
        return operations != null ? operations.rows : new Operation[] { new Operation() };
    }

    private async UniTask<ControlParam[]> GetParams(OperationIdParam idParam){
        var parameters = await _webRequestSender.GetServiceResult<ControlParams, OperationIdParam>(GET_CONTROL_PARAMS_SERVICE, idParam);
       return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    private async UniTask<ControlParamDropDownRow[]> GetDropDownParams(ControlParameterIdParam controlParamId){
        var parameters = await _webRequestSender.GetServiceResult<ControlParamDropDownRows, ControlParameterIdParam>(GET_PARAM_DROP_DOWN_ROW_SERVICE, controlParamId);
        return parameters != null ? parameters.rows : throw new ArgumentException("Drop down params cannot be null");
    }

    private OperationIdParam CreateOperationIdParam(int operationId) => new() { work_log_operation_id = operationId };
    private WorkLogIdParam CreateWorkLogIdParam(int workLogId) => new() { work_log_id = workLogId };
    private ControlParameterIdParam CreateControlParameterIdParam(int controlParamId) => new()  {work_instruction_operation_cp_id = controlParamId };
}