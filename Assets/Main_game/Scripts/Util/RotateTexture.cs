using UnityEngine;

public class RotateTexture : MonoBehaviour {

	[SerializeField]
	private Material material;

	void FixedUpdate ()
	{
		material.SetFloat("_RotationSpeed", Mathf.Deg2Rad * gameObject.transform.eulerAngles.z);	
	}
}
