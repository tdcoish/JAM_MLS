using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable {

	[SerializeField]
	private float health = 100f;

	[SerializeField]
	private float maxHealth = 100f;

	[SerializeField]
	private bool autoDestroy = true;

	private void Awake()
	{
		ClampHealth();
	}

	private void Update()
	{
		if(health == 0 && autoDestroy)
		{
			Enemy e = gameObject.GetComponent<Enemy>();
			if(e != null)
			{
				EnemySpawner.enemies.Remove(e);
				GameManager.Instance.EnemyKilled(true);
				e.SpawnDeadObj();
			}

			Destroy(gameObject);
		}
	}
    
	public void TakeDamage(float damage)
    {
		health -= damage;
		if(health <= 0)
		{
			health = 0;
		}

		StonedEnemy stoner = gameObject.GetComponent<StonedEnemy>();
		if(stoner != null){
			stoner.SpawnHitParticles();
		}
    }

	public void Recover(float recoveredHealth)
	{
		health += recoveredHealth;
		ClampHealth();
	}

	public float GetHealth()
	{
		return health;
	}

	private void ClampHealth()
	{
		if(health > maxHealth)
		{
			health = maxHealth;
		}
	}
}
