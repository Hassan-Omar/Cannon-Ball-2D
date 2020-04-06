using System.IO;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;


/*
 * This Class Written by H.Omar 
 * 
 * This class manages the player Score (Read & Write BestScoes , groups the score Attributes )  
 * 
 */
public class ScoresManager : MonoBehaviour
{
    // Resfernce of UI Text to Update when the Attributes changed 
    public Text pointsText, starsText, bsetScoreText;
    public GameObject basket;
    // private Attributes
    public static int points;
    private int stars;
    private int bestScore;
    private Text doubleText;


    private void Start()
    {
        string path = "Assets/Resources/scores/firstime.txt";
        //Write  text to the best.txt file
        File.WriteAllText(path, "1");
       
    }

    public void startGame()
    {
        // initilize 
        points = 0;
        stars = 0; 
        // load bestScore value from Resources
        var bestScore_TXT = Resources.Load<TextAsset>("scores/best").ToString();
        // cast to integer and assign the to  bestScore
        bestScore = int.Parse(bestScore_TXT);
        // Initialize UI element 
        pointsText.text = "0";
        starsText.text = "0";
        bsetScoreText.text = bestScore.ToString();
    }
    
    // Function to update the Stored value in Asset 
    public void update_BestScore(int score)
    {       
        string path = "Assets/Resources/scores/best.txt";
        //Write  text to the best.txt file
        File.WriteAllText(path, score.ToString());
    }



    //__________________ Setter & Getter for points Property __________________\\
    public void setPoints(int value)
    {
        points = value;
        // update UI Text value
        pointsText.text = value.ToString();
       
    }
    public int getPoints()
    {
        return points;
    }
    //___________________ Setter & Getter for stars Property ___________________\\ 
    public void setStars(int value)
    {
        this.stars = value;
        // update UI Text value
        starsText.text = value.ToString(); 
    }
    public int getStars()
    {
        return this.stars;
    }
    //__________________ Setter & Getter for bestScore Property __________________\\ 
    public void setBestScore(int value)
    {
        this.bestScore = value;
        // update UI Text value
        bsetScoreText.text = value.ToString();
    }
    public int getBestScore()
    {
        return this.bestScore;
    }


    public void viewDoubleTXT()
    {

        // view text 
        doubleText = GameObject.Find("Double Text").GetComponent<Text>();
        this.doubleText.gameObject.SetActive(true);
        doubleText.text = "X" + (2 *Star.succssiveTouched);
        StartCoroutine("resizeDoubleTxt");
    }


    IEnumerator resizeDoubleTxt()
    {
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 125;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 110;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 100;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 90;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 80;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 70;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 60;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.fontSize = 50;
        yield return new WaitForSeconds(0.05f);
        this.doubleText.text = "";


    }

}
