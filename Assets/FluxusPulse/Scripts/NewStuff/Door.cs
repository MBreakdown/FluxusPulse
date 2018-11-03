using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Door : MonoBehaviour
{
	public Transform locationClosed;
	public Transform locationOpened;
	public Transform TargetLocation { get { return Opened ? locationOpened : locationClosed; } }

	public float speed = 5;
	public float rotateSpeed = 30;
	
	[SerializeField]
	private BoolEventState m_Opened = new BoolEventState { IsTrue = false };
	public bool Opened {
		get { return m_Opened.IsTrue; }
		set
		{
			if (stayOpen && Opened)
				return;

			m_Opened.IsTrue = value;
		}
	}

	[SerializeField]
	private bool stayOpen = false;



	public Rigidbody2D rb { get; private set; }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		rb.isKinematic = true;
		JumpTo(TargetLocation);
	}

	private void FixedUpdate()
	{
		MoveTowards(TargetLocation, Time.fixedDeltaTime);
	}

	void MoveTowards(Transform location, float deltaTime)
	{
		rb.MovePosition(Vector2.MoveTowards(rb.position, location.position, speed * deltaTime));
		rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, location.eulerAngles.z, rotateSpeed * deltaTime));
	}

	void JumpTo(Transform location)
	{
		rb.position = location.position;
		rb.rotation = location.eulerAngles.z;
	}
}
//~ class
