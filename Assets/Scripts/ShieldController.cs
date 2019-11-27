using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Elements element;
    public float hp;

    private float currentHp;
    private float blinkTime = 0.1f;
    private float blinkTimer;
    private bool isBlinking = false;

    void Start()
    {
        currentHp = hp;
        blinkTimer = 0f;
    }

    void Update()
    {
        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer < blinkTime)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isBlinking = false;
                blinkTimer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shot")
        {
            ShotController sc = other.GetComponent<ShotController>();
            if (ElementsUtils.elementMatch(sc.element, element)) Damage();
            sc.Stop();
        }
    }

    private void Damage()
    {
        currentHp--;
        if (currentHp <= 0f) Destroy(gameObject);
        else isBlinking = true;
    }
}
