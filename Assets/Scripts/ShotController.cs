using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject vfx;

    private Rigidbody2D rb;
    private Elements element;

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        yield return StartCoroutine("AutoDestroy");
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Stop();
    }

    public void Stop()
    {
        vfx.GetComponent<ParticleSystem>().Stop();
        Destroy(vfx, vfx.GetComponent<ParticleSystem>().main.startLifetime.constant);
        transform.DetachChildren();
        Destroy(gameObject);
    }

    public void SetElement(Elements elem)
    {
        element = elem;
        vfx.GetComponent<TrailRenderer>().colorGradient = ElementsUtils.getElementGradient(elem);
        var mainParticle = vfx.GetComponent<ParticleSystem>().main;
        mainParticle.startColor = ElementsUtils.getElementColor(elem);
    }
}
