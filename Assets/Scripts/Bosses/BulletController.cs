using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<PlayerController>().Damage();
        Destroy(gameObject);
    }
}
