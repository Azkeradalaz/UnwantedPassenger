﻿using UnityEngine;


public class RotateToMouse : MonoBehaviour {


	void FixedUpdate()
    {
		
		Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}

}
