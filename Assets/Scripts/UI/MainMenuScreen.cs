/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : UIScreen 
{	
	
	#region Screen Life Cycle

		protected override void UIScreenEnabled()
		{
			// if (AudioManager.Instance.IsPlaying("MenuMusic") == false)
			// {
			// 	AudioManager.Instance.Play("MenuMusic");
			// }
		}

		protected override void UIScreenDisabled()
		{
			// Do something on disable
		}

	#endregion

	#region Events
	
		public void OnStartGameMenuPressed()
		{
			// SceneManager.LoadScene("CarlosPlayground");
			// SceneManager.LoadScene("TempScene");
			SceneManager.LoadScene("Game");
			UIManager.Instance.ShowScreen<GameScreen>();
		}

		public void OnCreditsMenuPressed()
		{	
			UIManager.Instance.ShowScreen<CreditsScreen>();
		}

		public void OnExitMenuPressed()
		{
			Application.Quit();
		}
	
	#endregion

}