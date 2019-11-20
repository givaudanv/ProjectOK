using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPlate : MonoBehaviour
{
    public Elements element;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerController>().weapon.GetComponent<PlayerElement>().element = element;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerController>().weapon.GetComponent<PlayerElement>().element = Elements.Neutral;
    }
}
