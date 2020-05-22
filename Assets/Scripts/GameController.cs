using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This Class Written by H.Omar 
 * 
 * this class is the core of game (initialize the scene and conrol on canon based on user touch )  
 */
public class GameController : MonoBehaviour
{
    public Text text;
    public int forceValue;
    public static bool toutchFlag = true;
    // Refernce on game objs that will be created @ Run Time 
    public GameObject star, madf3,canon, ball, wheel, background, engine,admanger, scoresMenu, bottomMenu,endGamePanel, basket , mainmenu,optMainMenu;
    // the points whitch is availabe to instantiate a star
    public Vector2[] starPoints;
    public Transform[] ballParents; 

    // to hold the value of instantaied balls 
    private GameObject[] instance_balls = new GameObject[3];
    private int ballCounter;
    // keep the mumber of failing 
    private int failsNum;
    // reference on refile sound 
    private AudioSource refile; 

    private AdManager instAdManager;

    // fill canon with 3 balls 
    private void fillCanon()
    {
        // create the balls inside canon 
        for (int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(ball, ballParents[i]);
            instance.GetComponent<Rigidbody2D>().isKinematic = true;
            instance_balls[i] = instance;
        }
    }

    // fill canon after small delay



    IEnumerator delayRefileCanon()
    {
        // we can ad refile sound now 
        refile.Play();
        yield return new WaitForSeconds(1.5f);
        fillCanon();

        Debug.Log("Called ===> " + ballCounter);
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        selectActiveScene();

        instAdManager = admanger.GetComponent<AdManager>(); 
        bottomMenu.transform.SetParent(mainmenu.transform);
        bottomMenu.SetActive(false);
        optMainMenu.SetActive(false);
        Star.succssiveTouched = 0;
        refile = this.GetComponent<AudioSource>();
        ballCounter = 0;
        resetFailsNum();
        // activate some objects im hierarchy 
        madf3.SetActive(true);
        canon.GetComponent<Animator>().speed = 0.5f; 
        scoresMenu.SetActive(true);
        engine.SetActive(true);
        basket.SetActive(true);
        var basketManager = basket.GetComponent<BasketManager>();
        basketManager.moveFlag = false;
        basketManager.moveSpeed = 0.5f;
        basketManager.direction = 1;
        // clear the scores on score menue ;
        engine.GetComponent<ScoresManager>().startGame();
        // create a star in a random point 
        createNewStar();

        // fill the canon 
        fillCanon();

        //engine.GetComponent<AdManager>().RequestBanner();
        //engine.GetComponent<AdManager>().RequestFullScreenAd();

    }

    public void increaseCanonSpeed()
    {
        // afetr 21 enter will be 2.39
        if(canon.GetComponent<Animator>().speed<2.4)
            canon.GetComponent<Animator>().speed += 0.04f;
    }

    public void increaseFailsNum()
    {
        this.failsNum += 1;
        text.text = failsNum.ToString();
    }

    public void resetFailsNum()
    {
        this.failsNum = 0;
        text.text = failsNum.ToString();

    }


    // Update is called once per frame
    void Update()
    {

        if (toutchFlag)
        {

            if (failsNum != 3)
            { 
                // turn on  Canon animation 
                GameObject.FindGameObjectWithTag("Canon").GetComponent<Animator>().enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                project(ballCounter);
                ballCounter++;

                if(ballCounter==3)
                {
                    // reinitialize canon's ball 
                    StartCoroutine("delayRefileCanon");
                    ballCounter = 0; 
                }

                toutchFlag = false;
            }}

        }

        if (failsNum == 3)
        {
            if(toutchFlag)
            {
                Debug.Log("*********End Called");
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
        try {

            // number of iteration  is same as ball numbers 
            for (int i = ballCounter; i < 3; i++)
            {
                instance_balls[i].transform.position = ballParents[i - shiftValue].position;
            }
        }
        catch(System.Exception e)
        {

        }
    }
    public void moveBallsToMuzzle()
    {
        moveOneStep(ballCounter);
    }

    // function give a random point to instantiate a star 
    public void createNewStar()
    {
        // this check to prevent multi stars
        StartCoroutine("createNewStarCoroutine");
    }

    IEnumerator createNewStarCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("--------" + GameObject.FindGameObjectWithTag("Star"));
        if (GameObject.FindGameObjectWithTag("Star") == null)
        {
            float fPoint = Random.Range(0, starPoints.Length - 1);
            Instantiate(star, starPoints[(int)fPoint], Quaternion.identity);
        }
    }
    // function to end the game 
    private void endGame()
    {
       
        try
        {
            instAdManager.ShowFullScreenAd();
        }
        catch(System.Exception e)
        {
            //text.text += ("----"+e);
        }
        

        // destroy last star if not and last balls
        var stars = GameObject.FindGameObjectsWithTag("Star");
        foreach(var star in stars)
        {
            Destroy(star);
        }

        GameObject.Find("Double Text").GetComponent<Text>().text = "";
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
        // deactivate objects a gain
        madf3.SetActive(false);
        scoresMenu.SetActive(false);
        optMainMenu.SetActive(true);
        bottomMenu.transform.SetParent(optMainMenu.transform);
        bottomMenu.SetActive(true);
        engine.SetActive(false);
        basket.SetActive(false);
        endGamePanel.SetActive(true);
       
        ballCounter = 0;
        this.failsNum = 0;
        // update stored value if current is better
        var scoresManager = engine.GetComponent<ScoresManager>();
        // update if current score is better than stored  

        if (scoresManager.getPoints() > scoresManager.getBestScore())
        {
            scoresManager.update_BestScore(scoresManager.getPoints());
        }

         //engine.GetComponent<AdManager>().ShowFullScreenAd();
         this.gameObject.SetActive(false);
    }


    public void loadHighScores()
    {
        SceneManager.LoadScene("Submit");
    }


    private void selectActiveScene()
    {
        //var currentActiveTheme =  PlayerPrefs.GetInt("Active_Theme");
        var currentActiveTheme = 1;
        if(currentActiveTheme != 0)
        {
            //ball.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("themes/" + currentActiveTheme + "/ball"); 
            //canon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("themes/" + currentActiveTheme + "/cannon"); 
            // wheel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("themes/" + currentActiveTheme + "/wheel"); 
            //background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("themes/" + currentActiveTheme + "/background");

            ball.GetComponent<SpriteRenderer>().sprite =
            ConvertTextureToSprite(LoadTexture(Application.dataPath + "/themes/1/ball.png"));
          }
    }



    private Sprite ConvertTextureToSprite(Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Converts a Texture2D to a sprite, assign this texture to a new sprite and return its reference

        Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }

    private Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

}
