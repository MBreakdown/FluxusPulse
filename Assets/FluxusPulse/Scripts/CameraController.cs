/***********************************************************************
*	Bachelor of Software Engineering
*	Media Design School
*	Auckland
*	New Zealand
*
*	(c) 2018 Media Design School
*
*	File Name	:	CameraController.cs
*	Description	:	Follows one or more objects, keeping all of them in view by zooming in and out.
*	Project		:	FluxusPulse
*	Team Name	:	M Breakdown Studios
*	Author		:	Elijah Shadbolt
*	Mail		:	elijah.sha7979@mediadesign.school.nz
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Obsolete("Camera should be fixed position and size.")]
[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
	#region Public



	// Types

	public enum FollowMode
	{
		CentreAverage,
		CentreBounds,
	}
	//~ enum



	// Inspector Fields

	[Tooltip("Camera to zoom.")]
	public Camera cam;

	[Tooltip("This script will follow the positions of these targets.")]
	public List<Transform> targetsToFollow;

	[Tooltip("How to calculate the position.")]
	public FollowMode followMode = FollowMode.CentreBounds;

	[Tooltip("If true, the position and zoom changes will be smoothed.")]
	public bool smoothZoom = false;

	[Tooltip("Smaller value means slower/smoother change.")]
	[Range(0f, 1f)]
	public float smoothZoomLerp = 0.3f;

	[Tooltip("Minimum world units between any target and the screen edge.")]
	public float padding = 3f;

	[Tooltip("Scale factor of the calculated zoom. A value of 1 means the targets "
		+ "will be at the edge of the screen (before padding).")]
	public float zoomScale = 1f;



	#endregion Public
	#region Private



	// Unity Event Methods

	void Awake()
	{
		if (!cam)
			Debug.LogError("Camera is not assigned.", this);
	}
	//~ fn

	void Start()
	{
		m_initialZ = transform.position.z;
	}
	//~ fn

	void Update()
	{
		if (targetsToFollow == null)
			return;

		IEnumerable<Transform> targets = targetsToFollow.Where(target => (target));
		if (targets.Count() <= 0)
			return;

		// Aggregate all the target positions.
		Vector3 sum = Vector3.zero;
		Vector3 min = targets.First().position;
		Vector3 max = min;
		int count = 0;
		foreach (Transform target in targets)
		{
			Vector3 pos = target.position;
			sum += pos;
			count++;
			min = Vector3.Min(min, pos);
			max = Vector3.Max(max, pos);
		}

		// Calculate position in the centre of all the targets.
		Vector3 centre;
		if (followMode == FollowMode.CentreAverage)
		{
			centre = sum / count;
		}
		else
		{
			centre = (min + max) / 2;
		}
		centre.z = m_initialZ;

		if (smoothZoom)
		{
			transform.position = Vector3.Lerp(transform.position, centre, smoothZoomLerp);
		}
		else
		{
			transform.position = centre;
		}

		// Calculate camera scale.
		float height = Mathf.Abs(min.y - max.y);
		float width = Mathf.Abs(min.x - max.x);
		float targetSize = Mathf.Max(height, width / cam.aspect) * (zoomScale / 2f) + padding;

		if (smoothZoom)
		{
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smoothZoomLerp);
		}
		else
		{
			cam.orthographicSize = targetSize;
		}
	}
	//~ fn



	// Private Fields

	private float m_initialZ;



	#endregion Private
}
//~ class
