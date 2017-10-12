using System.Collections;
using UnityEngine;

public class VLS2DCamSolver : MonoBehaviour {


	//solving problem with VLS2D stuck light rendering after reloading the scene
	[SerializeField]
	private Camera cam;
	void Start()
	{
		StartCoroutine(LVS2DCamSolvHelp());
	}

	private IEnumerator VLS2DCamSolv()
	{

		cam.GetComponent<Camera>().enabled = false;

		yield return new WaitForSeconds(.1f);
		cam.GetComponent<Camera>().enabled = true;

	}

	private IEnumerator LVS2DCamSolvHelp()
	{
		yield return new WaitForSeconds(.1f);
		StartCoroutine(VLS2DCamSolv());
	}
}
