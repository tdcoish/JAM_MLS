using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************
All this script does is set the crosshair to be where the mouse is. We should probably
do this in a different way, but it's fine for now.
*********************************************************************** */

public class Crosshairs : MonoBehaviour 
{

	#region Rotate Variables

		private SnakeRing ring;
		private Eye eye;

	#endregion

	private void Awake()
	{
		ring = GetComponentInChildren<SnakeRing>();
		eye = GetComponentInChildren<Eye>();
	}
	
	// Set its position to wherever the mouse is.
	void Update () 
	{
		Aim();
	}

	public void SetShooting(bool isShooting)
	{
		ring.SetShooting(isShooting);
	}

	public void SetOverheat(float status)
	{
		eye.SetOverheat(status);
	}

	private void Aim()
	{
		if(!GameManager.Instance.IsGamePaused()){
			
			Camera c = Camera.main;
			Vector3 msPos = c.ScreenToWorldPoint(Input.mousePosition);
			Vector2 convertedPos;
			convertedPos.x = msPos.x;
			convertedPos.y = msPos.y;

			transform.position = convertedPos;
		}
	}

}
