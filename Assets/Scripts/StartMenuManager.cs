using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private int firstTime; 
    private void Start()
    { 
        // cast to integer and assign the to  bestScore
        firstTime = PlayerPrefs.GetInt("firstTime");
        Debug.Log("firstTime" + firstTime);

    }
    // Start is called before the first frame update
    public void startBTN()
   {
      
       if (firstTime == 0)
            SceneManager.LoadSceneAsync("Tutorial");
       else
            SceneManager.LoadSceneAsync("CoreGame");

    }

    public void openTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void ourWebSite()
    {
        Application.OpenURL("http://www.hassan.com");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
