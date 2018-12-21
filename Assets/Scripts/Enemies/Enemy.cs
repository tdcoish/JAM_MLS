using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MonoBehaviour, IDamageable, IPetrify
{


    #region GlobalVars
        private Health health;

        [SerializeField]
        private GameObject          _StonePrefab;

        [SerializeField]
        private float           	_MoveSpeed;

		[Tooltip("When the enemy is within this distance to the player they will try to chuck spears at the player")]
		[SerializeField]
		private float				_DistanceToAttack;

		[Tooltip("This is the time it takes for the melee enemy to start attack 2 after attack 1 has started.")]
		[SerializeField]
		private float				_TimeBetweenAttacks;

		[Tooltip("Upon death, we swap in a version of ourselves that is a dead enemy")]
		[SerializeField]
		private GameObject			_DeadVersion;

		[Tooltip("Enemies get rotated just a bit when they die. Choose how much for this enemy")]
		[SerializeField]
		private float				_RotationAmount;

		[SerializeField]
		private GameObject		_BloodParticles;

		[SerializeField]
		private bool			_MeleeType;

		private PlayerController	_PlayerReference;

        private Rigidbody2D         _RB;
		private Animator            _Anim;
		private WeaponHolder		_WH;

		private FeetAnimationController			_FeetAnim;		

        private float               _XVel;
		private float               _YVel;

		private bool				_Attacking;
		private bool				_Hurt = false;
		private bool				_Alive = true;

        // Mainly for animations, there's a little break here in between attacks, so we always idle for at least a bit.
        private bool                _CanAttack = true;

    #endregion GlobalVars

    
    private void Awake()
    {
        health = GetComponent<Health>();
        _Anim = GetComponent<Animator>();
		_RB = GetComponent<Rigidbody2D>();
		_WH = GetComponentInChildren<WeaponHolder>();

		_FeetAnim = GetComponentInChildren<FeetAnimationController>();

		// Get a reference to the player character in the scene on awake
		_PlayerReference = FindObjectOfType<PlayerController>();
        EnemyAwake();
    }

    protected abstract void EnemyAwake();

    void Update()
    {
        RotateToPlayer();
        Move();
		HandleAttacking();
        // Add Animation state code here
		_FeetAnim.SetFeetAnimationState(_Attacking);
		SetUpperBodyAnimationState();

		
    }

	public void SpawnDeadObj(){
		// Make the enemies corpse spawn in a semi-random location
		float randomSwing = Random.Range(0, _RotationAmount) - _RotationAmount/2f;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z += randomSwing;
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

		// Also instantiate a scream
		AkSoundEngine.PostEvent("Play_Death_Screams", gameObject);

		Instantiate(_DeadVersion, transform.position, transform.rotation);
	}

    public void Petrify()
    {
        Instantiate(_StonePrefab, transform.position, transform.rotation);
        EnemySpawner.enemies.Remove(this);
        GameManager.Instance.EnemyKilled(false);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
		health.TakeDamage(damage);

		Instantiate(_BloodParticles, transform.position, transform.rotation);		
    }

    private void SetUpperBodyAnimationState(){
		_Anim.SetBool("isAlive", _Alive);		//change later
		_Anim.SetBool("isAttacking", _Attacking);
		_Anim.SetBool("isHurt", _Hurt);
	}


    // we might not have any trigger volumes in the game, if we do handle them here
	private void OnTriggerEnter2D(Collider2D other)
	{ 
	}

	private void RotateToPlayer(){
		Vector2 distance = _PlayerReference.transform.position - transform.position;
		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		angle-=90;
		transform.eulerAngles = new Vector3(0, 0, angle);
	}

    private void Move(){
		if(!_Attacking){
			_RB.velocity = _MoveSpeed * transform.up;
		}else{
			Vector2 vel = new Vector2(0.0f, 0.0f);
			_RB.velocity = vel;
		}
	}

	// If you aren't attacking, then you can do something cool, like pause and swipe at the player 
	private void HandleAttacking(){

		if(!_Attacking && _CanAttack){
			if(Vector2.Distance(_PlayerReference.transform.position, transform.position) < _DistanceToAttack){
				_Attacking = true;

				// Play Animation
				// Spawn collision2d to check for player hit.
				// do other things I'm sure.
				// Integrate with the Weapon System Carlos wrote.
				_WH.Attack(0, transform.rotation);

				// play different sound if melee or ranged.
				if(_MeleeType){
					AkSoundEngine.PostEvent("Play_Sword_Swings", gameObject);
				}else{
					AkSoundEngine.PostEvent("Play_Throw", gameObject);
				}

				// After x seconds, you can attack again.
				Invoke("RecoverSwipe", _TimeBetweenAttacks);
			}

		}
	}

	private void RecoverSwipe(){
		_Attacking = false;
        _CanAttack = false;
        Invoke("CanAttackAgain", 0.1f);
	}

    private void CanAttackAgain(){
        _CanAttack = true;
    }
}