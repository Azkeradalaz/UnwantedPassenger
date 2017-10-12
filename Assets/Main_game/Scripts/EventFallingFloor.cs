using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFallingFloor : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			StartCoroutine(Event());
		}
	}
	private IEnumerator Event()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
