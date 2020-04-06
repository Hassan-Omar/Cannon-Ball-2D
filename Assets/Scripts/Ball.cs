using System.Collections;
using UnityEngine;

/*
 * This Class Written by H.Omar 
 * 
 * this class will run on the instance of ball to handel it's state 
 */
public class Ball : MonoBehaviour
{
    // Refernce of ScoresManager 
    private ScoresManager scoresManager;
    // if the ball is moving echo will apears 
    private bool isMoving;
    // to tell me if this ball entered the basket or not 
    private bool isToutchingBasket = false;
    // reference on game controller 
    private GameController gameController;
    // to solve multi increase problem 
    private bool istouchedWithGoal = false;

    private void Start()
    {
        scoresManager = GameObject.FindGameObjectWithTag("Engine").GetComponent<ScoresManager>();
        gameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
        // disable line renderer 
        GetComponent<TrailRenderer>().widthMultiplier = 0;
       
    }



    // trigger colider in the middel of cloth network
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Basket")
        {
            // check if this is the first touch 
            if(!this.istouchedWithGoal)
            {

                if (Star.succssiveTouched == 0)
                {
                    //Increment Player Score by 3 
                    scoresManager.setPoints(3 + scoresManager.getPoints());
                }
                else
                {
                    // Increment Player Score by 3 
                    scoresManager.setPoints(3 * (2*Star.succssiveTouched) + scoresManager.getPoints());
                }
            }
            istouchedWithGoal = true; 


            // vibrate the basket 
            colider.GetComponent<Animator>().enabled = true;
            StartCoroutine("turnOffBasketAnimator");
            //colider.transform.position.y -= Random.Range(0, 24f);
            this.isToutchingBasket = true;

            gameController.resetFailsNum();
            gameController.increaseCanonSpeed();

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when we colid to floor we should do
        // 1- move balls one step 
        // 2- check if the ball hit the basket 
        if (collision.collider.tag == "Floor")
        {  
            // restet succssiveTouched
            Star.succssiveTouched = 0;

            gameController.moveBallsToMuzzle();
            if(!this.isToutchingBasket)
            {
                gameController.increaseFailsNum();
            }

            // move network if the speed >100 
            var value = scoresManager.getPoints();
            var basketManager = GameObject.Find("Basket Net").GetComponent<BasketManager>();
            if (value > 50 && value < 160)
            {
                basketManager.moveSpeed = (value - 50);
                basketManager.moveFlag = true;
            }
            else
            {
                basketManager.moveBasketNet();
            }

            StartCoroutine("delayDestroy");
        }
        else if (collision.collider.tag == "Basket")
        {
            // vibrate the basket 
            collision.gameObject.GetComponent<Animator>().enabled = true;
            StartCoroutine("turnOffBasketAnimator");
        }

    }

    // Coroutine to destroy the ball after 1 second 
    IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(0.6f);
        // turn on canon animation 
        GameController.toutchFlag = true;

        Destroy(this.gameObject);
    }

    //_________________ Setter & Getter ______________________________\\
    public void setIsMoving(bool value)
    {
        this.isMoving = value;
    }
    public bool getIsMoving()
    {
        return this.isMoving;
    }

    IEnumerator turnOffBasketAnimator()
    {
        yield return new WaitForSeconds(0.5f);
        if(GameObject.FindGameObjectWithTag("Basket")!=null)
        {
            GameObject.FindGameObjectWithTag("Basket").GetComponent<Animator>().enabled = false;
        }
    }
    
}
