using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;
/// <summary>
/// This Class Created to handle High Scores 
/// </summary>
public class RestApiManager : MonoBehaviour
{
    private string playerName; 
    private string baseUrl = "https://cannon-ball.herokuapp.com";
    TransfaredObj[] allScores;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject enterNamePanel;
    [SerializeField] private GameObject isNotAvailable;
    [SerializeField] private Text textToView;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        if(playerName.Equals(""))
        {
            // this means first Time to submit 
            enterNamePanel.SetActive(true);
        }
        else
        {
            loadingPanel.SetActive(true);
        }

        //getMyScores();
        //getTopScores(10);
    }

    public void getMyScores()
    {
        RestClient.GetArray<TransfaredObj>(this.baseUrl + "/scores/like/"+ playerName).Then(Score =>
        {
            allScores = Score;
            ViewScores(allScores);
        });
    }
    public void getTopScores(int num)
    {
        RestClient.GetArray<TransfaredObj>(this.baseUrl + "/scores/top/" + num).Then(Score =>
        {
            allScores = Score;
            ViewScores(allScores);

        });
    }
    public void addNewScores()
    {
        TransfaredObj myScore = new TransfaredObj();
        myScore.name = this.playerName;
        myScore.score = ScoresManager.points;

        RestClient.Post<TransfaredObj>(this.baseUrl + "/scores/insert", myScore).Then(Score =>
        {
            getTopScores(10);
        }).Catch(err => {
            Debug.Log(err); 
        });
    }
    /// <summary>
    /// Function To Check If the Name Available 
    /// </summary>
    public void checkName(InputField userEnteredName)
    {
        if(playerName.Equals(""))
        {
            // this means the player this is first time to play 
            // check if the name is available 
            RestClient.GetArray<TransfaredObj>(this.baseUrl + "/scores/like/" + userEnteredName.text).Then(Score =>
            {

                if (Score != null)
                {
                    // This means the name isn't Available 
                    isNotAvailable.SetActive(true);

                }
            }).Catch(err =>
            {
                   // this means the name is Available 
                    isNotAvailable.SetActive(false);
                    // Now Save this Name 
                     PlayerPrefs.SetString("PlayerName", userEnteredName.text);
                 this.playerName = userEnteredName.text; 
                // Visualize Loading Bar 
                loadingPanel.SetActive(true);
                    enterNamePanel.SetActive(false);
                    

                // Now Save This Score To DB 
                addNewScores();
            });

        }
    }
    public void ViewScores(TransfaredObj[] arr)
    {
        textToView.text = ""; 
        foreach (TransfaredObj score in arr)
        {
            textToView.text += "\r\n" + score.name +" : "+ score.score; 
        }
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}

[Serializable]
public class TransfaredObj
{
    public int scoreId;
    public string name;
    public int score;
    public string scoreDate;
}