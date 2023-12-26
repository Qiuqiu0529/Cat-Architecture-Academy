using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBox : MonoBehaviour
{
    LineRenderer line;
    public float cellSize;
    void Start()
    {
        List<Vector3> points = new List<Vector3>();

        points.Add(transform.position + new Vector3(0, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 1) * cellSize);
        points.Add(transform.position + new Vector3(0, 0, 1) * cellSize);

        points.Add(transform.position + new Vector3(0, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 1) * cellSize);
        points.Add(transform.position + new Vector3(0, 0, 1) * cellSize);

        points.Add(transform.position + new Vector3(0, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 0) * cellSize);
        points.Add(transform.position + new Vector3(1, 0, 1) * cellSize);
        points.Add(transform.position + new Vector3(0, 0, 1) * cellSize);

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
