/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class CameraFollow : MonoBehaviour {

	#region Global Variables

		[SerializeField]
		private Transform objectToFollow;
		[SerializeField]
		private float movementSpeed = 5f;
		
		// This is controlled by the triggers around the level
		private float xOffset = 0.0f;
		private float yOffset = 0.0f;

	#endregion

	#region Unity Functions

		void Awake()
		{
			if(objectToFollow == null)	
			{
				Debug.LogWarning("The camera is not following the player");
			}
		}

	#endregion
	
	#region Camera Movement

		private void FixedUpdate()
		{
			Vector3 smoothPosition = Vector2.Lerp(transform.position, GetTarget(), movementSpeed * Time.fixedDeltaTime);
			smoothPosition.z = -3f;
			transform.position = smoothPosition;
		}

		private Vector2 GetTarget()
		{
			Vector2 target = objectToFollow.position;
			target.x += xOffset;
			target.y += yOffset;
			return target;
		}

		public void SetOffset(float xPos, float yPos)
		{
			xOffset = xPos;
			yOffset = yPos;
		}

	#endregion
}
