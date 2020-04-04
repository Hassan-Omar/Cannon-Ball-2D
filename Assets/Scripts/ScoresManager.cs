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
    private bool moveFlag = false;
    private float moveSpeed = 0.5f;
    private int  direction = 1;
    private void Update()
    {
        Debug.Log("****** " +moveFlag+ "********"+moveSpeed+"-----" + new Vector3(0, 1, 0) * moveSpeed * direction);
        if (basket.transform.position.y > 400f)
            direction = -1;
        if (basket.transform.position.y < 260f)
            direction = 1;

        if (moveFlag)
        {
            basket.transform.position += new Vector3(0,1,0)*Time.deltaTime * moveSpeed *direction;
        }
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

        // move network if the speed >100 
        if(value>50 && value<110)
        {
            moveSpeed = (value - 50);
            moveFlag = true;
        }
        else
        {
            StartCoroutine("moveBasketNet");
        }
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


    IEnumerator moveBasketNet()
    {
        yield return new WaitForSeconds(2f);
        this.basket.transform.position = new Vector3(this.basket.transform.position.x, Random.Range(281f, 440f), this.basket.transform.position.z);
    }


}
