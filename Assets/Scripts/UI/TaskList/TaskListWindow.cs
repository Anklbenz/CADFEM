using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ThingData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListWindow : Window {
    [SerializeField] private TMP_Text loginData;
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private TaskListItem itemPrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Button confirmButton;

    private UniTaskCompletionSource<TaskData> _selectedTaskIdCompletionSource;
    private  List<TaskListItem> _taskListItems = new();
    private TaskData _selectedTask;

    public async UniTask<TaskData> TaskSelectionProcess(){
        _selectedTaskIdCompletionSource = new UniTaskCompletionSource<TaskData>();
        return await _selectedTaskIdCompletionSource.Task;
    }

    public void Initialize(TaskData[] taskList){
        if (_taskListItems.Count > 0)
            RemoveObsoleteItems();

        foreach (var task in taskList)
            AddListItem(task);
    }

    private void AddListItem(TaskData task){
        var item = Instantiate(itemPrefab, contentParent);
        item.Initialize(task, toggleGroup);
        item.OnTaskSelectEvent += SelectTask;
        item.OnTaskSelectEvent += SetConfirmButtonInteractable;
        
        _taskListItems.Add(item);
    }

    private void RemoveObsoleteItems(){
        foreach (var item in _taskListItems)
            Destroy(item.gameObject);

        _taskListItems.Clear();
    }

    private void OnConfirmClicked(){
        if (_selectedTask == null) return;

        if (!_selectedTaskIdCompletionSource.TrySetResult(_selectedTask))
            Debug.Log("Try not Success");
    }

    private void SelectTask(TaskData task) => _selectedTask = task;
    private void SetConfirmButtonInteractable(TaskData task) => confirmButton.interactable = task != null;
    private void OnEnable() => confirmButton.onClick.AddListener(OnConfirmClicked);
    private void OnDisable() => confirmButton.onClick.RemoveListener(OnConfirmClicked);
}