using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpiralController : MonoBehaviour {
    public float waveSpeed;
    public float killRadius;

    private ParticleSystem.ShapeModule shape;

    private void Start() {
        shape = GetComponent<ParticleSystem>().shape;
    }

    void Update() {
        if(shape.radius >= killRadius)
            Destroy(gameObject);
        shape.radius += waveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>()
                 .Damage();
    }
}