using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float minDashDistance;
    [SerializeField] private float maxDashDistance;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform pfDashEffect;

    [SerializeField] private bool isDashing;
    [SerializeField] private Vector2 dashTarget;
    [SerializeField] private float dashDistance;
    [SerializeField] private bool chargingDash;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 lastDashDir;

    private Rigidbody2D _rb;
    private float _dashStartTime;
    private Vector2 _moveVelocity;

    public bool Godmod;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        chargingDash = false;
        lastDashDir = Vector2.up;
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        lastDashDir = direction == Vector2.zero ? lastDashDir : direction;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashStartTime = Time.time;
            chargingDash = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            dashDistance = dashSpeed * (Time.time - _dashStartTime);
            float distance = dashDistance + minDashDistance;
            distance = distance > maxDashDistance ? maxDashDistance : distance;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDashDir, distance, obstacleLayer);
            if (hit)
            {
                distance *= hit.fraction;
            }

            dashTarget = (Vector2)transform.position + distance * lastDashDir;

            isDashing = true;
            chargingDash = false;
        }
    }

    private void FixedUpdate()
    {
        if (!chargingDash)
            _rb.MovePosition(_rb.position + speed * direction);

        var position = transform.position;
        if (isDashing)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerDashing");
            float distSqr = (dashTarget - (Vector2)transform.position).sqrMagnitude;
            if (distSqr < 2f)
            {
                isDashing = false;
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
            else
            {
                Transform dashEffectTransform = Instantiate(pfDashEffect, position, Quaternion.identity);
                dashEffectTransform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(((Vector3)dashTarget - position).normalized));
                float dashEffectWidth = 30f;
                dashEffectTransform.localScale = new Vector3(Vector3.Distance(dashTarget, position) / dashEffectWidth - 0.05f, 0.15f, 1f);
                _rb.MovePosition(dashTarget);
            }
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }


    public void Damage()
    {
        if (!Godmod)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Damage();
    }
}
