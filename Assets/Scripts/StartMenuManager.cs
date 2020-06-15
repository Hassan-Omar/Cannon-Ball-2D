using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
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
            PlayerPrefs.SetInt("Sound", 1);

            PlayerPrefs.SetInt("Coins", 30000);
            // Set Prices Of Items 
            PlayerPrefs.SetInt("0", 0);
            PlayerPrefs.SetInt("1", 1000);
            PlayerPrefs.SetInt("2", 2000);

            PlayerPrefs.SetInt("a", 3500);
            PlayerPrefs.SetInt("b", 10000);
            PlayerPrefs.SetInt("c", 6000);
            PlayerPrefs.SetInt("d", 12000);
        }
    }
    // Start is called before the first frame update
    public void startBTN()
    {
        loading.SetActive(true);
        if (firstTime == 0)
        {
            SceneManager.LoadSceneAsync("Tutorial");
        }
        else
        {
            SceneManager.LoadSceneAsync("CoreGame");
        }

    }

    public void openTutorial()
    {
        loading.SetActive(true);
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
   
}
