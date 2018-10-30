using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChargeComparisons
{
	public const float TinyFloat = 0.001f;

	public static bool IsPositive(float value)
	{
		return value > TinyFloat;
	}

	public static bool IsNegative(float value)
	{
		return value < -TinyFloat;
	}

	public static bool IsZero(float value)
	{
		return Mathf.Abs(value) <= TinyFloat;
	}

	public static bool IsAttracted(float a, float b)
	{
		return (IsPositive(a) && IsNegative(b))
			|| (IsNegative(a) && IsPositive(b));
	}

	public static bool IsRepelled(this float a, float b)
	{
		return (IsPositive(a) && IsPositive(b))
			|| (IsNegative(a) && IsNegative(b));
	}

	public static bool IsInfluenced(this float a, float b)
	{
		return !(IsZero(a) || IsZero(b));
	}
}
