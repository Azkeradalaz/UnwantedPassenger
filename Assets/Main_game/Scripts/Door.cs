using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField]
	private bool isActive=false;
	private Animator anim;
	private bool doorStateChanged = false;
	[SerializeField]
	private bool open;
	private bool coroutineIsRunning;
	private GameObject entry;
	private IEnumerator coroutine;
	// Use this for initialization

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		anim.SetBool("open", open);
		entry = gameObject.transform.GetChild(0).gameObject;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive)
		{
			if (doorStateChanged)
			{
				anim.SetBool("open", open);
				doorStateChanged = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isActive)
		{
			if (collision.tag == "Player")
			{
				if (coroutineIsRunning)
				{
					StopClosing();
				}
				open = true;
				entry.SetActive(false);
				doorStateChanged = true;
			}
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isActive)
		{
			if (collision.tag == "Player")
			{
				if (!open){
					if (coroutineIsRunning)
					{
						StopClosing();
					}
					open = true;
					entry.SetActive(false);
					doorStateChanged = true;
				}
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isActive)
		{
			if (collision.tag == "Player")
			{
				if (!coroutineIsRunning)
				{
					
					coroutine = WaitToClose();
					StartCoroutine(coroutine);
				}
			}
		}
	}


	private IEnumerator WaitToClose()
	{
		coroutineIsRunning = true;
		yield return new WaitForSeconds(0.5f);
		open = false;
		entry.SetActive(true);
		doorStateChanged = true;
		coroutineIsRunning = false;
	}
	public void ActivaDoor()
	{
		isActive = true;
	}

	private void StopClosing()
	{
		StopCoroutine(coroutine);
		coroutineIsRunning = false;
	}

}
