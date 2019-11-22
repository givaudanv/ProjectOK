using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWall : MonoBehaviour
{
    public Elements element;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
            collision.gameObject.GetComponent<ShotController>().SetElement(element);
    }
}
