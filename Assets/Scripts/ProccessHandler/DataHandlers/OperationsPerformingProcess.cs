using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RequestParamClasses;
using ThingData;

public class OperationsPerformingProcess {
    private const string COMPLETED = "COMPLETED";

    private const string DAILY_ORDER = "WORK_LOG";
    private const string WORK_REQUEST = "REQUEST";

    private readonly OperationsWindow _operationsWindow;
    private readonly OperationsInitializer _operationsInitializer;
    private readonly Queue<Operation> _operationsQueue;
    private readonly ThingWorksServices _thingWorksServices;

    private GetInfo _workLogInfo;
    private int _currentWorkLogId;
    
    public OperationsPerformingProcess(ThingWorksServices thingWorksServices, OperationsWindow operationWindow){
        _operationsWindow = operationWindow;
        _thingWorksServices = thingWorksServices;
        _operationsInitializer = new OperationsInitializer(thingWorksServices);
        _operationsQueue = new Queue<Operation>();
    }
    public async UniTask TrackOperationsInitialize(int workLogId){
        _currentWorkLogId = workLogId;
        var operations = await _operationsInitializer.GetOperationsWithControlParams(_currentWorkLogId);

        _operationsWindow.Initialize(operations);

        foreach (var operation in operations)
            if (operation.operation_status_code != COMPLETED)
                _operationsQueue.Enqueue(operation);
        
        _operationsWindow.Show();
        await TrackOperations();
    }
    
    public async void TrackOperationsFinalize(){
        
        // SaveComment
    }

    private async UniTask TrackOperations(){
        while (_operationsQueue.Count > 0){
            var operation = _operationsQueue.Peek();

            await _thingWorksServices.WorkLogOperationSetPaused(operation.id);
            var saveParams = await _operationsWindow.PerformingProcess(operation);

            if (operation.control_params_count > 0) await SaveControlParams(saveParams);

            //            WorkLogOperation_CheckControlParamsAndFoto
            
            await _thingWorksServices.WorkLogOperationSetCompleted(operation.id);
            _operationsQueue.Dequeue();
        }
    }
    


    public async UniTask AppendOperationsFromWorkInstruction(int workLogId){
        var selectedCPFromLastOperation = await _thingWorksServices.WorkLogGetSelectedCPFromLastOperation(workLogId);

        await _thingWorksServices.WorkLogAppendOperationsFromWorkInstruction(workLogId, selectedCPFromLastOperation.work_instruction_id);
    }

    private async UniTask SaveControlParams(WorkLogOperationSaveCpParam[] saveParams){
        foreach (var saveParam in saveParams)
            await _thingWorksServices.WorkLogOperationSaveControlParam(saveParam);
    }
}