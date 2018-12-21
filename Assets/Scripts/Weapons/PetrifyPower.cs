using System.Collections.Generic;
using UnityEngine;

public class PetrifyPower : MonoBehaviour 
{
	
	[SerializeField]
	private int powerSlots = 3;
	[SerializeField]
	private int startingPowers = 3;

	[SerializeField]
	private GameObject 			_PowerEffectGFX;

	private int powersLeft;

	private void Awake()
	{
		powersLeft = startingPowers;
		UIManager.Instance.GetScreen<GameScreen>().SetPowerIcons(powersLeft);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space) && powersLeft > 0)
		{
			var clone = Instantiate(_PowerEffectGFX, transform.position, transform.rotation);
			Destroy(clone, 2.0f);		// any time above ~0.2 is arbitrary.
			powersLeft--;
			UIManager.Instance.GetScreen<GameScreen>().SetPowerIcons(powersLeft);
			List<Enemy> tempList = new List<Enemy>(EnemySpawner.enemies);
			foreach(IPetrify enemy in tempList)		
			{
				enemy.Petrify();
			}
		}
	}

	public void AddPower()
	{
		if(powersLeft < powerSlots)
		{
			powersLeft++;
			UIManager.Instance.GetScreen<GameScreen>().SetPowerIcons(powersLeft);
		}
	}

	public int GetPowersLeft()
	{
		return powersLeft;
	}
}
