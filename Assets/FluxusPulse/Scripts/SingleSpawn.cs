using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawn : MonoBehaviour {
	[Range(1,2)]
	public int playerIndexToFollow = 1;
	public EnemyScript prefab; 
	public int Count =1;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < Count; i++) {
			var clone = Instantiate<EnemyScript> (prefab, this.transform.position, this.transform.rotation);
			clone.name = prefab.name;
			clone.playerToFollow = PlayerShip.GetPlayer (playerIndexToFollow).transform;
			clone.playerToAvoid = (playerIndexToFollow == 1 ? PlayerShip.GetPlayer2 : PlayerShip.GetPlayer1).transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
