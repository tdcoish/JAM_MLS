using UnityEngine;

public class SnakeRing : MonoBehaviour 
{

	#region Rotate Variables

		private Rigidbody2D rb2d;
		private bool shooting;
		[SerializeField]
		private float addedAngularSpeed = 30f;
		[SerializeField]
		private float maxAngularSpeed = 1000f;
		[SerializeField]
		private float slowDownSpeed = 10f;

	#endregion

	// Use this for initialization
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate()
	{
		Shoot();
	}

	public void SetShooting(bool isShooting)
	{
		shooting = isShooting;
	}

	private void Shoot()
	{
		if(shooting)
		{			
			if(rb2d.angularVelocity > -maxAngularSpeed)
			{
				rb2d.AddTorque(-addedAngularSpeed, ForceMode2D.Force);
			}
		}
		else
		{
			rb2d.angularVelocity = Mathf.Lerp(rb2d.angularVelocity, 0, slowDownSpeed * Time.deltaTime);
		}
	}
}
