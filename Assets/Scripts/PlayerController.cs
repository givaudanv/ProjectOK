using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float minDashDistance;
    [SerializeField] private float maxDashDistance;

    private Rigidbody2D _rb;
    private Vector2 _moveVelocity;
    private float _dashStartTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + speed * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);

        if (Input.GetMouseButtonDown(1))
        {
            _dashStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = _rb.position;
            Vector2 direction = (mousePos - position).normalized;
            float distance = Mathf.Clamp(dashSpeed * (Time.time - _dashStartTime), minDashDistance, maxDashDistance);
            _rb.MovePosition(position + direction * distance);
        }
    }
}