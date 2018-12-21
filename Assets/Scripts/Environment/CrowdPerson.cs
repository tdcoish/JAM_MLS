using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just used as a hook to call GetComponentsInChildren.

public class CrowdPerson : MonoBehaviour {

    [Tooltip("Dude jimmies back and forth on this interval")]
	[SerializeField]
	private float				_MoveInterval = 0.3f;

	private float				_SavedTime = 0;

	[Tooltip("Distance the crowd 'jumps' per movement")]
	[SerializeField]
	private float 				_MoveDis = 0.2f;

	[SerializeField]
	private bool				_MoveUp;			//because sides move in x, top/bot move in y

    private bool                _SideToSide;

	private bool				_lastMoveUp;		// go the other way the next time.

    private void Awake(){
        _MoveInterval += Random.Range(0, _MoveInterval/2);

        // Total hack, but I measured the units, and this works.
        if(transform.position.x > 13 || transform.position.x < -13){
            _SideToSide = false;
        }else{
            _SideToSide = true;
        }
    }
	
    // Update is called once per frame
	void Update () {
		// when time limit has been reached, move the crowd back or forth.
		_SavedTime += Time.deltaTime;

		if(_SavedTime > _MoveInterval){

            Vector2 pos = transform.position;

            if(_SideToSide){
                if(_lastMoveUp){
                    pos.x += _MoveDis;
                }else{
                    pos.x -= _MoveDis;
                }
            }else{
                if(_lastMoveUp){
                    pos.y += _MoveDis;
                }else{
                    pos.y -= _MoveDis;
                }
            }

            transform.position = pos;

            _SavedTime = 0;
			_lastMoveUp = !(_lastMoveUp);
		}
	}

}
