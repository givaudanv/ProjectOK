using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Elements element;
    public GameObject shotPrefab;
    public float chargeTime;

    private float currentCharge;
    private bool charging;

    void Update()
    {
        TrackMouse();
        ShotCharge();
    }

    void ShotCharge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            charging = true;
            currentCharge = 0f;
        }

        if (charging) currentCharge += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            charging = false;
            if (currentCharge >= chargeTime) Shoot();
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(shotPrefab, transform.position, transform.rotation);
        go.GetComponent<ShotController>().SetElement(element);
    }

    void TrackMouse()
    {
        Vector2 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
