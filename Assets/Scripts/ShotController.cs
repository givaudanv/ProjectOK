using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject vfx;
    public Elements element;

    private Rigidbody2D rb;

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        yield return StartCoroutine("AutoDestroy");
    }

    /*void FixedUpdate()
    {
        Vector3 nextPos = transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0);
        int layerMask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, nextPos, Vector3.Distance(nextPos, transform.position), layerMask);
        if (hit.collider)
        {
            Debug.Log(hit.collider.tag);
        }
        if (hit.collider.tag == "Shield")
        {
            if (ElementsUtils.elementMatch(element, hit.collider.GetComponent<ShieldController>().element)) hit.collider.GetComponent<ShieldController>().Damage();
            Debug.Log("touché");
            Stop();
        }
    }*/

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
