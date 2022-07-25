using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using ThingData;

public class OperationsPerformingWindow : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] private OperationButton operationButtonPrefab;

    [SerializeField] private ControlParamsInputWindow controlParamsInputWindow;
    [SerializeField] private Button referenceButtonPrefab;

    [Header("ContentContainers")]
    [SerializeField] private RectTransform operationButtonsParent;

    [SerializeField] private RectTransform referenceButtonParent;

    [Header("InformText")]
    [SerializeField] private Text operationNameText;

    [SerializeField] private Text operationDescriptionText;

    [Header("WindowControls")]
    [SerializeField] private Button acceptButton;

    private UniTaskCompletionSource<Operation> _operationCompletionSource;
    private List<OperationButton> _operationButtonList = new();
    private Operation _currentOperation;

    private void OnEnable() => acceptButton.onClick.AddListener(OnAcceptClick);

    public async UniTask<Operation> OperationPerformingProcess(Operation operation){
        _currentOperation = operation;
       
        UpdateWindowsLabels(_currentOperation);

        _operationCompletionSource = new UniTaskCompletionSource<Operation>();

        return await _operationCompletionSource.Task;
    }

    public void AddOperationButton(Operation operation){
        var item = Instantiate(operationButtonPrefab, operationButtonsParent);
        item.Initialize(operation);
        _operationButtonList.Add(item);
    }

    private void UpdateWindowsLabels(Operation operation){
        operationNameText.text = operation.name;
        operationDescriptionText.text = operation.description;
    }

    private async void OnAcceptClick(){
        if (_currentOperation.control_params_count > 0){
            controlParamsInputWindow.gameObject.SetActive(true);
            await controlParamsInputWindow.InputProcess(_currentOperation.ControlParams);
        }

        _operationCompletionSource.TrySetResult(_currentOperation);
    }
}

public enum ReferenceType {
    Image = 0,
    Document = 1,
    Video = 2,
    Cad = 3
}