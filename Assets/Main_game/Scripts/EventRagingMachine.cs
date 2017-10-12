using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRagingMachine : MonoBehaviour {
	private Color rend;
	[SerializeField]
	private GameObject hand;
	[SerializeField]
	private GameObject boxPile;
	[SerializeField]
	private GameObject elevator;
	private void Start()
	{
		rend = hand.gameObject.GetComponent<SpriteRenderer>().color;
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player" && collision.transform.GetComponent<PlayerControl>().useButtonActive)
		{
			StartCoroutine(Event());
		}
	}
	

	private IEnumerator Event()
	{

		rend = new Color(1f, 0, 0);
		hand.gameObject.GetComponent<SpriteRenderer>().color = rend;
		yield return new WaitForSeconds(5f);
		elevator.GetComponent<Elevator>().RepairElevator();
		Destroy(boxPile);

		
	}
}
