using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAddController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;

    [HideInInspector]
    public float bulletCd;

    private float bulletCurrentCd;
    private GameObject target;
    private Quaternion initialRotation;

    void Start()
    {
        bulletCd = 2f;
        bulletCurrentCd = 0f;
        target = GameObject.FindWithTag("Player");
        Quaternion initialRotation = transform.rotation;
        Debug.Log(Vector3.Distance(transform.position, transform.parent.position));
    }

    void Update()
    {
        Shoot();
        Move();
    }

    private void Move()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, speed * Time.deltaTime);
        transform.rotation = initialRotation;
    }

    private void Shoot()
    {
        if (bulletCurrentCd >= bulletCd && target)
        {
            bulletCurrentCd = 0f;

            Vector2 direction = target.transform.position - transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        }

        bulletCurrentCd += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            ShotController sc = other.GetComponent<ShotController>();
            sc.Stop();
            GetComponentInParent<FirstBossController>().AddDestroyed();
            Destroy(gameObject);
        }
    }
}
