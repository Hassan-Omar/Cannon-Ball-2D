using UnityEngine;
using UnityEngine.UI;
using Proyecto26; 
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
        if(!PlayerPrefs.HasKey("PlayerName"))
        {
            // this means first Time to submit 
            enterNamePanel.SetActive(true);
        }
        else
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            if(ScoresManager.points>0)
            {
                addNewScores();
            }
            loadingPanel.SetActive(true);
        }
         
    }

    public void getMyScores()
    {
        RestClient.GetArray<TransfaredObj>(this.baseUrl + "/scores/like/"+ PlayerPrefs.GetString("PlayerName")).Then(Score =>
        {
            allScores = Score;
            ViewScores(allScores);
        });
        Debug.Log(this.baseUrl + "/scores/like/" + PlayerPrefs.GetString("PlayerName"));
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
        Debug.Log(myScore.name +"    "+ myScore.score);

    }
    /// <summary>
    /// Function To Check If the Name Available 
    /// </summary>
    public void checkName(InputField userEnteredName)
    {
        if(userEnteredName.text!="" && userEnteredName.text!=null)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            if (playerName.Equals(""))
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
    }
    public void ViewScores(TransfaredObj[] arr)
    {
        textToView.text = ""; 
        foreach (TransfaredObj score in arr)
        {
            textToView.text += "  \r\n" + score.name +" : "+ score.score; 
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