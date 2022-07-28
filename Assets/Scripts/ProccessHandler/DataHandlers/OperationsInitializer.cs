using Cysharp.Threading.Tasks;
using ThingData;

public class OperationsInitializer {
    private const string CP_DROP_DOWN_CODE = "CP_DROP_DOWN";

    private readonly ThingWorksServices _thingWorksServices;

    public OperationsInitializer(ThingWorksServices thingWorksServices){
        _thingWorksServices = thingWorksServices;
    }

    public async UniTask<Operation[]> GetOperationsWithControlParams(int workLogId){
        var operations = await _thingWorksServices.WorkLogGetOperations(workLogId);
        foreach (var operation in operations)
            if (operation.control_params_count > 0)
                await InitializeControlParams(operation);

        return operations;
    }

    private async UniTask InitializeControlParams(Operation operation){
        operation.ControlParams = await _thingWorksServices.WorkLogOperationGetControlParams(operation.id);
        if (operation.ControlParams != null)
            await InitializeDropDown(operation.ControlParams);
    }

    private async UniTask InitializeDropDown(ControlParam[] controlParams){
        foreach (var controlParam in controlParams)
            if (controlParam.operation_cp_type_code == CP_DROP_DOWN_CODE)
                controlParam.DropDownRows = await _thingWorksServices.WorkLogOperationGetDropDownRowsForControlParam(controlParam.id);
    }
}