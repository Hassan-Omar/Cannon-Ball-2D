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
    private float lifeTime;
    private float startLifeTime;
    private Slider slider;        

    private void Start()
    {
        scoresManager = GameObject.FindGameObjectWithTag("Engine").GetComponent<ScoresManager>();
        lifeTime = Random.Range(3f, 9f);
        startLifeTime = lifeTime; 
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>(); 
    }

    private void Update()
    {
        slider.value = (lifeTime/ startLifeTime);
        if (lifeTime<=0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().createNewStar();
            Destroy(this.gameObject);
        }
        lifeTime -= Time.deltaTime;
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
        // view x2 ,x4 on the screen 
        scoresManager.viewDoubleTXT();

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
        Destroy(this.gameObject);
    }


}
