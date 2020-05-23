using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTool : MonoBehaviour
{
    private bool moveFlag;
    private int direction = -1;
    [SerializeField] private float minVal, maxVal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            moveFlag = true;
            direction = -1;
            StartCoroutine("InvertDirection");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Ball")
        {
            moveFlag = true;
            direction = 1;
        }*/
    }
        private void FixedUpdate()
    {
        if(moveFlag)
        {
            if(direction == -1)
            {
                if(transform.localPosition.x > minVal)
                {
                    moveDelta(200, direction);
                }
            }
            else if(direction == 1)
            {
                if(transform.localPosition.x < maxVal)
                {
                    moveDelta(200, direction);
                }

            }
        }
    }

    private void moveDelta(float delta,float direction)
    {
        transform.position += new Vector3(delta, 0, 0) * Time.deltaTime*direction; 
    }

    IEnumerator InvertDirection()
    {
        yield return new WaitForSeconds(0.2f);
        direction = 1;
    }
}
