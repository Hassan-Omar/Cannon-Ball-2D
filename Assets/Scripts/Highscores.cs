using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour
{
    public InputField playerName;
    //public Text kils;
    public Text scrollerContent;
    public GameObject submitCanvas;
    public GameObject scoresCanvas;
    public Text uploadHint;
    private bool flag = true; 


    const string privateCode = "LYqwkpeTzUG-AijqYfzY2Qfe38UL61-ECLTbWxywr_BA";
    const string publicCode = "5e863398403c2d12b8ae65f6";
    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;


    public void backToMain()
    {
        SceneManager.LoadSceneAsync(0);
    }


    //___________________________________________________________


    // this method to upload new scores 
    // replace 25 in line 32 with the student grade 
    public void onSubmit()
    {
       if(flag)
        {
            if (playerName.text != null && playerName.text != "")
            {
                AddNewHighscore(playerName.text, ScoresManager.points);
            }

            flag = false; 
        }
    }
    public void showScores()
    {
        // don't forget to tell user wait 
        DownloadHighscores();
    }
    //______________________________________________________________
    // tow methods to call routines for download and upload data 
    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }
    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }
    //++++++++++++++++++++++++++++++++++

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        // check if we get error 
        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            // don't forget to tell user that upload is done  
            submitCanvas.SetActive(false);
            DownloadHighscores();
            scoresCanvas.SetActive(true);

        }
        else
        {
            //++++++ here put the Text ++++++++++
            uploadHint.text = " Please Turn on Wifi or Mobile Data";
            print("Error uploading: " + www.error);
        }
    }

    
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Downloading: " + www.error);
            //++++++ here put the Text ++++++++++
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }


    //+++++++++++++++++++
    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreList.Length; i++)
        {
            string temp = i + 1 + " : ";
            if (i < highscoreList.Length)
            {
                temp += highscoreList[i].username + "      " + highscoreList[i].score;
            } 
            var text = temp.Replace("+", " "); 
            scrollerContent.text += text + "\r\n";


        }
    }
  

}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }

    
}
