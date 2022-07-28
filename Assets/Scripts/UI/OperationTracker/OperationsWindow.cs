using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RequestParamClasses;
using UnityEngine.UI;
using UnityEngine;
using ThingData;

public class OperationsWindow : Window {

    [Header("Prefabs")]
    [SerializeField] private OperationButton operationButtonPrefab;

    [SerializeField] private ControlParamsInputWindow controlParamsInputWindow;
    [SerializeField] private DialogWindow dialogWindow;

    [Header("ContentContainers")]
    [SerializeField] private RectTransform operationButtonsParent;

    [SerializeField] private RectTransform referencesParent;
    [SerializeField] private RectTransform referencesHeaderParent;

    [Header("InformText")]
    [SerializeField] private Text operationNameText;

    [SerializeField] private Text operationDescriptionText;

    [Header("WindowControls")]
    [SerializeField] private Button acceptButton;

    [SerializeField] private ReferenceButton referencePhotoButton,referenceVideoButton, referenceDocumentButton;
    [SerializeField] private Toggle referenceCloseToggle, referenceHeaderCloseToggle;

    private readonly List<OperationButton> _operationButtonsList = new();
    private UniTaskCompletionSource<WorkLogOperationSaveCpParam[]> _operationCompletionSource;
    private Operation _currentOperation;

    private void OnEnable(){
        acceptButton.onClick.AddListener(OnAcceptClick);
        referenceCloseToggle.onValueChanged.AddListener(OnReferenceToggleChange);
        referenceHeaderCloseToggle.onValueChanged.AddListener(OnReferenceHeaderToggleChange);
    }

    public void Initialize(Operation[] operations){
        foreach (var operation in operations)
            AddOperationButton(operation);
    }
    
    public async void ShowWorkInfo(){
     //   await dialogWindow.ShowConfirmProcess()
    }

    public async UniTask<WorkLogOperationSaveCpParam[]> PerformingProcess(Operation operation){
        _currentOperation = operation;

        UpdateWindow();

        _operationCompletionSource = new UniTaskCompletionSource<WorkLogOperationSaveCpParam[]>();
        return await _operationCompletionSource.Task;
    }

    private void AddOperationButton(Operation operation){
        var item = Instantiate(operationButtonPrefab, operationButtonsParent);
        item.Initialize(operation);
        _operationButtonsList.Add(item);
    }
    
    private void UpdateWindow(){
        UpdateLabels();
        UpdateReferenceButtons();
        UpdateOperationButtonsColors();
    }

    private void UpdateOperationButtonsColors(){
        foreach (var operationButton in _operationButtonsList)
            operationButton.UpdateColor();
    }

    private void UpdateLabels(){
        operationNameText.text = _currentOperation.name;
        operationDescriptionText.text = _currentOperation.description;
    }

    private void UpdateReferenceButtons(){
        referenceDocumentButton.SetEnable(_currentOperation.url_document != null);
        referencePhotoButton.SetEnable(_currentOperation.url_image != null);
        referenceVideoButton.SetEnable(_currentOperation.url_video != null);
    }

    private async void OnAcceptClick(){
        if (_currentOperation.control_params_count > 0){
            controlParamsInputWindow.Show();
            var saveData = await controlParamsInputWindow.InputProcess(_currentOperation.ControlParams, _currentOperation.is_foto_required);
            _operationCompletionSource.TrySetResult(saveData);
            controlParamsInputWindow.ParamsListClear();
            controlParamsInputWindow.Hide();
        }
        else{
            _operationCompletionSource.TrySetResult(new[] { new WorkLogOperationSaveCpParam() });
        }
    }

    private void OnReferenceHeaderToggleChange(bool state){
        referencesParent.gameObject.SetActive(state);
    }

    private void OnReferenceToggleChange(bool state){
        referencesParent.gameObject.SetActive(state);
        referencesHeaderParent.gameObject.SetActive(state);
    }
}