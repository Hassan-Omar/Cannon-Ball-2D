using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Sprite mute;
    [SerializeField] private Sprite unMute;
    [SerializeField] private GameObject loading;

    private int firstTime; 
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        // cast to integer and assign the to  bestScore
        firstTime = PlayerPrefs.GetInt("firstTime");
        if(firstTime==0)
        {
            PlayerPrefs.SetString("ActiveTheme", "0");
            PlayerPrefs.SetString("AvailableItems", "0-");
            
            PlayerPrefs.SetInt("Coins", 20000);
            // Set Prices Of Items 
            PlayerPrefs.SetInt("1", 500);
            PlayerPrefs.SetInt("2", 1000);
            PlayerPrefs.SetInt("a", 1200);
            PlayerPrefs.SetInt("b", 1800);
            PlayerPrefs.SetInt("c", 10000);
        }
    }
    // Start is called before the first frame update
    public void startBTN()
   {

        if (firstTime == 0)
        {
            SceneManager.LoadSceneAsync("Tutorial");
        }
        else
        {
            loading.SetActive(true);
            SceneManager.LoadSceneAsync("CoreGame");
        }

    }

    public void openTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
    public void openStore()
    {
        SceneManager.LoadSceneAsync("Store");
    }

    public void exit()
    {
        Application.Quit();
    }
    public void openUrl(string url)
    {
        Application.OpenURL("https://" + url);
    }
    public void Mute_UnMuteSound(Button btn)
    {
        if(PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            btn.GetComponent<Image>().sprite = unMute;

        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            btn.GetComponent<Image>().sprite = mute;

        }
    }
}
