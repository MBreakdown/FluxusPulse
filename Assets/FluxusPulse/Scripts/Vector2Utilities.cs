using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utilities
{
    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

    public static float CalculateArcAngle(float radius, float arcLength)
    {
        return Mathf.Rad2Deg * (radius * arcLength);
    }
}
