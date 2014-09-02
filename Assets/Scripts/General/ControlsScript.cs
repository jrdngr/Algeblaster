using UnityEngine;
using System.Collections;


// Script for the splash screen that shows the controls

public class ControlsScript : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown ("Fire1"))
			Application.LoadLevel ("TestLevel1");
	}
}
