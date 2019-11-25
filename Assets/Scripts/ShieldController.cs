using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Elements element;
    public float hp;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shot")
        {
            other.GetComponent<ShotController>().Stop();
        }
    }
}
