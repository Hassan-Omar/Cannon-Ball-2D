using UnityEngine;
using System.Collections;

public class SmartZone: MonoBehaviour
{
    [SerializeField] Transform CenterOfBasketBallNet;
    private Rigidbody2D rb;
    [SerializeField] private Color color;
    [SerializeField] private TimeManager timeManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Vector2 forceDirection = CenterOfBasketBallNet.position - collision.transform.position;
            rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.AddForce(300000 * forceDirection);
            // disable echo 
            collision.gameObject.GetComponentInChildren<EchoEffect>().enabled = false;
            var tr = collision.gameObject.GetComponentInChildren<TrailRenderer>();
            tr.widthMultiplier = 100;
            tr.startColor = color;
            tr.endColor = Color.black;
            StartCoroutine("changColor");
            timeManager.doSlowMotion(0.2f);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timeManager.backToNormal();
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.forward * Time.deltaTime * 100);
    }

    IEnumerator changColor()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        color = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.1f, 1), Random.Range(0.5f, 1));
        transform.GetComponent<SpriteRenderer>().color = color; 
    }
}
