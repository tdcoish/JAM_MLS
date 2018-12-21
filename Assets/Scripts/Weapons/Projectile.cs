using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Projectile : MonoBehaviour 
{
	[SerializeField]
	private float speed = 10f;
	[SerializeField]
	private float damage = 10f;
	[SerializeField]
	private GameObject deathParticles;

	// It is okay to not have this, the game won't crash, we just won't get our behaviour.
	[SerializeField]
	private GameObject		_TrailParticles;

	private CapsuleCollider2D col;

	private void Awake()
	{
		col = GetComponent<CapsuleCollider2D>();
		col.isTrigger = true;
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamageable damageableObject = other.gameObject.GetComponent<IDamageable>();
		if(damageableObject != null)
		{
			damageableObject.TakeDamage(damage);

			_TrailParticles.transform.parent = null;
			_TrailParticles.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
			Destroy(_TrailParticles, 2.0f);

			// TODO: Could also call object to play audio clip here, for now just destroy
			Destroy(gameObject);
		}
	}
}
