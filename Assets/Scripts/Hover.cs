using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverHeight = 0.5f;
    public float interval;
    public Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.position;
    }
    
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startPos.y + Mathf.Sin(Time.time * interval * 6.28f) * hoverHeight, transform.position.z);
    }
}
