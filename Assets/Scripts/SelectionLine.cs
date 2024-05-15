using UnityEngine;

public class SelectionLine : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float stepDistance = 0.1f;
    LineRenderer lineRenderer;
    public float height = 10f;
    public float offsetSpeed;
    public Transform arrowHead;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if( startPoint == null || endPoint == null) return;
         ConstructLine( startPoint.position + Vector3.up * 1.3f, endPoint.position);
         

         lineRenderer.material.mainTextureOffset = new Vector2(offsetSpeed * Time.time,0);
         arrowHead.transform.position = endPoint.position;
         var count = lineRenderer.positionCount;
         var dir = lineRenderer.GetPosition(count - 1) - lineRenderer.GetPosition(count - 2);
         dir.Normalize();
         arrowHead.transform.right = dir;
    }

    void ConstructLine(Vector3 start, Vector3 end)
    {
        if( start == end) return;
        
        var dist = Vector3.Distance(start, end);
        var parts = dist / stepDistance;
        lineRenderer.positionCount = (int) parts + 1;
        var dx = end.x - start.x;
        dx = Mathf .Abs(dx);

        for (int i = 0; i < parts; i++)
        {
            var t = (float) i / parts;
            var pos = Vector3.Lerp(start, end, t);
            // line goes in parabola shape
            t-=0.5f;
            t *= 2;
            pos.y += (1 - t * t) * height * dx/5;
            lineRenderer.SetPosition(i, pos);
        }
    }
}
