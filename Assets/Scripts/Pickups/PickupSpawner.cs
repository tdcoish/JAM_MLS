using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************
* This class will spawn health after a certain amount of kills, decided by in the 
* GameManager script.
***************************************************** */

public class PickupSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject healthPickup;

	[SerializeField]
	private Transform[] healthPickupLocations;

	private PlayerController			_PlayerReference;

	[Tooltip("Pickups spawn within a box around the spawn point. This controls how large that box is")]
	[SerializeField]
	private float			_Radius;

	private void Awake(){
		_PlayerReference = FindObjectOfType<PlayerController>();
	}

	public void SpawnPickup()
	{
		// Get all 4 positions
		// Find distance to all 4
		// Spawn a health pickup there, in a semi-random spot.
		float[] distances = new float[4];
		for(int i=0; i<4; i++){
			Vector2 pos = healthPickupLocations[i].transform.position;
			distances[i] = Vector2.Distance(pos, _PlayerReference.transform.position);
		}

		// get the index of the furthest away.
		int longest = 0;
		for(int i=0; i<4; i++){
			if(distances[i] > distances[longest]){
				longest = i;
			}
		}

		// shift position to spawn from in a random, but small, radius.
		Vector2 curPos = healthPickupLocations[longest].transform.position;

		curPos.x += Random.Range(0, _Radius); curPos.x -= (_Radius/2.0f);
		curPos.y += Random.Range(0, _Radius); curPos.y -= (_Radius/2.0f);	
		Instantiate(healthPickup, (Vector3)curPos, transform.rotation);	
	}
}
