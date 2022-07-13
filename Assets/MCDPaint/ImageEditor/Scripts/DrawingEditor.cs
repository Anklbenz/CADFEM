using System.Collections.Generic;
using UnityEngine;

public class DrawingEditor : MonoBehaviour {
    private const int LINES_POOL_AMOUNT = 10;

    [SerializeField] private Line linePrefab;
    [SerializeField] private InputHandler input;
    [SerializeField] private DrawingUIController uiController;

    [Header("Canvas")]
    [SerializeField] private Camera renderCamera;

    [SerializeField] private LayerMask drawingCanvasLayerMask;

    private LineDrawer _lineDrawer;
    private ArrowDrawer _arrowDrawer;
    private LineDrawer _activeDrawer;

    private Line _line;
    private Stack<Line> _undoStack;
    private bool _drawing;
    private PoolObjects<Line> _linesPool;

    private ColorPicker _colorPicker;
    private SizeSetter _sizeSetter;

    private Color _color;
    private float _startSize, _endSize;


    private void Awake(){
        _lineDrawer = new LineDrawer();
        _arrowDrawer = new ArrowDrawer();

        _undoStack = new Stack<Line>();
        _linesPool = new PoolObjects<Line>(linePrefab, LINES_POOL_AMOUNT, this.transform, true);

        _colorPicker = uiController.colorPicker;
        _sizeSetter = uiController.sizeSetter;

        input.OnLeftMouseButtonEvent += OnDrawInputStarts;
        input.OnLeftMouseButtonUpEvent += OnDrawInputEnds;

        _colorPicker.OnColorSampledEvent += SetLineColor;
        _sizeSetter.OnBrushSizeChangedEvent += SetLineSize;


        uiController.OnUndoTapEvent += DoUndo;
        uiController.OnLineTapEvent += SetLineTool;
        uiController.OnArrowTapEvent += SetArrowTool;
    }


    private void OnDestroy(){
        input.OnLeftMouseButtonEvent -= OnDrawInputStarts;
        input.OnLeftMouseButtonUpEvent -= OnDrawInputEnds;

        _colorPicker.OnColorSampledEvent -= SetLineColor;
        _sizeSetter.OnBrushSizeChangedEvent -= SetLineSize;


        uiController.OnUndoTapEvent -= DoUndo;
        uiController.OnLineTapEvent -= SetLineTool;
        uiController.OnArrowTapEvent -= SetArrowTool;
    }

    private void OnDrawInputStarts(){
        if (_activeDrawer == null) return;

        var touchPos = ScreenToCanvasPosition(input.MousePosition);
        if (touchPos == null) return;

        _drawing = true;

        if (_activeDrawer.IsNewLine){
            _line = _linesPool.GetFreeElement();
            _line.Initialize(_color, _startSize, _endSize);
        }

        _activeDrawer.SetPin(_line);
        _activeDrawer.Draw((Vector3)touchPos);
    }

    private void OnDrawInputEnds(){
        if (!_drawing) return;
        _drawing = false;

        _undoStack.Push(_activeDrawer.GetPinAndStop());
    }

    private void SetLineTool() => _activeDrawer = _lineDrawer;
    private void SetArrowTool() => _activeDrawer = _arrowDrawer;

    private void SetLineColor(Color color) => _color = color;

    private void SetLineSize(float size){
        _endSize = size;
        _startSize = size; // * some percent of %
    }

    private void DoUndo(){
        if (_undoStack.Count == 0) return;

        var line = _undoStack.Pop();
        line.gameObject.SetActive(false);
    }

    private Vector3? ScreenToCanvasPosition(Vector2 touchPos){
        const float rayLength = 100f;
        var ray = renderCamera.ScreenPointToRay(touchPos);

        if (Physics.Raycast(ray, out var raycastHit, rayLength, drawingCanvasLayerMask))
            return raycastHit.point;

        return null;
    }
}