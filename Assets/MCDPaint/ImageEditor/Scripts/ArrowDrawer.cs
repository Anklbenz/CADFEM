using System.Collections.Generic;
using UnityEngine;

public class ArrowDrawer : LineDrawer {
    private const int DIRECTION_SPOT_POINT_INDEX = 6;

    private const float ARROWHEAD_LENGTH = 0.4f;
    private const float ARROWHEAD_WIDTH = 0.4f;

    public override Line GetPinAndStop(){
        if (Line.LineRenderer.positionCount >= DIRECTION_SPOT_POINT_INDEX)
            ArrowDraw();
     
        return base.GetPinAndStop();
    }

    private void ArrowDraw(){
        var endPos = Line.LineRenderer.GetPosition(LineLastPointIndex);
        var startPos = Line.LineRenderer.GetPosition(Line.LineRenderer.positionCount - DIRECTION_SPOT_POINT_INDEX);

        var arrowheadPoints = CalculateArrowheadPoints(endPos, startPos);
        foreach (var point in arrowheadPoints)
            PointDraw(point);
    }

    private void PointDraw(Vector3 position){
        Line.LineRenderer.positionCount++;
        Line.LineRenderer.SetPosition(LineLastPointIndex, position);
    }

    private List<Vector3> CalculateArrowheadPoints(Vector3 endLinePoint, Vector3 startLinePoint){
        var points = new List<Vector3>();

        var dir = GetNormalizedLineDirection(endLinePoint, startLinePoint);
        var normal = GetNormalVector2D(dir);

        var arrowMiddlePoint = endLinePoint - dir * ARROWHEAD_LENGTH;
        var posLeft = arrowMiddlePoint + normal * ARROWHEAD_WIDTH;
        var posRight = arrowMiddlePoint - normal * ARROWHEAD_WIDTH;

        points.Add(posLeft);
        points.Add(endLinePoint);
        points.Add(posRight);

        return points;
    }

    private Vector3 GetNormalizedLineDirection(Vector3 endPos, Vector3 startPos) => (endPos - startPos).normalized;
    private Vector3 GetNormalVector2D(Vector3 vector) => new Vector3(-vector.y, vector.x, vector.z);
}