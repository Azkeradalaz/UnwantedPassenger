using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivationPanel : MonoBehaviour {
	[SerializeField]
	private List<Door> doors = new List<Door>();


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player" && collision.GetComponent<PlayerControl>().useButtonActive)
		{
			Color dCol = new Color(0, 1f, 0);
			foreach (Door door in doors)
			{
				door.ActivaDoor();
				door.GetComponent<SpriteRenderer>().color = dCol;
			}
			Color pCol = new Color(1f, 0, 0);
			gameObject.GetComponent<SpriteRenderer>().color = pCol;
		}

	}
}
