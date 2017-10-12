using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckToElevator : MonoBehaviour
{

	//private void OnTriggerStay(other:Collider)
	//{

	//if (other.gameObject.tag == "platform")
	//{
	//	transform.parent = other.transform;

	//}
	//}

	//private void OnTriggerExit(other:Collider)
	//{
	//	if (other.gameObject.tag == "platform")
	//	{
	//		transform.parent = null;

	//	}
	//}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Elevator")
		{
			gameObject.transform.parent = null;

		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.gameObject.tag == "Elevator")
		{
			gameObject.transform.parent = collision.transform;
		}

}
}
