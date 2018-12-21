
/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : UIScreen 
{
	#region Screen Life Cycle

		protected override void UIScreenEnabled() 
		{
			// GameManager.Instance.SetInputEnabled(false);
			// AudioManager.Instance.FadeSound("InGameMusic", 0.2f, 2f); // Fade Out
		}

		protected override void UIScreenDisabled()
		{
			// Do something on disable
		}

	#endregion

	#region Events

		public void OnRestartGameButtonPressed()
		{
			
		}
		
		public void OnGoToMenuButtonPressed()
		{
			AudioManager.Instance.Stop("InGameMusic");
			UIManager.Instance.ShowScreen<MainMenuScreen>();
			SceneManager.LoadScene("MainMenu");
		}

    #endregion
}
