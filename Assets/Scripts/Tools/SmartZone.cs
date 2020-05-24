using UnityEngine;

public class SmartZone: MonoBehaviour
{
    [SerializeField] Transform CenterOfBasketBallNet;
    private Rigidbody2D rb; 
    [SerializeField]private Color color;
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
        }
    }
}
