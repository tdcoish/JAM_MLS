using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
* NOTE: This class must absolutely be attached to a gameobject that 
* is a child of Medusa. Medusa passes into this her moving/notmoving state.
************************************************************ */
public class FeetAnimationController : MonoBehaviour {

	private Animator            _Anim;
	
	private void Awake()
	{
        _Anim = GetComponent<Animator>();
	}

    // If we're attacking, we can't be moving, so play the idle animation instead.
	public void SetFeetAnimationState(bool isAttacking){
        if(isAttacking){
            _Anim.SetBool("isMoving", false);
        }else{
            _Anim.SetBool("isMoving", true);
        }
	}
}
