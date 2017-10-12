using UnityEngine;
using System.Collections;
using PicoGames.VLS2D;

public class Elevator : MonoBehaviour {

	private Vector3 posA;

	private Vector3 posB;

	private Vector3 nextPos;
	[SerializeField]
	private enum ElevatorType { Standart, WithButtons };
	[SerializeField]
	private ElevatorType elevatorType;


	[SerializeField]
	private float speed = 0.2f;
	[SerializeField]
	private float stopTime = 1f;

	[SerializeField]
	private Transform childTransform;

	[SerializeField]
	private Transform transformB;



	private bool continueSequence = true;



	[SerializeField]
	private bool broken;
	[SerializeField]
	private bool active;

	private bool isMoving;

	private void Start()
	{
		posA = childTransform.localPosition;
		posB = transformB.localPosition;
		nextPos = posB;
	}


	private void Awake()
	{

	}

	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.GetChild(0).GetComponent<VLSObstructor>().UpdateFromReferencedCollider();

		if (elevatorType == ElevatorType.Standart)
		{
			if (!broken)
			{
				MovingSequence();
			}
		}
		else if (elevatorType == ElevatorType.WithButtons)
		{

			if (!broken && active)
			{
				MovingSequenceWithButton();
			}
		}
				
	}

	private void MovingSequence()
	{
		
		//childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed*Time.deltaTime);

		//if (Vector3.Distance(childTransform.localPosition, nextPos) == 0)
		//{
		//	if (continueSequence)
		//	{
		//		StartCoroutine(ChangeDestinationTimer());
		//	}
		//	continueSequence = false;
		//}	
	}

	private void MovingSequenceWithButton()
	{
		isMoving = true;

		childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
		childTransform.localPosition = SmoothApproach(childTransform.localPosition, childTransform.localPosition, nextPos, speed);
		//Vector3 speed = nextPos - transform.position;
		//myRB.MovePosition(myRB.position + speed * Time.deltaTime);

		if (Vector3.Distance(childTransform.localPosition, nextPos) == 0)
			{
				ChangeDestination();
				active = false;
				isMoving = false;
			}
	}
	Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
	{
		float t = Time.deltaTime * speed;
		Vector3 v = (targetPosition - pastTargetPosition) / t;
		Vector3 f = pastPosition - pastTargetPosition + v;
		return targetPosition - v + f * Mathf.Exp(-t);
	}
	public void ActivateElevator()
	{
		if (!broken)
		{
			active = true;
		}
	}



	private IEnumerator ChangeDestinationTimer()
	{
		yield return new WaitForSeconds(stopTime);
		ChangeDestination();
	}


	private void ChangeDestination()
	{
		nextPos = nextPos != posA ? posA : posB;
		continueSequence = true;
	}
	public void RepairElevator()
	{
		broken = false;
	}
}
