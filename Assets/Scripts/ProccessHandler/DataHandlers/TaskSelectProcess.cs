using Cysharp.Threading.Tasks;
using ServiceRequestsData;
using ThingData;

public class TaskSelectProcess {
    private const string GET_TASKS_SERVICE = "RequestAndWorkLog_GetView";

    private readonly WebRequestSender _webRequestSender;
    private readonly TaskListWindow _tasksListWindow;

    public TaskSelectProcess(WebRequestSender webRequestSender, TaskListWindow tasksListWindow){
        _webRequestSender = webRequestSender;
        _tasksListWindow = tasksListWindow;
    }

    public async UniTask<TaskData> SelectTask(string assetName){
        var taskListWindow = await Initialize(assetName);
        var selectedTaskId = await taskListWindow.TaskSelectionProcess();
        TasksSelectWindowClose();
        return selectedTaskId;
    }

    private async UniTask<TaskListWindow> Initialize(string assetName){
        var assetNameParam = CreateAssetNameParam(assetName);
        var tasksList = await _webRequestSender.GetServiceResult<TasksList, AssetName>(GET_TASKS_SERVICE, assetNameParam);
        return ProvideTasksSelectWindow(tasksList);
    }

    private TaskListWindow ProvideTasksSelectWindow(TasksList taskList){
        foreach (var task in taskList.rows)
            _tasksListWindow.Add(task);

        _tasksListWindow.gameObject.SetActive(true);

        return _tasksListWindow;
    }

    private void TasksSelectWindowClose() => _tasksListWindow.gameObject.SetActive(false);
    private AssetName CreateAssetNameParam(string assetName) => new AssetName() { thing = assetName };
}
