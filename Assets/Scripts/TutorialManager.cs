using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    private bool tapFalg = true;
    // Refernce on game objs that will be created @ Run Time 
    [SerializeField] private GameObject star, madf3, canon, ball, ballbasket;
    [SerializeField] private Transform[] ballParents;
    [SerializeField] private Transform starParent;
   
    private int stepNum=-1, ballCounter=0;
    private GameObject[] instance_balls = new GameObject[3];
    [SerializeField] private TextMeshProUGUI handler;
    [SerializeField] private Text pointsTxt;

    private GameObject instStar;

    // Start is called before the first frame update
    void Start()
    {
        // fill the canon 
        for(var i= 0; i < 3; i++)
        {
            
            GameObject instance = Instantiate(ball, ballParents[i]);
            instance.GetComponent<Rigidbody2D>().isKinematic = true;
            instance_balls[i] = instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(tapFalg)
                viewTutorial(stepNum);
        }
    }


    // function to project the ball 
    public void project(int ballNum)
    {
        GameObject currentBall = instance_balls[ballNum];
        currentBall.transform.SetParent(null);
        currentBall.GetComponent<Rigidbody2D>().isKinematic = false;
        currentBall.GetComponent<Rigidbody2D>().AddForce(ballParents[0].position * 600000);
        this.ballCounter++;
        moveOneStep(this.ballCounter);
    }

    // this corotuine to run the tutorial 
    private void viewTutorial(int stepNum)
    {
        Debug.Log("Step No " + stepNum);
         
         switch (stepNum)
         {
            case -1:
                handler.fontSize = 65;
                handler.text = "Your Target is Entering Ball Inside The Basket";
                break;
            case 0:
                tapFalg = false;
                handler.text = "Canon's capacity 3 ball only";
                StartCoroutine("delayTXT");
                break;
            case 1:
                tapFalg = false;
                // here projectile 
                project(0);
                handler.text = "";
                StartCoroutine("delayTXT");
                break;
            case 2:
                
                handler.text = "Hitting Stars Will double The Score";
                instStar = Instantiate(star, starParent);
                break;
            case 3:
                handler.text = "Tap to Project ball";
                break;
            case 4:
                project(1);
                handler.text = " ";
                tapFalg = false;
                StartCoroutine("delayTXT");
                Destroy(instStar, 0.13f);
                break;
            case 5:
                tapFalg = false;
                handler.text = "Take care The Basket change its Place ";
                StartCoroutine("delayTXT");
                break;
            case 6:
                ballbasket.SetActive(false);
                handler.text = "Each Star has a life Time";
                break;
            case 7:
                canon.GetComponent<Animator>().enabled = true; 
                handler.text = "Canon's Rotation Speed Increase with High Scores";
                break;
            case 8:
                madf3.SetActive(false);
                handler.text = "If you lost 3 Succsessive balls then Game end";
                break;

            case 9:
                handler.text = "Starting the Game ";
                loadGame();
                break;
         }
        this.stepNum++;

    }

    private void moveOneStep(int shiftValue)
    {
        try
        {

            // number of iteration  is same as ball numbers 
            for (int i = ballCounter; i < 3; i++)
            {
                instance_balls[i].transform.position = ballParents[i - shiftValue].position;
            }
        }
        catch (System.Exception e)
        {

        }
    }
    public void moveBallsToMuzzle()
    {
        moveOneStep(ballCounter);
    }

    IEnumerator delayTXT()
    {
        if(this.stepNum == 1)
        {
            yield return new WaitForSeconds(4f);
            handler.text = "Nice! you get 3 points";
            pointsTxt.text = "3";
            instance_balls[0].SetActive(false);
            tapFalg = true;
        }
        else if (this.stepNum == 0)
        {
            yield return new WaitForSeconds(2f);
            handler.text = "Tap to project the ball ";
            tapFalg = true;
        }
        else if (this.stepNum==4)
        {
            yield return new WaitForSeconds(4f);
            handler.text = "Nice! you get 6 points";
            pointsTxt.text = "9";
            instance_balls[1].SetActive(false);
            tapFalg = true; 
        }
        else if (this.stepNum == 5)
        {
            yield return new WaitForSeconds(2f);
            ballbasket.transform.position += new Vector3(0, 40, 0);
            tapFalg = true;
        } 
    }

    public void loadGame()
    {
        SceneManager.LoadSceneAsync("CoreGame");
    }
}
