using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {


	public GameObject Target { get; set; }
	[SerializeField]
	private float movementSpeed = 1f;
	[SerializeField]
	private float roundPerMinute = 30f;

	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private GameObject shootingPoint;

	private bool isDead;

	private Animator myAnim;

	private SpriteRenderer spriteRenderer;

	private bool canShoot = true;
	// Use this for initialization
	private void Start()
	{
		myAnim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	private void Update()
	{
		if (isDead)
			return;
		FlipToTarget();
		MoveToTarget();
		ShootAtTarget();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Projectile")
		{
			Death();
		}

	}

	private void FlipToTarget()
	{
		if (Target != null)
		{
			if (gameObject.transform.localScale.x < 0 && Target.transform.position.x - gameObject.transform.position.x > 0)
			{
				gameObject.transform.localScale = new Vector3(1, 1, 1);
			}

			else if (gameObject.transform.localScale.x > 0 && Target.transform.position.x - gameObject.transform.position.x < 0)
			{
				gameObject.transform.localScale = new Vector3(-1, 1, 1);
			}
		}
	}

	private void MoveToTarget()
	{

		if (Target != null)
		{
			transform.Translate(GetDirection() * movementSpeed * Time.deltaTime);
			myAnim.SetFloat("speed", 1f);
		}
		else myAnim.SetFloat("speed", 0f);
	}
	private void ShootAtTarget()
	{
		if (Target != null)
		{
			if (canShoot)
			{
				canShoot = false;

				if (GetDirection() == Vector2.right)
				{
					Instantiate(projectilePrefab, shootingPoint.transform.position, Quaternion.AngleAxis(0, Vector3.forward));
				}

				else if (GetDirection() == Vector2.left)
				{
					Instantiate(projectilePrefab, shootingPoint.transform.position, Quaternion.AngleAxis(180, Vector3.forward));
				}
				StartCoroutine(FireDelay(60/roundPerMinute));
			}
		}

	}

	private IEnumerator FireDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		canShoot = true;

	}
	
	private void Death()
	{
		isDead = true;
		spriteRenderer.color = Color.red;
	}

	private Vector2 GetDirection()
	{
		return gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
	}
}

