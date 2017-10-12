using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartTurret : MonoBehaviour {


	public GameObject Target { get; set; }

	

	private GameObject shootingPoint;
	private GameObject gun;

	private BoxCollider2D bc2D;
	private PolygonCollider2D detector;

	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private float speed = 100f;
	[SerializeField]
	private float roundPerMinute = 30f;

	private bool canFire=true;

	private bool isBroken;

	// Use this for initialization
	void Start () {

		gun = this.gameObject.transform.GetChild(0).gameObject;
		//gun = GameObject.Find("Turret/Gun");
		shootingPoint = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
		//shootingPoint = GameObject.Find("Turret/Gun/ShootingPoint");
		bc2D = gameObject.GetComponent<BoxCollider2D>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isBroken)
		{
			if (Target != null)
			{
				Vector3 vectorToTarget = Target.transform.position - transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg +90;
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
				gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, q, Time.deltaTime * speed);
				if (canFire)
				{
					Shoot();
				}
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Projectile")
		{
			Death();
		}
		
	}

	public void Death()
	{

		isBroken = true;	
		spriteRenderer.color = Color.red;
	}

	private void Shoot()
	{
		canFire = false;
		Instantiate(projectilePrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
		StartCoroutine(FireDelay(60f / roundPerMinute));

	}
	private IEnumerator FireDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		canFire = true;
	}
}
