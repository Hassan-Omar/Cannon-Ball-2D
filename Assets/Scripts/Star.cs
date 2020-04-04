using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/*
 * This Class Written by H.Omar 
 * 
 * this class will run on the instance of star 
 */
public class Star : MonoBehaviour
{
    // Refernce of ScoresManager 
    private ScoresManager scoresManager;
    public static int succssiveTouched = 0;
    private Text doubleText;
    private void Start()
    {
        scoresManager = GameObject.FindGameObjectWithTag("Engine").GetComponent<ScoresManager>();
    }

    // Increament Stars when got colided then destroy the star 

    private void OnTriggerExit2D(Collider2D collision)
    {
        // here we don't need to check on collision.colider.tag because the instantiated object 
        // of star is fixed @ a certain place 
        scoresManager.setStars(1 + scoresManager.getStars());
        // enable trailer 
        var tr = collision.gameObject.GetComponentInChildren<TrailRenderer>();
        tr.widthMultiplier = 100;
        // off render 
        this.GetComponent<Renderer>().enabled = false; 

        succssiveTouched += 1;
        // view text 
        doubleText = GameObject.Find("Double Text").GetComponent<Text>();
        this.doubleText.gameObject.SetActive(true);
        doubleText.text = "X" + (2 * succssiveTouched);
        StartCoroutine("resizeGame");

        // change color if it is greater than 1; 
        if (succssiveTouched == 2)
        {
            tr.startColor = Color.yellow;
            tr.endColor = Color.blue;
        }
        else if(succssiveTouched >2)
        {

            tr.startColor = Color.white;
            tr.endColor = Color.magenta;
        }
        // disable echo 
        collision.gameObject.GetComponentInChildren<EchoEffect>().enabled = false;
        

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().createNewStar();
        Destroy(this.gameObject,0.7f);
    }


    IEnumerator resizeGame()
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
