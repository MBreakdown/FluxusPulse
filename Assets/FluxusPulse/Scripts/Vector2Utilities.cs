using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utilities
{
    public static Vector2 Rotate(Vector2 v, float radians)
    {
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

    public static float CalculateArcAngle(float radius, float arcLength)
    {
        return arcLength / radius;
    }

    public static Vector2 Perpendicular(Vector2 vector)
    {
        return new Vector2(-vector.y, vector.x);
    }
}
