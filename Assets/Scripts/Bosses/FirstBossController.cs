using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject wavePrefab;
    public int nbBulletCircle;
    public float bulletCircleCd;
    public float waveCd;

    [HideInInspector]
    public int nbAdd;

    private float bulletCircleCurrentCd;
    private float waveCurrentCd;
    private float bulletCircleStride;

    private bool waveEnabled = false;
    private bool bulletCircleEnabled = false;

    void Start()
    {
        bulletCircleCurrentCd = 0f;
        waveCurrentCd = 0f;
        nbAdd = 3;
    }

    void Update()
    {
        if (bulletCircleEnabled) { ShootCircle(); }
        if (waveEnabled) { ShootWave(); }
    }

    private void ShootCircle()
    {
        if (bulletCircleCurrentCd >= bulletCircleCd)
        {
            bulletCircleStride = 360f / nbBulletCircle;
            bulletCircleCurrentCd = 0f;
            for (int i = 0; i < nbBulletCircle; i++)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, bulletCircleStride * i)));
            }
        }

        bulletCircleCurrentCd += Time.deltaTime;
    }

    private void ShootWave()
    {
        if (waveCurrentCd >= waveCd)
        {
            waveCurrentCd = 0f;
            Instantiate(wavePrefab, transform.position, Quaternion.identity);
        }

        waveCurrentCd += Time.deltaTime;
    }

    public void AddDestroyed()
    {
        nbAdd--;
        FirstBossAddController[] addControllers = GetComponentsInChildren<FirstBossAddController>();
        foreach (FirstBossAddController add in addControllers)
        {
            add.bulletCd -= 0.5f;
        }

        if (nbAdd <= 2) { bulletCircleEnabled = true; }
        if (nbAdd <= 1) { waveEnabled = true; }
        if (nbAdd <= 0) { nbBulletCircle += 10; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
