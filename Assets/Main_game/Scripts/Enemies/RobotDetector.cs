using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDetector : MonoBehaviour {

	private BoxCollider2D bc2D;

	[SerializeField]
	private Robot robot;

	// Use this for initialization
	void Start () {
		bc2D = GetComponent<BoxCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D _collision)
	{
		if (_collision.tag == "Player")
		{
			robot.Target = _collision.gameObject;
		}

	}
	private void OnTriggerExit2D(Collider2D _collision)
	{
		if (_collision.tag == "Player")
		{
			robot.Target = null;
		}
	}
}
