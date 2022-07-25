using System;
using ThingData;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItem : MonoBehaviour {
    [SerializeField] private Text typeText, taskCodeText, descriptionText, commentText;
    [SerializeField] private Toggle selectToggle;

    public event Action<TaskData> OnTaskSelectEvent;
    private TaskData _taskData;

    public void Initialize(TaskData taskData, ToggleGroup toggleGroup){
        _taskData = taskData;

        typeText.text = taskData.row_type_name;
        taskCodeText.text = taskData.row_code;
        descriptionText.text = taskData.row_status_name;
        commentText.text = taskData.row_comment;

        selectToggle.group = toggleGroup;
        selectToggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool state){
        if (state)
            OnTaskSelectEvent?.Invoke(_taskData);
    }

    private void OnDestroy() => selectToggle.onValueChanged.RemoveAllListeners();
}