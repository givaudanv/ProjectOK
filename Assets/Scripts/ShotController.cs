using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject vfx;

    private Rigidbody2D rb;
    public Elements element;

    IEnumerator Start()
    {
        SetElement(Elements.Neutral);
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
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

    public void SetElement(Elements elem)
    {
        element = elem;
        vfx.GetComponent<TrailRenderer>().colorGradient = ColorGradientUtils.getElementGradient(elem);
        var mainParticle = vfx.GetComponent<ParticleSystem>().main;
        mainParticle.startColor = ColorGradientUtils.getElementColor(elem);
    }
}
