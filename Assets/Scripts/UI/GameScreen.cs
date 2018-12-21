/*
* 
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c) 
* Email: krlozadan@gmail.com
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen 
{

    [Tooltip("This is the font to be used for all texts in this class")]
    [SerializeField]
    private Font            _OldSchoolFont;

    [Header("Kills & Score")]
    [SerializeField]
    private int scoreMultiplier; 
    [SerializeField]
    private Text killText;
    [SerializeField]
    private Text scoreText;

    [Header("Petrify Counter")]
    [SerializeField]
    private GameObject petrifyPowerIcon;
    [SerializeField]
    private GameObject petrifyContainer;

    [Space]
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private Color fullhealth;
    [SerializeField]
    private Color lowhealth;
    

    #region Screen Life Cycle

        protected override void UIScreenEnabled() 
        {
            Cursor.visible = false;
            // if(AudioManager.Instance.IsPlaying("InGameMusic") == false)
            // {
            //     AudioManager.Instance.Play("InGameMusic"); 
            // }
            // dialog = GetComponentInChildren<UIDialog>();
            // ResetData();
        }
        
        protected override void UIScreenDisabled()
        {
            Cursor.visible = true;
            // Do something on disable
        }

    #endregion

    private void Update()
    {

        if((Input.GetKeyDown(KeyCode.Escape)))
		{
            UIManager.Instance.ShowScreen<PauseScreen>();
            GameManager.Instance.PauseGame();
		}
        // UpdateHackedData();
    }

    public void SetPowerIcons(int powersLeft)
    {
        foreach (Transform child in petrifyContainer.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < powersLeft; i++)
        {
            GameObject newIcon = Instantiate(petrifyPowerIcon, Vector3.zero, Quaternion.identity);
            newIcon.transform.SetParent(petrifyContainer.transform);
            newIcon.transform.localScale = Vector3.one;
        }
    }

    public void SetKills(int count)
    {
        killText.text = "Kills: " + count;
    }
    
    public void SetScore(int count)
    {
        scoreText.text = "Score: " + (count * scoreMultiplier);
    }

    public void SetHealth(float health)
    {
        Vector3 newScale = healthBar.transform.localScale;
        newScale.x = health / 100f;
        healthBar.transform.localScale = newScale;
        healthBar.GetComponent<Image>().color = Color.Lerp(lowhealth, fullhealth, newScale.x);
    }

}
