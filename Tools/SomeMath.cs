using UnityEngine;
using UnityEngineInternal;

public static class SomeMath
{
    public static float DistBetweenLinePoint(Vector2 linePoint1, Vector2 linePoint2, Vector2 point)
    {
        return Vector2.Distance(FindNearestPointOnLine(linePoint1, linePoint2, point), point);
    }

    public static float DistBetweenLineSegmentPoint(Vector2 linePoint1, Vector2 linePoint2, Vector2 point)
    {
        return Vector2.Distance(FindNearestPointOnLineSegment(linePoint1, linePoint2, point), point);
    }

    public static Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
    {
        direction.Normalize();
        Vector2 lhs = point - origin;

        float dotP = Vector2.Dot(lhs, direction);
        return origin + direction * dotP;
    }

    public static Vector2 FindNearestPointOnLineSegment(Vector2 origin, Vector2 end, Vector2 point)
    {
        Vector2 dir = end - origin;
        point = FindNearestPointOnLine(origin, end - origin, point);

        Vector2 v = point - origin;
        if (v.magnitude > dir.magnitude)
            return end;
        else if (v.normalized == -dir.normalized)
            return origin;
        else return point;
    }

    public static float PowerOfTwo(float x) { return x * x; } 
}

