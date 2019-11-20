using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWall : MonoBehaviour
{
    public Elements element;

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<ShotController>().SetElement(element);
    }
}
