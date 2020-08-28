using UnityEngine;

public class CurveLineRenderer
{
    private static Vector3 BezierCurveFunction(Transform[] cpoints, float t)
    {     
        return (Mathf.Pow(1 - t, 4) * cpoints[0].position) +
             (4 * t * Mathf.Pow(1 - t, 3) * cpoints[1].position) +
             (6 * Mathf.Pow(t, 2) * Mathf.Pow(1 - t, 2) * cpoints[2].position) +
             (4 * Mathf.Pow(t, 3) * (1 - t) * cpoints[3].position) +
             (Mathf.Pow(t, 4) * cpoints[4].position);
    }
    public static void SetCurvePositions(LineRenderer curve, Transform[] points)
    {
        if (points.Length<5) //we agreed on 5 control points. 2 for start end 3 for middle 
        {
            Debug.Log("U Fuckin dumb come here u see why");
            return;
        }

        int quality = curve.positionCount;
        for (int i = 0; i < quality; i++)
        {
            float t = (float)i / (quality-1);
            Vector3 destenation = Vector3.Lerp(curve.GetPosition(i), BezierCurveFunction(points, t),0.1f );
            curve.SetPosition(i, destenation);
        }
    }
}
