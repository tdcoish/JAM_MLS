using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonBehaviour<GameManager> 
{
	
	#region Global Variables
		
		private bool gameOver = false;
		private bool gamePaused = false;
		

		private int totalKills = 0;
		private int killsNoPetrified = 0;

		private int healthPBodyCount = 0;
		private int powerUpBodyCount = 0;
		
		[SerializeField]
		private int powerUpBonusBodyCount = 5;
		[SerializeField]
		private int healthBonusBodyCount = 10;
		
		private PetrifyPower power;
		private EnemySpawner enemySpawner;
		private PickupSpawner pickupSpawner;
		private GameScreen gameScreen;

	#endregion


	#region Singleton Object

		protected override void SingletonAwake()
		{
			gameScreen = UIManager.Instance.GetScreen<GameScreen>();
			power = FindObjectOfType<PetrifyPower>();
			enemySpawner = FindObjectOfType<EnemySpawner>();
			pickupSpawner = FindObjectOfType<PickupSpawner>();
		}

	#endregion 

	private void Update()
	{
		gameScreen.SetKills(killsNoPetrified);
		gameScreen.SetScore(totalKills);
	}



	#region Petrify Power Logic

		public void EnemyKilled(bool countsForPetrifyPower)
		{
			totalKills++;
			healthPBodyCount++;

			if(countsForPetrifyPower)
			{
				killsNoPetrified++;
				powerUpBodyCount++;
			}

			// Gives the player a power up
			if (powerUpBodyCount == powerUpBonusBodyCount)
			{
				powerUpBodyCount = 0;
				power.AddPower();
			}

			if(healthPBodyCount == healthBonusBodyCount)
			{
				healthPBodyCount = 0;
				pickupSpawner.SpawnPickup();
			}
		}

	#endregion

	#region Game Pause Logic

		public void PauseGame()
		{
			Time.timeScale = 0f;
			gamePaused = true;
		}
		
		public void ResumeGame()
		{
			Time.timeScale = 1f;
			gamePaused = false;
		}

		public bool IsGamePaused()
		{
			return gamePaused;
		}

	#endregion

	#region Game Over Logic

		public bool isGameOver()
		{
			return gameOver;
		}
		
		public void ResetGame()
		{
			totalKills = 0;
			killsNoPetrified = 0;
			healthPBodyCount = 0;
			powerUpBodyCount = 0;
			enemySpawner.Reset();
			UIManager.Instance.ShowScreen<GameOverScreen>();
		}

	#endregion

}
