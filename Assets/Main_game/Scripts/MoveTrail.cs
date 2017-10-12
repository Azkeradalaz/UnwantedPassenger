using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class MoveTrail : MonoBehaviour {


	[SerializeField]
	private int trailSpeed = 200;

	[SerializeField]
	private int pushForce=100;

	[SerializeField]
	private float destroyTimer = 4f;

	private Rigidbody2D myRB2D;

	private Vector3 direction;

	private BoxCollider2D bc2D;
	[SerializeField]
	private LayerMask whatToHit;
	// Update is called once per frame


	private void Start()
	{
		myRB2D = GetComponent<Rigidbody2D>();
		bc2D = GetComponent<BoxCollider2D>();
		
	}
	void Update () {
		transform.Translate(Vector3.right*Time.deltaTime*trailSpeed);
		//myRB2D.velocity = Vector3.right * Time.deltaTime * trailSpeed;
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject, destroyTimer);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Hit " + collision.name);
		if (collision.gameObject.layer == 9)
		{
			if (collision.gameObject.tag == "Movable")
			{
				Vector3 dir = transform.position - collision.transform.position;
				dir.Normalize();
				collision.GetComponent<Rigidbody2D>().AddForce(-dir * pushForce);
				Destroy(gameObject);


			}

			else if (collision.gameObject.tag != "Movable")
			{
				Destroy(gameObject);
			}
		}
		
	}

}
