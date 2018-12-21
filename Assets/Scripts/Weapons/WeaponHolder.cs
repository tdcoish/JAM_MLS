using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour 
{
	
	#region Global Variables
		
		private Weapon[] weapons = null;
		private IEnumerator currentState = null;	

	#endregion

	#region Cooldown Variables

		[SerializeField, Range(0,10f)]
		private float cooldonTime = 1f;
		[SerializeField, Range(0,1f)]
		private float cooldownStep = 0.1f;
		
		private float heatStatus = 0;
		private float cooldownLimit = 1f;
		private bool overheated = false;

	#endregion

	public float GetWeaponStatus()
	{
		return heatStatus;
	}

	#region Initialization

		private void Awake() 
		{
			weapons = GetComponentsInChildren<Weapon>();
			// if (weaponPrefabs != null && weaponPrefabs.Length > 0)
			// {
			// 	weapons = new GameObject[weaponPrefabs.Length];
			// 	for(int i = 0; i < weaponPrefabs.Length; i++)
			// 	{
			// 		weapons[i] = Instantiate(weaponPrefabs[i], transform);
			// 		weapons[i].transform.SetParent(transform);
			// 	}
			// }
			SetState(Normal());
		}

	#endregion

	#region Weapon System Logic

		public bool Attack(int slot, Quaternion direction)
		{
			// Attacking logic
			if (!overheated)
			{
				heatStatus += weapons[slot].Attack(direction);
				if (heatStatus >= cooldownLimit)
				{
					heatStatus = cooldownLimit;
					overheated = true;
					SetState(CoolingDown());
				}
				return true;
			}
			return false;
		}

	#endregion

	#region State Machine
		
		private void SetState(IEnumerator state)
		{
			if (currentState != null)
			{
				StopCoroutine(currentState);
			}
			currentState = state;
			StartCoroutine(currentState);
		}

		IEnumerator CoolingDown()
		{
			while(heatStatus > 0f) 
			{
				heatStatus -= cooldownStep;
				yield return new WaitForSeconds(cooldonTime);
			}
			overheated = false;
			heatStatus = 0;
			SetState(Normal());
		}

		IEnumerator Normal()
		{
			while(true)
			{
				if(heatStatus > 0f) 
				{
					heatStatus -= cooldownStep;
				}
				else
				{
					heatStatus = 0;
				}
				yield return new WaitForSeconds(cooldonTime);
			}
		}

	#endregion

}
