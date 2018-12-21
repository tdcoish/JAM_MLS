using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScreen : MonoBehaviour 
{
	private void OnEnable()
	{
		UIScreenEnabled();	
	}

	private void OnDisable()
	{
		UIScreenDisabled();
	}
	
	protected abstract void UIScreenEnabled();
	protected abstract void UIScreenDisabled();
}
