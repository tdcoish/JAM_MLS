using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	[Tooltip("Crowd moves back and forth on this interval")]
	[SerializeField]
	private float				_MoveInterval;

	private float				_SavedTime = 0;

	[Tooltip("Distance the crowd 'jumps' per movement")]
	[SerializeField]
	private float 				_MoveDis;

	[SerializeField]
	private bool				_MoveUp;			//because sides move in x, top/bot move in y

	private bool				_lastMoveUp;		// go the other way the next time.

	private CrowdPerson[]		_CrowdPeople;

	// Use this for initialization
	void Awake () {
		_CrowdPeople = GetComponentsInChildren<CrowdPerson>();
		// AkSoundEngine.PostEvent("Play_Audience_Loop_1", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		// when time limit has been reached, move the crowd back or forth.
		_SavedTime += Time.deltaTime;

		if(_SavedTime > _MoveInterval){
			
			for(int i=0; i<_CrowdPeople.Length; i++){

				Vector2 pos = _CrowdPeople[i].transform.position;

				if(_lastMoveUp){
					pos.y += _MoveDis;
				}else{
					pos.y -= _MoveDis;
				}

				_CrowdPeople[i].transform.position = pos;

				_SavedTime = 0;
			}
			_lastMoveUp = !(_lastMoveUp);
		}
	}
}
