using System;

namespace ServiceRequestsData {
    public class AssetName {
        public string thing;
    }

    public class WorkLogIdParam {
        public int work_log_id;
    }   
    public class OperationIdParam {
        public int work_log_operation_id;
    }

    public class ControlParameterIdParam {
        public int work_instruction_operation_cp_id;
    }

    public class ControlParamSaveValueInt {
        public int work_log_operation_cp_id;
        public int value_fact;
    }
}