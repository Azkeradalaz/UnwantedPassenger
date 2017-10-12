using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleUpgradeComponent : MonoBehaviour {


	private bool toBeDestroyed; // for double use restiction
	[SerializeField]
	private int count = 1;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (!toBeDestroyed)
			{
				toBeDestroyed = true;
				collision.transform.GetComponent<PlayerControl>().GetUpgradeComponents(count);


				Destroy(gameObject);
			}
		}
	}
}
