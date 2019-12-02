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
    private Dictionary<GameObject, int> shieldedObject;

    void Start()
    {
        currentHp = hp;
        blinkTimer = 0f;
        shieldedObject = new Dictionary<GameObject, int>();

        CircleCollider2D shieldCollider = GetComponent<CircleCollider2D>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, shieldCollider.radius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider != shieldCollider && shieldCollider.bounds.Contains(collider.bounds.min) && shieldCollider.bounds.Contains(collider.bounds.max))
            {
                shieldedObject.Add(collider.gameObject, collider.gameObject.layer);
                collider.gameObject.layer = LayerMask.NameToLayer("Shielded");
            }
        }
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

    void OnDestroy()
    {
        foreach (KeyValuePair<GameObject, int> entry in shieldedObject)
        {
            entry.Key.layer = entry.Value;
        }
    }

    public void Damage()
    {
        currentHp--;
        if (currentHp <= 0f) Destroy(gameObject);
        else isBlinking = true;
    }
}
