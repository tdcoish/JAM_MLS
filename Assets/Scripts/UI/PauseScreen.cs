/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : UIScreen 
{

	#region Screen Life Cycle

		protected override void UIScreenEnabled() 
		{
			// GameManager.Instance.PauseGame();
			//GameManager.Instance.SetInputEnabled(false);
			// AudioManager.Instance.FadeSound("InGameMusic", 0.2f, 2f); // Fade Out
		}

		protected override void UIScreenDisabled()
		{
			// Do something on disable
		}

	#endregion

	#region Events


		public void OnResumeGameButtonPressed()
		{
			// AudioManager.Instance.FadeSound("InGameMusic", 1f, 2f); // Fade in	
			UIManager.Instance.ShowScreen<GameScreen>();
			GameManager.Instance.ResumeGame();
			// GameManager.Instance.ResumeGame();
			// GameManager.Instance.SetInputEnabled(true);
		}

		public void OnQuitGameButtonPressed()
		{
			SceneManager.LoadScene("MainMenu");
			UIManager.Instance.ShowScreen<MainMenuScreen>();
		}

    #endregion
}