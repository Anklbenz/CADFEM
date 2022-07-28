using RequestParamClasses;
using ThingData;

public class ThingWorksParamCreator {

    public WorkLogIdParam WorkLogIdCreate(int workLogId){
        return new WorkLogIdParam { work_log_id = workLogId };
    }

    public RequestId RequestIdCreate(int requestId){
        return new RequestId() { request_id = requestId };
    }

    public WorkLogOperationIdParam WorkLogOperationIdCreate(int workLogOperationId){
        return new WorkLogOperationIdParam { work_log_operation_id = workLogOperationId };
    }

    public WorkLogOperationCpIdParam WorkLogOperationCpIdCreate(int workLogOperationCpId){
        return new WorkLogOperationCpIdParam { work_log_operation_cp_id = workLogOperationCpId };
    }

    public WorkLogIdWorkInstructionIdParam WorkLogIdWorkInstructionIdCreate(int workLogId, int workInstructionId){
        return new WorkLogIdWorkInstructionIdParam { work_log_id = workLogId, work_instruction_id = workInstructionId };
    }

    public WorkInstructionOperationIdParam WorkInstructionOperationIdCreate(int workInstructionOperationId){
        return new WorkInstructionOperationIdParam { work_instruction_operation_id = workInstructionOperationId };
    }

    public WorkInstructionOperationCpIdParam WorkInstructionOperationCpIdCreate(int workInstructionOperationCpId){
        return new WorkInstructionOperationCpIdParam { work_instruction_operation_cp_id = workInstructionOperationCpId };
    }

    public ThingParam ThingNameCreate(string assetName){
        return new ThingParam() { thing = assetName };
    }
}
