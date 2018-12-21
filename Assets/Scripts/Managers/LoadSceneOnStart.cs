/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour 
{
	[SerializeField]
	private string sceneToLoad;

	private void Start()
	{
		sceneToLoad = sceneToLoad.Trim();
		if(sceneToLoad.Length > 0)
		{
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}