/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	#region Properties

	public string name;
	public AudioClip clip;

	internal AudioSource source;

	[Range(0f,1f)]
	public float volume;
	[Range(0.1f,3f)]
	public float pitch;
	public bool loop;
	

	#endregion

	#region Class Methods

	public void FadeVolume(float newVolume, float time)
	{
		float startingVolume = volume;
		volume = newVolume;
		source.volume = Mathf.Lerp(startingVolume ,newVolume, time);
	}

	#endregion
}