using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour {

	[SerializeField]
	private GameObject elevator;
	private bool activated;
	private bool useButtonActive;

	private void OnTriggerStay2D(Collider2D collision)
	{
		PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();


		if (collision.transform.tag == "Player" && pc.useButtonActive)
		{
			if (!activated)
			{
				activated = true;
				elevator.transform.GetComponent<Elevator>().ActivateElevator();
				Debug.Log("Active");
				StartCoroutine(Deactivate());
			}
		}
	}
	private IEnumerator Deactivate()
	{
		yield return new WaitForSeconds(2f);
		activated = false;
	}

}
