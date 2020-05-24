using System.Collections;
using UnityEngine;

public class CannonMover : MonoBehaviour
{
    private bool moveFlag;
    private int direction = -1;

    [SerializeField] private float minVal, maxVal;

    private void OnEnable()
    {
        moveFlag = true;
        direction = 1;
        StartCoroutine("InvertDirection");
    }
    
    private void FixedUpdate()
    {
        if (moveFlag)
        {
            if (direction == -1)
            {
                if (transform.localPosition.x > minVal)
                {
                    moveDelta(50, direction);
                }
            }
            else if (direction == 1)
            {
                if (transform.localPosition.x < maxVal)
                {
                    moveDelta(50, direction);
                }

            }
        }
    }

    private void moveDelta(float delta, float direction)
    {
        transform.position += new Vector3(delta,0, 0) * Time.deltaTime * direction;
    }

    IEnumerator InvertDirection()
    {
        yield return new WaitForSeconds(1f);
        direction = -1;
    }
}
