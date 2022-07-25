namespace ThingData {

    [System.Serializable]
    public class ControlParamDropDownRow {
        public int id;
        public int work_instruction_operation_cp_id;
        public string name;
    }
    [System.Serializable]
    public class ControlParamDropDownRows {
        public ControlParamDropDownRow[] rows;
    }

    [System.Serializable]
    public class ControlParam {
        public int id;
        public string operation_cp_type_code;
        public string name;
        public string value_nominal;
        public string value_fact;
        public string value_unit;
        public bool state_nominal;
        public bool state_fact;
        public string drop_down_selected;
        public bool is_complete;

        public ControlParamDropDownRow[] DropDownRows;
    }
    
    [System.Serializable]
    public class ControlParams {
        public ControlParam[] rows;
    }

    [System.Serializable]
    public class Operation {
        public int id;
        public string code;
        public string mark;
        public string mark_with_zeroes;
        public string work_log_id;
        public string asset_part_id;
        public string name;
        public string description;
        public string operation_status_code;
        public string operation_status_color;
        public string executor_person_id;
        public string duration_minutes;
        public string url_image;
        public string url_video;
        public string url_document;
        public string url_cad;
        public string created_dt;
        public string creator_person_id;
        public string modified_dt;
        public string modifier_person_id;
        public bool is_foto_required;
        public string url_foto;
        public string work_instruction_operation_id;
        public string work_instruction_id;
        public string fact_start_dt;
        public string fat_end_dt;
        public int control_params_count;

        public ControlParam[] ControlParams;
    }

    [System.Serializable]
    public class Operations {
        public Operation[] rows;
    }

    [System.Serializable]
    public class TaskData {
        public string row_id;
        public string row_code;
        public string row_type_code;
        public string row_type_name;
        public string row_status_code;
        public string row_status_name;
        public string row_status_color;
        public string row_comment;
        public string request_id;
        public int work_log_id;
    }

    [System.Serializable]
    public class TasksList {
        public TaskData[] rows;
    }

    [System.Serializable]
    public class SensorData {
        public string tag_full;
        public string tag;
        public float value;
        public string unit;
        public string fill;
        public string comment;
        public string text;
    }

    [System.Serializable]
    public class SensorsData {
        public SensorData[] rows;
    }

}