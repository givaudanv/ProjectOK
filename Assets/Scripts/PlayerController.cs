using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float speed;
	[SerializeField] private float dashSpeed;
	[SerializeField] private float minDashDistance;
	[SerializeField] private float maxDashDistance;
	[SerializeField] private LayerMask obstacleLayer;

	private Rigidbody2D rb;
	private Vector2 _moveVelocity;
	private float _dashStartTime;

	public bool isDashing;
	public Vector2 dashTarget;
	public Transform pfDashEffect;

	public float dashDistance;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(1)) {
			_dashStartTime = Time.time;
		}

		if (Input.GetMouseButtonUp(1)) {
			dashDistance = dashSpeed * (Time.time - _dashStartTime);

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 dashDir = (mousePos - rb.position).normalized;

			float distance = dashDistance + minDashDistance;
			distance = distance > maxDashDistance ? maxDashDistance : distance;
			float rayDistance = 1000;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDir, distance, obstacleLayer);
			if (hit) {
				distance = hit.distance;
			}
			
			dashTarget = (Vector2) transform.position + distance * dashDir;
			isDashing = true;
		}
	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + speed * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);
		
		if (isDashing) {
			gameObject.layer = LayerMask.NameToLayer("PlayerDashing");
			float distSqr = (dashTarget - (Vector2) transform.position).sqrMagnitude;
			if (distSqr < 0.01f) {
				isDashing = false;
				gameObject.layer = LayerMask.NameToLayer("Player");
			}
			else {
				Transform dashEffectTransform = Instantiate(pfDashEffect, transform.position, Quaternion.identity);
				dashEffectTransform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(((Vector3) dashTarget - transform.position).normalized));
				float dashEffectWidth = 30f;
				dashEffectTransform.localScale = new Vector3(Vector3.Distance(dashTarget, transform.position) / dashEffectWidth - 0.05f, 0.15f, 1f);
				rb.MovePosition(dashTarget);
			}
		}
	}
	
	public static float GetAngleFromVectorFloat(Vector3 dir) {
		dir = dir.normalized;
		float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		if (n < 0) n += 360;

		return n;
	}
}