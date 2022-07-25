using Cysharp.Threading.Tasks;
using ThingData;
using UnityEngine;
using UnityEngine.UI;

public class TaskListWindow : MonoBehaviour {
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private TaskListItem itemPrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Button confirmButton;

    private UniTaskCompletionSource<TaskData> _selectedTaskIdCompletionSource;
    private TaskData _selectedTask;

    public async UniTask<TaskData> TaskSelectionProcess(){
        _selectedTaskIdCompletionSource = new UniTaskCompletionSource<TaskData>();
        return await _selectedTaskIdCompletionSource.Task;
    }
    public void Add(TaskData task){
        var item = Instantiate(itemPrefab, contentParent);
        item.Initialize(task, toggleGroup);
        item.OnTaskSelectEvent += GetSelectedTaskId;
    }

    private void GetSelectedTaskId(TaskData task){
        _selectedTask = task;
    }

    private void OnConfirmClicked(){
        if (_selectedTask == null) return;

        if (!_selectedTaskIdCompletionSource.TrySetResult(_selectedTask))
            Debug.Log("Try not Success");
    }

    private void OnEnable() => confirmButton.onClick.AddListener(OnConfirmClicked);
    private void OnDisable() => confirmButton.onClick.RemoveListener(OnConfirmClicked);
}
