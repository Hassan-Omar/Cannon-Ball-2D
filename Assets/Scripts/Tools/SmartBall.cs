using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBall : MonoBehaviour
{
    [SerializeField]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Basket")
    }
}
