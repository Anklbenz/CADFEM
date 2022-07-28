using System;
using ThingData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItem : MonoBehaviour {
    [SerializeField] private TMP_Text typeText, operationIdText, statusText, commentText;
    [SerializeField] private Image highlightBackground;
    [SerializeField] private Toggle selectToggle;

    public event Action<TaskData> OnTaskSelectEvent;
    private TaskData _taskData;

    public void Initialize(TaskData taskData, ToggleGroup toggleGroup){
        _taskData = taskData;

        typeText.text = taskData.row_type_name;
        operationIdText.text = taskData.row_code;
        statusText.text = taskData.row_status_name;
        commentText.text = taskData.row_comment;

        if (ColorUtility.TryParseHtmlString(taskData.row_status_color, out var color))
            highlightBackground.color = color;
        

        selectToggle.group = toggleGroup;
        selectToggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool state){
        OnTaskSelectEvent?.Invoke(state ? _taskData : null);
    }

    private void OnDestroy() => selectToggle.onValueChanged.RemoveAllListeners();
}