using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

/*
 * This Class Written by H.Omar 
 * 
 * This class to control on the Main Menu (open PlayStore Linke , mute audio or allow , close the Game )  
 * 
 */
public class MainMenuManager : MonoBehaviour
{
    // Reference of Sprites for Audio Button
    [SerializeField] private Sprite muteSprite, audioSprite; 
    // Reference of Sprites for Play-Puse Button
    [SerializeField] private Sprite play, pause;
    // Reference of Audio Button , Play-Puse Button
    [SerializeField] private Button audioButton ,play_Puse;
   


    public AudioSource backgroundSource;
    // this function close the game 
    public void exit()
    {
        Application.Quit(); 
    }

    // this function to open the game page in play store 
    public void rateUs()
    {
        Application.OpenURL("https://play.google.com/store/search?q=MYSALWARD");
    }

    public void invertAudioState()
    {
        
        // invert Button sprite 
        if (!backgroundSource.mute)
            audioButton.GetComponent<Image>().sprite = muteSprite;
        else
            audioButton.GetComponent<Image>().sprite = audioSprite;
        // invert boolean state 
        backgroundSource.mute = !backgroundSource.mute;
    }

    public void palyOrPauseGame()
    {
        if(Time.timeScale == 1)
        {
            play_Puse.GetComponent<Image>().sprite = play;
            backgroundSource.Pause(); 
            Time.timeScale = 0; 
        }
        else
        {
            play_Puse.GetComponent<Image>().sprite = pause;
            Time.timeScale = 1;
            backgroundSource.Play();

        }
    }
   
  
}
