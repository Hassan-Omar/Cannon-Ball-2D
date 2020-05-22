using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTool : MonoBehaviour
{
    private bool moveFlag;
    private int direction = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveFlag = true;
        StartCoroutine("InvertDirection");
    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            moveDelta(0.03f, direction);
        }
    }

    private void moveDelta(float delta, float direction)
    {
        transform.Rotate(new Vector3(0,delta,0)*direction)
    }

    IEnumerator InvertDirection()
    {
        yield return new WaitForSeconds(2);
        direction = 1;
        yield return new WaitForSeconds(2);
        moveFlag = false;
    }
}
