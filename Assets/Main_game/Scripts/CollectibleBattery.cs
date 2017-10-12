using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBattery : MonoBehaviour
{

	private BoxCollider2D boxCollider;
	[SerializeField]
	private float energyAmount = 20f;

	private bool toBeDestroyed; // for double use restiction

	// Use this for initialization
	void Start()
	{
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (!toBeDestroyed)
			{
				toBeDestroyed = true;
				PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
				pc.GetBattery(energyAmount);
				Debug.Log("Energy bonus: " + energyAmount);
				Destroy(gameObject);
				
			}

		}
	}
}
