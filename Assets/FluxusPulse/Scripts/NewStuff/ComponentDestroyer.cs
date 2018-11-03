using UnityEngine;

public class ComponentDestroyer : MonoBehaviour
{
	public Object[] objectsToDestroy;
	public float delay = 0.0f;
	public bool destroyOnAwake = false;

	public void DestroyObjects()
	{
		if (delay != 0.0f)
		{
			foreach (var obj in objectsToDestroy)
			{
				Destroy(obj, delay);
			}
		}
		else
		{
			foreach (var obj in objectsToDestroy)
			{
				Destroy(obj);
			}
		}
	}

	private void Awake()
	{
		if (destroyOnAwake)
		{
			DestroyObjects();
		}
	}
}
