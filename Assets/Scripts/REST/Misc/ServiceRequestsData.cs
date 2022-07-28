using System;

namespace RequestParamClasses {
    public class ThingParam {
        public string thing;
    }

    public class WorkLogIdParam {
        public int work_log_id;
    }

    public class WorkLogOperationIdParam {
        public int work_log_operation_id;
    }

    public class WorkInstructionOperationCpIdParam {
        public int work_instruction_operation_cp_id;
    }

    public class ControlParamSaveValueInt {
        public int work_log_operation_cp_id;
        public int value_fact;
    }

    public class WorkLogOperationSaveCpParam {
        public int work_log_operation_cp_id;
        public float value_fact;
        public bool? state_fact;
        public int selected_id;
    }

    public class WorkInstructionOperationIdParam {
        public int work_instruction_operation_id;
    }
    
    public class WorkLogOperationCpIdParam {
        public int work_log_operation_cp_id;
    }

    public class RequestId {
        public int request_id;
    }

    public class WorkLogIdWorkInstructionIdParam {
        public int work_log_id;
        public int work_instruction_id;
    }

}