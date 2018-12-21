using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour {

	[SerializeField]
	private float healthValue;

	[Tooltip("Spawned when you pick up the health")]
	[SerializeField]
	private GameObject				_HealthParticles;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Health>().Recover(healthValue);
			Instantiate(_HealthParticles, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
