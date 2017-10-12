using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerEnterTransparansyChildren : MonoBehaviour {
	private Color rend;
	[Range(0, 255)]
	[SerializeField]
	private float transparancy;
	[SerializeField]
	private Color color;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			rend = new Color(color.r, color.g, color.b, transparancy / 256);
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponent<SpriteRenderer>().color = rend;
			}
			
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			rend = new Color(color.r, color.g, color.b, 1f);
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponent<SpriteRenderer>().color = rend;
			}
		}
	}
	
}
