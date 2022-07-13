using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour {
    public LineRenderer LineRenderer{ get; private set; }

    private void Awake(){
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void Initialize(Color color, float startWidth, float endWidth){
        LineRenderer.positionCount = 0;
        LineRenderer.startColor = color;
        LineRenderer.endColor = color;

        LineRenderer.startWidth = startWidth;
        LineRenderer.endWidth = endWidth ;
    }
}