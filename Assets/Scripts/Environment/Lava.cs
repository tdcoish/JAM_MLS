using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour 
{
	[SerializeField]
	private float xSpeed;
	[SerializeField]
	private float ySpeed;

	private float xPos = 0;
	private float yPos = 0;

	private SpriteRenderer sr;

	// Use this for initialization
	void Awake () 
	{
		sr = GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
		float coordinate = Mathf.PerlinNoise(xPos, yPos);
		sr.materials[0].mainTextureOffset = new Vector2(coordinate, coordinate);
		xPos += xSpeed;
		yPos += ySpeed;
	}
}
