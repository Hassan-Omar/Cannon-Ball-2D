using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This Class Written by H.Omar 
 * 
 * this class will run on the instance of star 
 */
public class Star : MonoBehaviour
{
    // Refernce of ScoresManager 
    private ScoresManager scoresManager;
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
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().createNewStar();
        Destroy(this.gameObject);
    }

}
