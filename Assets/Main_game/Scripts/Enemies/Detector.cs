using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	private PolygonCollider2D detector;

	[SerializeField]
	private List<StandartTurret> standartTurret;
	// Use this for initialization
	void Start () {
		detector = gameObject.GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	private void OnTriggerEnter2D(Collider2D _collision)
	{

		if (_collision.tag == "Player")
			foreach (StandartTurret tur in standartTurret)
			{
				{
					tur.Target = _collision.gameObject;

				}
			}

	}
	private void OnTriggerExit2D(Collider2D _collision)
	{

		if (_collision.tag == "Player")
		{
			foreach (StandartTurret tur in standartTurret)
			{
				tur.Target = null;
			}
		}
	}

}
