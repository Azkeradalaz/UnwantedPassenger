using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : MonoBehaviour {


	const float oxygenMaxAmout = 100f;

	[Range(0, oxygenMaxAmout)]
	public float oxygenAmount = 100f;
	[SerializeField]
	private float oxygenRegen = 0.1f;
	[SerializeField]
	private float oxygenCharge = 10f;

	[SerializeField]
	private Color startColor;
	[SerializeField]
	private Color depletedColor;

	private SpriteRenderer spriteRenderer;

	private bool useButtonActive;


	private void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
		if (collision.transform.tag == "Player" && pc.useButtonActive)
		{
			if (oxygenAmount >= 0f)
			{
				pc.RechargeOxygen(oxygenCharge*Time.deltaTime);
				oxygenAmount -= oxygenCharge* Time.deltaTime;
			}
		}
	}

	// Update is called once per frame

	void Update () {
		if (oxygenAmount <= oxygenMaxAmout)
		{
			oxygenAmount += oxygenRegen * Time.deltaTime;
			spriteRenderer.color = Color.Lerp(depletedColor, startColor, Map(oxygenAmount, 0, oxygenMaxAmout, 0, 1));
		}


	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
