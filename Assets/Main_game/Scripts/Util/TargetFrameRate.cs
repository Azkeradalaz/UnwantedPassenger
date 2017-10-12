using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour {

	[SerializeField]
	private int frameRate = 60;
	private void Awake()
	{
		Application.targetFrameRate = 60;
	}
	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	if (Application.targetFrameRate != frameRate)
		{
			Application.targetFrameRate = frameRate;
		}	
	}
}
