using Cysharp.Threading.Tasks;
using ThingData;

public class TaskSelector {
    private const string REQUEST_TASK_TYPE = "REQUEST";

    private const string DAILY_ORDER_CREATE_MSG = "Создать обход на основании заявки";
    private const string DAILY_ORDER_EXECUTE_MSG_1 = "На основании заявки:";
    private const string DAILY_ORDER_EXECUTE_MSG_2 = "Успешно создан обход:";
    private const string DAILY_ORDER_EXECUTE_MSG_3 = "Приступить к выполнению?";

    private readonly ThingWorksServices _thingWorksServices;
    private readonly TaskListWindow _tasksListWindow;
    private readonly DialogWindow _dialogWindow;

    private TaskData _selectedTaskData;
    private CreatedWorkLog _createdDailyOrderData;
    private int _selectedTaskWorkLogId;

    public TaskSelector(ThingWorksServices thingWorksServices, TaskListWindow tasksListWindow, DialogWindow dialogWindow){
        _thingWorksServices = thingWorksServices;
        _tasksListWindow = tasksListWindow;
        _dialogWindow = dialogWindow;
    }

    public async UniTask<int> TaskSelectionProcess(string assetName){
        _tasksListWindow.Show();

        while (true){
            var taskList = await _thingWorksServices.RequestAndWorkLogGetView(assetName);
            _tasksListWindow.Initialize(taskList);
            _selectedTaskData = await _tasksListWindow.TaskSelectionProcess();

            if (_selectedTaskData.row_type_code == REQUEST_TASK_TYPE){
                var createDailyOrderDialogResult = await _dialogWindow.ShowConfirmProcess($"{DAILY_ORDER_CREATE_MSG} \n{_selectedTaskData.row_code}-{_selectedTaskData.row_comment}?");
                if (!createDailyOrderDialogResult) continue;

                _createdDailyOrderData = await _thingWorksServices.WorkLogCreateFromRequest(_selectedTaskData.request_id);
                _selectedTaskWorkLogId = _createdDailyOrderData.id;
            }
            else{
                _selectedTaskWorkLogId = _selectedTaskData.work_log_id;
            }

            var executeTaskDialogResult = await _dialogWindow.ShowConfirmProcess(
                $"{DAILY_ORDER_EXECUTE_MSG_1} {_selectedTaskData.row_code}-{_selectedTaskData.row_comment} \n {DAILY_ORDER_EXECUTE_MSG_2} {_selectedTaskWorkLogId} \n {DAILY_ORDER_EXECUTE_MSG_3}");

            if (executeTaskDialogResult) break;
        }

        _tasksListWindow.Hide();
        return _selectedTaskWorkLogId;
    }
}
   
