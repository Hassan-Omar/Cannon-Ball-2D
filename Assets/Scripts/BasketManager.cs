using UnityEngine;


public class BasketManager : MonoBehaviour
{

    public bool moveFlag = false;
    public float moveSpeed = 0.5f;
    public int direction = 1;

    private void Update()
    {
        // Debug.Log("****** " + moveFlag + "********" + moveSpeed + "-----" + new Vector3(0, 1, 0) * moveSpeed * direction);

        // max & min y val define the directions 
        if (transform.position.y > 400f)
            direction = -1;
        if (transform.position.y < 260f)
            direction = 1;

        if (moveFlag)
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed * direction;
        }
    }

    // to move the basket in a random place in the defined area 
    public void moveBasketNet()
    {
        if(!moveFlag)
            transform.position = new Vector3(transform.position.x, Random.Range(281f, 440f), transform.position.z);
    }
}
