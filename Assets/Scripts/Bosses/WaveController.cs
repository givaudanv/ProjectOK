using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float waveSpeed;

    void Update()
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<PlayerController>().Damage();
    }
}
