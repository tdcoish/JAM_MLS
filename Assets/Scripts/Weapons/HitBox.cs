using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitBox : MonoBehaviour 
{
	
	internal float damage;
	private BoxCollider2D col;

	private void Awake()
	{
		col = GetComponent<BoxCollider2D>();		
		col.isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamageable damageeableObject = other.gameObject.GetComponent<IDamageable>();
		if(damageeableObject != null)
		{
			damageeableObject.TakeDamage(damage);
		}
	}
}
