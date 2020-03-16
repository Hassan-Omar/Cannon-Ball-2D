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
    private void Start()
    {
        scoresManager = GameObject.FindGameObjectWithTag("Engine").GetComponent<ScoresManager>();
    }



    // trigger colider in the middel of cloth network
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Basket")
        {
            //Increment Player Score by 6 
            scoresManager.setPoints(6 + scoresManager.getPoints());
            // vibrate the basket 
            colider.GetComponent<Animator>().enabled = true;
            StartCoroutine("turnOfBasketAnimator");
            //colider.transform.position.y -= Random.Range(0, 24f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            GameController.toutchFlag = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().moveBallsToMuzzle();
            StartCoroutine("delayDestroy");
        }
        else if (collision.collider.tag == "Basket")
        {
            // vibrate the basket 
            collision.gameObject.GetComponent<Animator>().enabled = true;
            StartCoroutine("turnOfBasketAnimator");
        }

    }

    // Coroutine to destroy the ball after 1 second 
    IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(0.8f);
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

    IEnumerator turnOfBasketAnimator()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("Basket").GetComponent<Animator>().enabled = false;
    }
    
}
