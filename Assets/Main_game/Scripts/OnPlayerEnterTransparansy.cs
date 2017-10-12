using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerEnterTransparansy : MonoBehaviour {
	private Color rend;
	[Range(0, 255)]
	[SerializeField]
	private float transparancy;
	private void Start()
	{
		rend = gameObject.GetComponent<SpriteRenderer>().color;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			rend = new Color(rend.r, rend.g, rend.b, transparancy / 256);
			gameObject.GetComponent<SpriteRenderer>().color = rend;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			rend = new Color(rend.r, rend.g, rend.b, 1f);
			gameObject.GetComponent<SpriteRenderer>().color = rend;
		}
	}

}
