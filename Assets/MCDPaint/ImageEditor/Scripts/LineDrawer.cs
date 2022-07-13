using UnityEngine;

public class LineDrawer {
    public bool IsNewLine => Line == null;
    protected int LineLastPointIndex => Line.LineRenderer.positionCount - 1;

    protected Line Line;

    public void Draw(Vector3 position){

        if (IsDuplicatePosition(position)) return;

        Line.LineRenderer.positionCount++;
        Line.LineRenderer.SetPosition(LineLastPointIndex, position);
    }

    private bool IsDuplicatePosition(Vector3 position){
        if (Line.LineRenderer.positionCount <= 0) return false;
        var lastElementPosition = Line.LineRenderer.GetPosition(Line.LineRenderer.positionCount - 1);

        return position == lastElementPosition;
    }
    public void SetPin(Line lineRenderer) => Line = lineRenderer;

    public virtual Line GetPinAndStop(){
        var line = Line;
        Line = null;
        return line;
    }

}
