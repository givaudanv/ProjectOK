using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject vfx;

    private Rigidbody2D rb;

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Debug.Log(transform.position);
        yield return StartCoroutine("AutoDestroy");
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Stop();
    }

    private void Stop()
    {
        vfx.GetComponent<ParticleSystem>().Stop();
        Destroy(vfx, vfx.GetComponent<ParticleSystem>().main.startLifetime.constant);
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
