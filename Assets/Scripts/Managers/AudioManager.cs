/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : SingletonBehaviour<AudioManager>
{
	#region Properties

		[SerializeField]
		private Sound[] sounds;

	#endregion

	#region Unity Functions

	protected override void SingletonAwake()
    {
		foreach(Sound sound in sounds)
		{
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
		}
    }

	#endregion

	#region Sound / Music Handling Methods

	private Sound FindSound(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null)
		{
			Debug.LogWarning("Audio " + name + " was not found");
		}
		return s;
	}
	
	public bool IsPlaying(string name)
	{
		Sound sound = FindSound(name);
		if(sound != null)
		{
			return sound.source.isPlaying;
		}
		else 
		{
			return false;		
		}
	}

	
	public void Play(string name)
	{
		Sound sound = FindSound(name);
		if(sound != null)
		{
			sound.source.Play();
		}
	}

	public void Stop(string name)
	{
		Sound sound = FindSound(name);
		if(sound != null)
		{
			sound.source.Stop();
		}
	}

	public void FadeSound(string name, float volume, float time)
	{
		Sound sound = FindSound(name);
		if(sound != null)
		{
			sound.FadeVolume(volume, time);
		}
	}

    #endregion
}