using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour 
{

	[SerializeField]
	private Sprite[] sprites;
	private SpriteRenderer sr;
	private float overheatStatus;

	// Use this for initialization
	void Awake () 
	{
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CooldownStatus();
	}

	public void SetOverheat(float status)
	{
		overheatStatus = status;
	}

	private void CooldownStatus()
	{
		if(overheatStatus < 0.15f)
		{
			sr.sprite = sprites[0];
		}
		else if(overheatStatus >= 0.15f && overheatStatus < 0.30f)
		{
			sr.sprite = sprites[1];
		}
		else if(overheatStatus >= 0.30f && overheatStatus < 0.55f)
		{
			sr.sprite = sprites[2];
		}
		else if(overheatStatus >= 0.55f && overheatStatus < 0.85f)
		{
			sr.sprite = sprites[3];
		}
		else if(overheatStatus >= 0.85f)
		{
			sr.sprite = sprites[4];
		}
	}
}
