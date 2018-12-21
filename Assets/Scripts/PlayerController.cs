using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour , IDamageable {

	//region keyword acts like struct with single initialization
	#region GlobalVars

        [SerializeField]
        private float           _MoveSpeed;

		[Tooltip("Put the width and height of the world here. Bit of a hack, but it'll do.")]
		[SerializeField]
		private float 			_XRange;
		[SerializeField]
		private float			_YRange;

		[Tooltip("After taking damage you are invulnerable for this long")]
		[SerializeField]
		private float 			_ImmunityTime = 5f;

		[SerializeField]
		private GameObject		_BloodParticles;
		
		[SerializeField]
		private Crosshairs		_Crosshair;

		private bool            _IsAlive = true;
		private Collider2D[]    _Cols;

		private bool			_IsFiring = false;
		private bool			_IsFiringMG = false;
		private bool			_IsFiringShotty = false;

		// set each frame, only needed for the tail animations as of right now
		private bool			_IsMoving = false;				

        private Rigidbody2D         _RB;
		private Animator            _Anim;
		private WeaponHolder		_WH;
		private TailAnimationController		_TAnimCont;

        private float               _XVel;
		private float               _YVel;
		private Vector2				_MousePosition;

		private bool				_Invulnerable = false;

		private Health				_Health;

		private GameScreen			_GS;

    #endregion GlobalVars

	private void Awake()
	{
        _Anim = GetComponent<Animator>();
		_RB = GetComponent<Rigidbody2D>();
		_WH = GetComponentInChildren<WeaponHolder>();
		_TAnimCont = GetComponentInChildren<TailAnimationController>();
		_Health = GetComponent<Health>();
		_GS = UIManager.Instance.GetScreen<GameScreen>();
	}

    void Update()
	{
		UpdateUI();

		//disable update when dead.
		if(!_IsAlive) return;

        // Really, we could potentially just hardcode these, instead of fiddling around with the editor settings
        StoreInput();
        Move();

		RotateToMouse();

		HandleFiring();

		SetAnimations();
		_TAnimCont.SetTailAnimationState(_IsMoving);

        // Add Animation state code here
	}

	void SetAnimations(){
		_Anim.SetBool("isHurt", _Invulnerable);
		bool isFiring = _IsFiringMG | _IsFiringShotty;
		_Anim.SetBool("isFiring", isFiring);

		_IsFiring = false;
	}

    private void StoreInput()
    {
        /// WASD movement
        _XVel = 0; _YVel = 0;

		if(!GameManager.Instance.IsGamePaused()){
			if(Input.GetKey(KeyCode.A))
			{
				_XVel -= _MoveSpeed;
			}
			if (Input.GetKey(KeyCode.D))
			{
				_XVel += _MoveSpeed;
			}

			if(Input.GetKey(KeyCode.W))
			{
				_YVel += _MoveSpeed;
			}
			if(Input.GetKey(KeyCode.S))
			{
				_YVel -= _MoveSpeed;
			}

			// more audio, get first frame of state change.
			bool nowMoving = _IsMoving;
			if(_XVel!=0 || _YVel!=0){
				_IsMoving = true;
			}else{
				_IsMoving = false;
			}
		}


    }

	private void RotateToMouse(){
		if(!GameManager.Instance.IsGamePaused()){

			Camera c = Camera.main;
			Vector3 msPos = c.ScreenToWorldPoint(Input.mousePosition);
			_MousePosition = msPos;

			Vector2 distance = _MousePosition - (Vector2)transform.position;
			float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
			angle -= 90;
			transform.eulerAngles = new Vector3(0, 0, angle);
		}
	}

    private void Move(){

        // Vector2 pos = transform.position;
        // pos.x += _XVel * _MoveSpeed;
        // pos.y += _YVel * _MoveSpeed;

		Vector2 vel = new Vector2(_XVel, _YVel);
		_RB.velocity = vel;

		// If the position is out of bounds, move the player back
		// in bounds.
		Vector2 pos = transform.position;
		if(pos.x > _XRange) pos.x = _XRange; if(pos.x < -_XRange) pos.x = -_XRange;
		if(pos.y > _YRange) pos.y = _YRange; if(pos.y < -_YRange) pos.y = -_YRange;

        transform.position = pos;
	}

	// Some of this code is duplicated in Crosshairs.cs. Perhaps give us a reference to the crosshairs, and just aim towards that.
	// Also, obviously there's going to be some complicated logic here, Carlos is working on that. this is just for testing.
	private void HandleFiring(){

		bool lmb = Input.GetMouseButton(0);
		bool rmb = Input.GetMouseButton(1);

		_Crosshair.SetShooting(lmb | rmb);

		// for animations
		_IsFiring = lmb | rmb;

		if (Input.GetMouseButton(0))
		{
			// On first frame of state change, play audio.
			if(!_IsFiringMG){
				AkSoundEngine.PostEvent("Play_MachineGun", gameObject);				
			}
			_IsFiringMG = true;

			_WH.Attack(0, transform.rotation);
		}else{
			if(_IsFiringMG){
				AkSoundEngine.PostEvent("Stop_MachineGun", gameObject);				
			}
			_IsFiringMG = false;
		}

		// Now do shotgun.
		if (Input.GetMouseButtonDown(1))
		{
			if(!_IsFiringShotty){
				AkSoundEngine.PostEvent("Play_Shotgun", gameObject);				
			}
			_IsFiringShotty = true;

			_WH.Attack(1, transform.rotation);
		}else{
			_IsFiringShotty = false;

		}

	}

    public void TakeDamage(float damage)
    {
		if(!_Invulnerable){
			_Invulnerable = true;

			_Health.TakeDamage(damage);
			// play sounds, animations, change state, etcetera.
			Instantiate(_BloodParticles, transform.position, transform.rotation);

			AkSoundEngine.PostEvent("Play_Efforts", gameObject);

			Invoke("RemoveInvulnerability", _ImmunityTime);
		}
    }

	private void RemoveInvulnerability()
	{
		_Invulnerable = false;
	}

	private void UpdateUI()
	{
		_Crosshair.SetOverheat(_WH.GetWeaponStatus());
		_GS.SetHealth(_Health.GetHealth());
	}

}
