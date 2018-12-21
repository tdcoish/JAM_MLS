
/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : UIScreen 
{

    #region Screen Life Cycle

		protected override void UIScreenEnabled() 
		{
			
		}
		
		protected override void UIScreenDisabled()
		{
			// Do something on disable
		}

	#endregion

	#region Events

		public void OnBackMenuPressed()
		{
			UIManager.Instance.ShowScreen<MainMenuScreen>();
		}

	#endregion

}
