using UnityEngine;
using Cursor = UnityEngine.Cursor;

[DefaultExecutionOrder(-1)]
public class WorldCursor : MonoBehaviour
{
    public static WorldCursor Inst;
    Vector3 lastPos;
    public float leanAngle;
    
    private void Start()
    {
        Inst = this;
        Cursor.visible = false;
    }

    private void Update()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        transform.position = worldPos;
        
        var delta = worldPos - lastPos;
        lastPos = worldPos;

        var rotZ = transform.position.x * leanAngle;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.03f);

        if (Input.GetMouseButton(0))
        {
             transform.localScale = Vector3.one *0.7f;
        }
    }

    public static Vector3 Position => Inst.transform.position;
}
