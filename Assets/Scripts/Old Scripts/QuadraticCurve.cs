using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticCurve : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;

    int newPoints = 50;
    Vector3[] positions = new Vector3[50];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = newPoints;
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {
        for (int i = 1; i < newPoints + 1; i++)
        {
            float t = i / (float)newPoints;
            positions[i - 1] = CalculateBezierCurvePoints(t, point0.position, point1.position, point2.position);
        }
        lineRenderer.SetPositions(positions);
    }

    Vector3 CalculateBezierCurvePoints(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
        }
}
