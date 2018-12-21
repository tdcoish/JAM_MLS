/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class LightBlink : MonoBehaviour {

	#region Global Variables
		
		private SpriteRenderer sr;

		[SerializeField, Range(0.0f, 10.0f), Tooltip("Use flash speed 0 to make the light static")]
		private float flashSpeed = 10.0f;
		[SerializeField, Range(0.0f, 1.0f)]
		private float maxIntensity = 1.0f;
		[SerializeField, Range(0.0f, 1.0f)]
		private float minIntensity = 0.0f;

		private float intensity = 0.0f;
		private bool turningOn = true;
	
	#endregion

	#region Unity Funcitons

		void Awake () {
			sr = GetComponent<SpriteRenderer>();
		}
		
		void Update () 
		{ 
			Blink();
		}
	
	#endregion

	#region Light Blink

		private void Blink()
		{
			if(turningOn)
			{
				intensity += Time.deltaTime * MapValue(flashSpeed);
				if(intensity >= maxIntensity)
				{
					turningOn = !turningOn;
				}
			}
			else 
			{
				intensity -= Time.deltaTime * MapValue(flashSpeed);
				if(intensity <= minIntensity)
				{
					turningOn = !turningOn;
				}
			}
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, intensity);
		}

		private float MapValue(float value)
		{
			return value - minIntensity / maxIntensity - minIntensity;
		}

	#endregion
}
