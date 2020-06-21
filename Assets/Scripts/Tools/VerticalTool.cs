using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTool : MonoBehaviour
{
    private bool moveFlag;
    private int direction = -1;
    [SerializeField] private float minVal, maxVal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Ball")
        {
            moveFlag = true;
            direction = -1;
            StartCoroutine("InvertDirection");
        }
    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            if (direction == -1)
            {
                if (transform.localPosition.y > minVal)
                {
                    moveDelta(350, direction);
                }
            }
            else if (direction == 1)
            {
                if (transform.localPosition.y < maxVal)
                {
                    moveDelta(350, direction);
                }

            }
        }
    }

    private void moveDelta(float delta, float direction)
    {
        transform.position += new Vector3(0, delta, 0) * Time.deltaTime * direction;
    }

    IEnumerator InvertDirection()
    {
        yield return new WaitForSeconds(0.3f);
        direction = 1;
    }
}
