using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn : MonoBehaviour
{
	[Range(1,2)]
	public int playerIndexToFollow = 1;
	public EnemyScript prefab; 
	public int Count =1;


	// Use this for initialization
	void Start()
    {
		for (int i = 0; i < Count; i++)
        {
			EnemyScript clone = Instantiate(prefab, this.transform.position, this.transform.rotation);
			clone.name = prefab.name;
            clone.playerIndexToFollow = playerIndexToFollow;
		}
	}
}
//~ class
