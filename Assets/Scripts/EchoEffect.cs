using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This Class Written by H.Omar 
 * 
 * this class to simulate the echo effect on the tail of the ball 
 */
public class EchoEffect : MonoBehaviour
{
    private float time_BTWSpawns;
    public float startTime; 
    public GameObject echo_Ball;

    private Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.getIsMoving())
        {
            if (time_BTWSpawns <= 0)
            {
                GameObject instance = GameObject.Instantiate(echo_Ball,transform.position,Quaternion.identity);
                Destroy(instance, 2f);
                time_BTWSpawns = startTime;
            }
            else
                time_BTWSpawns -= Time.deltaTime;
        }

        
    }
}
