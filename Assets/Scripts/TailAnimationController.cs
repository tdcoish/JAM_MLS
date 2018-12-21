using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
* NOTE: This class must absolutely be attached to a gameobject that 
* is a child of Medusa. Medusa passes into this her moving/notmoving state.
************************************************************ */
public class TailAnimationController : MonoBehaviour {

	private Animator            _Anim;

	private bool				_currentlyMoving = false;
	
	private void Awake()
	{
        _Anim = GetComponent<Animator>();
	}

	public void SetTailAnimationState(bool isMoving){
		_Anim.SetBool("isMoving", isMoving);
		if(isMoving){
			if(!_currentlyMoving){
				AkSoundEngine.PostEvent("Play_Medusa_Movment_Loop", gameObject);
				_currentlyMoving = true;
			}
		}else{
			// stop
			AkSoundEngine.PostEvent("Stop_Medusa_Movment_Loop", gameObject);
			_currentlyMoving = false;
		}
	}
}
