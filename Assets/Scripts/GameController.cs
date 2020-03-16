using UnityEngine;
/*
 * This Class Written by H.Omar 
 * 
 * this class is the core of game (initialize the scene and conrol on canon based on user touch )  
 */
public class GameController : MonoBehaviour
{
    public int forceValue;
    public static bool toutchFlag = true;
    // Refernce on game objs that will be created @ Run Time 
    public GameObject star, madf3, ball, engine, scoresMenu, endGamePanel,basket;
    // the points whitch is availabe to instantiate a star
    public Vector2[] starPoints;
    public Transform[] ballParents;
    // this var created only to generalize the solution but itn't add value for now 
    public int maxNumberOfBalls;
    // to hold the value of instantaied balls 
    private GameObject[] instance_balls =new GameObject[3];
    private int ballCounter;

    // Start is called before the first frame update
    public void StartGame()
    {
        ballCounter = 0;
        // activate some objects im hierarchy 
        madf3.SetActive(true);
        scoresMenu.SetActive(true);
        engine.SetActive(true);
        basket.SetActive(true);
        // clear the scores on score menue ;
        engine.GetComponent<ScoresManager>().startGame();
        // create a star in a random point 
        createNewStar();
        // create the balls inside canon 
        for (int i = 0; i < maxNumberOfBalls; i++)
        {
            GameObject instance = Instantiate(ball, ballParents[i]);
            instance.GetComponent<Rigidbody2D>().isKinematic = true;
            instance_balls[i] = instance;
        }

        engine.GetComponent<AdManager>().RequestBanner();
        engine.GetComponent<AdManager>().RequestFullScreenAd();

    }



    // Update is called once per frame
    void Update()
    {
        if (ballCounter < maxNumberOfBalls)
        {
            if (toutchFlag)
            {
                // turn on  Canon animation 
                GameObject.FindGameObjectWithTag("Canon").GetComponent<Animator>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    project(ballCounter);
                    ballCounter++;
                    toutchFlag = false;
                }
            }

        }
        else 
        {
            if (toutchFlag)
            {
                endGame();
            }
        }
    }

    // function to project the ball 
    public void project(int ballNum)
    {
        GameObject currentBall = instance_balls[ballNum];
        currentBall.transform.SetParent(null);
        currentBall.GetComponent<Rigidbody2D>().isKinematic = false;
        currentBall.GetComponent<Ball>().setIsMoving(true);
        currentBall.GetComponent<Rigidbody2D>().AddForce(ballParents[0].position * forceValue);
        // turn off Canon animation 
        GameObject.FindGameObjectWithTag("Canon").GetComponent<Animator>().enabled = false;

    }

    //function to move all balls one step toward the muzzle of canon
    private void moveOneStep(int shiftValue)
    {
        // number of iteration  is same as ball numbers 
        for (int i=ballCounter; i<maxNumberOfBalls; i++)
        {
            instance_balls[i].transform.position = ballParents[i-shiftValue].position;
        }
    }
    public void moveBallsToMuzzle()
    {
        moveOneStep(ballCounter);
    }

    // function give a random point to instantiate a star 
    public void createNewStar()
    {
        float fPoint = Random.Range(0, starPoints.Length - 1);       
        Instantiate(star, starPoints[(int)fPoint], Quaternion.identity);
    }

    // function to end the game 
    private void endGame()
    {
        // deactivate objects a gain
        madf3.SetActive(false);
        scoresMenu.SetActive(false);
        engine.SetActive(false);
        basket.SetActive(false);
        endGamePanel.SetActive(true);
        // destroy last star if not 
        var star = GameObject.FindGameObjectWithTag("Star");
        Destroy(star);
        ballCounter = 0;
        // update stored value if current is better
        var scoresManager = engine.GetComponent<ScoresManager>();
        if (scoresManager.getPoints() > scoresManager.getBestScore())
            scoresManager.update_BestScore(scoresManager.getPoints());

        engine.GetComponent<AdManager>().ShowFullScreenAd();
        this.gameObject.SetActive(false);
    }
}
