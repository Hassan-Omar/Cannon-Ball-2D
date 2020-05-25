using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Sound")== 1)
        {
            backgroundSource.mute = false; 
        }
        else
        {
            backgroundSource.mute = true ;
        }
    }

   

    public void invertAudioState()
    {
        // invert Button sprite 
        if (!backgroundSource.mute)
            audioButton.GetComponent<Image>().sprite = muteSprite;
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            audioButton.GetComponent<Image>().sprite = audioSprite;
        }
        // invert boolean state 
        backgroundSource.mute = !backgroundSource.mute;
    }

    public void palyOrPauseGame()
    {
        if(Time.timeScale == 1)
        {
            play_Puse.GetComponent<Image>().sprite = play;
            pausePanel.SetActive(true);
            backgroundSource.Pause(); 
            Time.timeScale = 0; 
        }
        else
        {
            play_Puse.GetComponent<Image>().sprite = pause;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            backgroundSource.Play();

        }
    }

    public void exit()
    {
        Application.Quit();
    }

    public void Home()
    {
        SceneManager.LoadSceneAsync("Start");
        Time.timeScale = 1;
        GameController.toutchFlag = true; 
    }
}
